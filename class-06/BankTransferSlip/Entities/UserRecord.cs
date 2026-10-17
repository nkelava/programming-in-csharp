
namespace BankTransferSlip.Entities
{
    public class UserRecord
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public int IncomingCounter { get; set; }
        public int OutgoingCounter { get; set; }
    }
}
