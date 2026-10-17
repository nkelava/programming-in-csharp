

namespace BankTransferSlip.Entities
{
    class User : BaseUser
    {
        public string IBAN { get; private set; }
        public decimal Balance { get; private set; }
        public int IncomingCounter { get; private set; }
        public int OutgoingCounter { get; private set; }
        
        public User(string fullName, string address, string IBAN) 
            : base(fullName, address)
        {
            this.IBAN = IBAN;
            IncomingCounter = 0;
            OutgoingCounter = 0;
            Balance = 0;
        }

        public void AddBalance(decimal amount)
        {
            Balance = amount;
        }

        public void IncrementIncomingCounter()
        {
            ++IncomingCounter;
        }

        public void IncrementOutgoingCounter()
        {
            ++OutgoingCounter;
        }
    }
}
