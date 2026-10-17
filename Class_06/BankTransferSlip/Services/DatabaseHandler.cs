using System;
using System.Data;
using System.Data.SqlClient;
using BankTransferSlip.Entities;


namespace BankTransferSlip.Services
{
    class DatabaseHandler
    {
        private string _connectionString;
        private SqlConnection _dbConnection;

        public DatabaseHandler()
        {
            _connectionString = Properties.Settings.Default.connection_String;
            _dbConnection = new SqlConnection(_connectionString);
            _dbConnection.Open();
        }

        public void CloseDatabaseConnection()
        {
            if (_dbConnection.State != ConnectionState.Closed)
            {
                _dbConnection.Close();
            }
        }

        private void ExecuteQuery(string query)
        {
            using (var sqlCommand = new SqlCommand(query, _dbConnection))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }

        private bool IfUserRecordExists(User user)
        {
            var getUserRecordQuery = $"SELECT COUNT(*) FROM tbl_Users WHERE FullName = '{user.FullName}' " +
                                $"AND Address = '{user.Address}' AND IBAN = '{user.IBAN}'";

            using (var sqlCommand = new SqlCommand(getUserRecordQuery, _dbConnection))
            {
                return (Convert.ToInt32(sqlCommand.ExecuteScalar().ToString()) > 0) ? true : false;
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
            var addUserQuery = $"INSERT INTO tbl_Users ([FullName],[Address],[IBAN],[Balance],[IncomingCounter],[OutgoingCounter]) " +
                            $"VALUES ('{user.FullName}','{user.Address}','{user.IBAN}',{user.Balance},{user.IncomingCounter},{user.OutgoingCounter})";

            ExecuteQuery(addUserQuery);
        }

        private void UpdateUserRecord(User user)
        {
            var updateUserQuery = $"UPDATE tbl_Users " +
                        $"SET IncomingCounter = IncomingCounter + {user.IncomingCounter}, " +
                        $"OutgoingCounter = OutgoingCounter + {user.OutgoingCounter}, Balance = Balance + {user.Balance} " +
                        $"WHERE FullName = '{user.FullName}' AND Address = '{user.Address}' AND IBAN = '{user.IBAN}'";

            ExecuteQuery(updateUserQuery);
        }

        private void AddSlipRecord(Slip slip)
        {
            var payerId = GetUserId(slip.Payer);
            var recipientId = GetUserId(slip.Recipient);
            var addSlipQuery = $"INSERT INTO tbl_Slips " +
                               $"([Payer_Id],[Recipient_Id],[Urgent],[Currency],[Amount],[Model],[CallNumber]) " +
                               $"VALUES ('{payerId}','{recipientId}','{slip.Urgent}','{slip.Currency}'," +
                               $"'{slip.Amount}','{slip.Model}','{slip.CallNumber}')";

            ExecuteQuery(addSlipQuery);
        }

        private int GetUserId(User user)
        {
            var getUserIdQuery = $"SELECT * FROM tbl_Users WHERE FullName = '{user.FullName}' " +
                                $"AND Address = '{user.Address}' AND  IBAN = '{user.IBAN}'";
            int userId = -1;

            using (var sqlCommand = new SqlCommand(getUserIdQuery, _dbConnection))
            {
                using (var sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        userId = int.Parse(sqlReader[0].ToString());
                    }
                }
            }
            return userId;
        }

        public DataTable GetDataTable(BaseUser payer, BaseUser recipient)
        {
            var getUserRecordQuery = $"SELECT * FROM tbl_Users " +
                                    $"WHERE FullName = '{payer.FullName}' AND Address = '{payer.Address}' " +
                                    $"OR FullName = '{recipient.FullName}' AND Address = '{recipient.Address}' " +
                                    $"ORDER BY FullName";
            
            using (var sqlCommand = new SqlCommand(getUserRecordQuery, _dbConnection))
            {
                var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                var dataTable = new DataTable("Users");

                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
        }
    }
}
