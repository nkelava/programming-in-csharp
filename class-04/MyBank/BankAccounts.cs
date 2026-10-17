using System;
using System.Collections.Generic;


namespace MyBank
{
    class BankAccounts
    {
        public static Dictionary<int, Account> accounts = new Dictionary<int, Account>();

        public static void Add(Account newAccount)
        {
            accounts.Add(newAccount.ID, newAccount);
        }

        public static int GenerateAccountNumber()
        {
            int newAccountId;
            var randomId = new Random();

            do
            {
                newAccountId = randomId.Next(1, int.MaxValue);
            } while (CheckIfExists(newAccountId));

            return newAccountId;
        }

        private static bool CheckIfExists(int newId)
        {
            if (accounts.ContainsKey(newId))
            {
                return true;
            }
            return false;
        }

        public static Tuple<bool, Account> SearchAccount(long oib)
        {
            foreach(var account in accounts)
            {
                if(account.Value.OIB == oib)
                {
                    return new Tuple<bool, Account>(true, account.Value);
                }
            }
            return new Tuple<bool, Account>(false, new Account());
        }
    }
}
