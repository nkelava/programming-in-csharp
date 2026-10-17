using System;


namespace BankTransferSlip
{
    public class IBANFormatException : Exception
    {
        public IBANFormatException()
        {
        }

        public IBANFormatException(string message)
            : base(message)
        {
        }
    }
}
