using System;
using System.Linq;
using System.Text.RegularExpressions;
using BankTransferSlip.Exceptions;

namespace BankTransferSlip.Entities
{
    class Slip
    {
        private const string _ibanPattern = @"^([a-zA-Z]{2})([0-9]{19})$";
        private const string _ibanFormat = "An IBAN consists of 2 letters and 19 digits.\nE.g. HR0123456789123456789";

        public User Payer { get; private set; }
        public User Recipient { get; private set; }
        public bool Urgent { get; private set; }
        public string Currency { get; private set; }
        public decimal Amount { get; private set; } 
        public string  Model { get; private set; }
        public string ReferenceNumber { get; private set; }

        public Slip(string payerFullName, string payerAddress, 
                    string recipientFullName, string recipientAddress,
                    bool urgent, string currency,
                    decimal amount, string payerIBAN, string recipientIBAN, 
                    string model, string referenceNumber)
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
            ReferenceNumber = referenceNumber;
        }

        public void CheckFields()
        {
            if (!Regex.IsMatch(this.Currency, @"^[a-zA-Z]+$"))
            {
                throw new CurrencyFormatException("Currency format error.\n\nThe currency should consist of 3 letters, e.g. EUR.");
            }
            if (!Regex.IsMatch(this.Payer.IBAN, _ibanPattern))
            {
                throw new IBANFormatException($"Payer IBAN format error.\n\n{_ibanFormat}");
            }
            if (!Regex.IsMatch(this.Recipient.IBAN, _ibanPattern))
            {
                throw new IBANFormatException($"Recipient IBAN format error.\n\n{_ibanFormat}");
            }
            if (!Regex.IsMatch(this.Model, @"^([a-zA-Z]{2})([0-9]{2})$"))
            {
                throw new ModelFormatException("Model format error.\n\nThe model consists of 2 letters and 2 digits.\nE.g. HR00");
            }

            int digitCount = ReferenceNumber.Count(c => Char.IsDigit(c));
            int hyphenCount = ReferenceNumber.Count(c => c == '-');
            
            if (digitCount != (22 - hyphenCount) || hyphenCount < 0 || hyphenCount > 2)
            {
                throw new ReferenceNumberFormatException("Reference number format error.\n\n" +
                    "The reference number consists of 22 characters: up to 2 hyphens ('-') and digits for the rest.\n" +
                    "E.g. 0123456789-01234-56789 or 01234567891-0123456789 or 0123456789012345678901");
            }
        }
    }
}
