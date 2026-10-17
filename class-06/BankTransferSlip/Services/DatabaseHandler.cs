using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using BankTransferSlip.Entities;
using Microsoft.Data.Sqlite;


namespace BankTransferSlip.Services
{
    class DatabaseHandler
    {
        private static readonly string _databasePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create),
            "BankTransferSlip", "BankTransferSlip.db");

        private SqliteConnection _dbConnection;

        public DatabaseHandler()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_databasePath));
            _dbConnection = new SqliteConnection($"Data Source={_databasePath}");
            _dbConnection.Open();
            CreateTables();
        }

        public void CloseDatabaseConnection()
        {
            if (_dbConnection.State != ConnectionState.Closed)
            {
                _dbConnection.Close();
            }
        }

        // SQLite has no decimal type, so amounts are stored as text to keep them exact
        private void CreateTables()
        {
            ExecuteQuery("CREATE TABLE IF NOT EXISTS tbl_Users (" +
                         "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                         "FullName TEXT NOT NULL COLLATE NOCASE, Address TEXT NOT NULL COLLATE NOCASE, IBAN TEXT NOT NULL COLLATE NOCASE, " +
                         "Balance TEXT NOT NULL, IncomingCounter INTEGER NOT NULL, OutgoingCounter INTEGER NOT NULL)");

            ExecuteQuery("CREATE TABLE IF NOT EXISTS tbl_Slips (" +
                         "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                         "Payer_Id INTEGER NOT NULL REFERENCES tbl_Users (Id), Recipient_Id INTEGER NOT NULL REFERENCES tbl_Users (Id), " +
                         "Urgent INTEGER NOT NULL, Currency TEXT NOT NULL, Amount TEXT NOT NULL, Model TEXT NOT NULL, ReferenceNumber TEXT NOT NULL)");
        }

        private SqliteCommand CreateCommand(string query, params (string Name, object Value)[] parameters)
        {
            var sqlCommand = new SqliteCommand(query, _dbConnection);

            foreach (var (name, value) in parameters)
            {
                sqlCommand.Parameters.AddWithValue(name, value);
            }
            return sqlCommand;
        }

        private void ExecuteQuery(string query, params (string Name, object Value)[] parameters)
        {
            using (var sqlCommand = CreateCommand(query, parameters))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }

        private static (string Name, object Value)[] UserParameters(User user, params (string Name, object Value)[] extraParameters)
        {
            var parameters = new List<(string Name, object Value)>
            {
                ("@fullName", user.FullName), ("@address", user.Address), ("@iban", user.IBAN)
            };
            parameters.AddRange(extraParameters);
            return parameters.ToArray();
        }

        private bool IfUserRecordExists(User user)
        {
            var getUserRecordQuery = "SELECT COUNT(*) FROM tbl_Users WHERE FullName = @fullName AND Address = @address AND IBAN = @iban";

            using (var sqlCommand = CreateCommand(getUserRecordQuery, UserParameters(user)))
            {
                return Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0;
            }
        }

        public void SaveRecord(Slip slip)
        {
            AddOrUpdateRecord(slip.Payer);
            AddOrUpdateRecord(slip.Recipient);

            AddSlipRecord(slip);
        }

        private void AddOrUpdateRecord(User user)
        {
            if (!IfUserRecordExists(user))
            {
                AddUserRecord(user);
            }
            else
            {
                UpdateUserRecord(user);
            }
        }

        private void AddUserRecord(User user)
        {
            var addUserQuery = "INSERT INTO tbl_Users ([FullName],[Address],[IBAN],[Balance],[IncomingCounter],[OutgoingCounter]) " +
                               "VALUES (@fullName, @address, @iban, @balance, @incomingCounter, @outgoingCounter)";

            ExecuteQuery(addUserQuery, UserParameters(user,
                ("@balance", user.Balance), ("@incomingCounter", user.IncomingCounter), ("@outgoingCounter", user.OutgoingCounter)));
        }

        private void UpdateUserRecord(User user)
        {
            var getBalanceQuery = "SELECT Balance FROM tbl_Users WHERE FullName = @fullName AND Address = @address AND IBAN = @iban";
            decimal balance;

            using (var sqlCommand = CreateCommand(getBalanceQuery, UserParameters(user)))
            {
                balance = decimal.Parse((string)sqlCommand.ExecuteScalar(), CultureInfo.InvariantCulture);
            }

            var updateUserQuery = "UPDATE tbl_Users " +
                                  "SET IncomingCounter = IncomingCounter + @incomingCounter, " +
                                  "OutgoingCounter = OutgoingCounter + @outgoingCounter, Balance = @balance " +
                                  "WHERE FullName = @fullName AND Address = @address AND IBAN = @iban";

            ExecuteQuery(updateUserQuery, UserParameters(user,
                ("@incomingCounter", user.IncomingCounter), ("@outgoingCounter", user.OutgoingCounter), ("@balance", balance + user.Balance)));
        }

        private void AddSlipRecord(Slip slip)
        {
            var payerId = GetUserId(slip.Payer);
            var recipientId = GetUserId(slip.Recipient);
            var addSlipQuery = "INSERT INTO tbl_Slips " +
                               "([Payer_Id],[Recipient_Id],[Urgent],[Currency],[Amount],[Model],[ReferenceNumber]) " +
                               "VALUES (@payerId, @recipientId, @urgent, @currency, @amount, @model, @referenceNumber)";

            ExecuteQuery(addSlipQuery,
                ("@payerId", payerId), ("@recipientId", recipientId), ("@urgent", slip.Urgent), ("@currency", slip.Currency),
                ("@amount", slip.Amount), ("@model", slip.Model), ("@referenceNumber", slip.ReferenceNumber));
        }

        private int GetUserId(User user)
        {
            var getUserIdQuery = "SELECT Id FROM tbl_Users WHERE FullName = @fullName AND Address = @address AND IBAN = @iban";
            int userId = -1;

            using (var sqlCommand = CreateCommand(getUserIdQuery, UserParameters(user)))
            {
                using (var sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        userId = sqlReader.GetInt32(0);
                    }
                }
            }
            return userId;
        }

        public List<UserRecord> GetUserRecords(BaseUser payer, BaseUser recipient)
        {
            var getUserRecordQuery = "SELECT Id, FullName, Address, IBAN, Balance, IncomingCounter, OutgoingCounter FROM tbl_Users " +
                                     "WHERE FullName = @payerFullName AND Address = @payerAddress " +
                                     "OR FullName = @recipientFullName AND Address = @recipientAddress " +
                                     "ORDER BY FullName";
            var userRecords = new List<UserRecord>();

            using (var sqlCommand = CreateCommand(getUserRecordQuery,
                ("@payerFullName", payer.FullName), ("@payerAddress", payer.Address),
                ("@recipientFullName", recipient.FullName), ("@recipientAddress", recipient.Address)))
            {
                using (var sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        userRecords.Add(new UserRecord
                        {
                            Id = sqlReader.GetInt32(0),
                            FullName = sqlReader.GetString(1),
                            Address = sqlReader.GetString(2),
                            IBAN = sqlReader.GetString(3),
                            Balance = decimal.Parse(sqlReader.GetString(4), CultureInfo.InvariantCulture),
                            IncomingCounter = sqlReader.GetInt32(5),
                            OutgoingCounter = sqlReader.GetInt32(6)
                        });
                    }
                }
            }
            return userRecords;
        }
    }
}
