using System;


namespace MyBank
{
    class Account
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthday { get; private set; }
        public long OIB { get; private set; }
        public SexType Sex { get; private set; }
        public int ID { get; private set; }
        public decimal Balance { get; private set; }
        public AccountType Type { get; private set; }

        public Account(){}

        public Account(string firstName, string lastName, DateTime birthday, long oib, SexType Sex,
                       int accountNumber, AccountType accountType, decimal balance = 0)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
            this.OIB = oib;
            this.Sex = Sex;
            this.ID = accountNumber;
            this.Balance = balance;
            this.Type = accountType;
        }
    }
}
