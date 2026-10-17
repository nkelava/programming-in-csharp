using System;


namespace BankTransferSlip.Exceptions
{
    public class CallNumberFormatException : Exception
    {
        public CallNumberFormatException()
        {
        }

        public CallNumberFormatException(string message)
            : base(message)
        {
        }
    }
}
