using System;


namespace BankTransferSlip
{
    public class CurrencyFormatException : Exception
    {
        public CurrencyFormatException()
        {
        }

        public CurrencyFormatException(string message)
            : base(message)
        {
        }
    }
}
