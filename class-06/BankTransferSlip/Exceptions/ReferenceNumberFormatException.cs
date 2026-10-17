using System;


namespace BankTransferSlip.Exceptions
{
    public class ReferenceNumberFormatException : Exception
    {
        public ReferenceNumberFormatException()
        {
        }

        public ReferenceNumberFormatException(string message)
            : base(message)
        {
        }
    }
}
