using System;
using System.Linq;
using System.Text.RegularExpressions;
using BankTransferSlip.Exceptions;

namespace BankTransferSlip.Entities
{
    class Slip
    {
        public User Payer { get; private set; }
        public User Recipient { get; private set; }
        public bool Urgent { get; private set; }
        public string Currency { get; private set; }
        public decimal Amount { get; private set; } 
        public string  Model { get; private set; }
        public string CallNumber { get; private set; }

        public Slip(string payerFullName, string payerAddress, 
                    string recipientFullName, string recipientAddress,
                    bool urgent, string currency,
                    decimal amount, string payerIBAN, string recipientIBAN, 
                    string model, string callNumber)
        {
            Payer = new User(payerFullName, payerAddress, payerIBAN);
            Payer.IncrementIncomingCounter();

            Recipient = new User(recipientFullName, recipientAddress, recipientIBAN);
            Recipient.IncrementOutgoingCounter();
            Recipient.AddBalance(amount);

            Urgent = urgent;
            Currency = currency;
            Amount = amount;
            Model = model;
            CallNumber = callNumber;
        }

        public void CheckFields()
        {
            if (!Regex.IsMatch(this.Currency, @"^[a-zA-Z]+$"))
            {
                throw new CurrencyFormatException("Currency format error.\n\nCurrency should consist of 3 letters eg. EUR.");
            }
            if (!Regex.IsMatch(this.Recipient.IBAN, @"^([a-zA-Z]{2})([0-9]{19})$"))
            {
                throw new IBANFormatException("IBAN format error.\n\nIBAN consists of 2 letters and 19 digits.\nEg. HR0123456789123456789");
            }
            if (!Regex.IsMatch(this.Model, @"^([a-zA-Z]{2})([0-9]{2})$"))
            {
                throw new ModelFormatException("Model format error.\n\nModel consists of 2 letters and 2 digits.\nEg. HR00");
            }

            int digitCount = CallNumber.Count(c => Char.IsDigit(c));
            int hyphenCount = CallNumber.Count(c => c == '-');
            
            if (digitCount != (22 - hyphenCount) || hyphenCount < 0 || hyphenCount > 2)
            {
                throw new CallNumberFormatException("Call number format exception.\n\n" +
                    "Call number consists of 22 characters: up to 2 '-' and up to 22 digits(depending on '-'.\n" +
                    "Eg. 0123456789-01234-56789 or 01234567891-0123456789 or 0123456789012345678901");
            }
        }
    }
}
