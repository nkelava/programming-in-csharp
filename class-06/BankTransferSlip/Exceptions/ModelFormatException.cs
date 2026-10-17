using System;


namespace BankTransferSlip.Exceptions
{
    public class ModelFormatException : Exception
    {
        public ModelFormatException()
        {
        }

        public ModelFormatException(string message)
            : base (message)
        {
        }
    }
}
