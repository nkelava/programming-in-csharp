

namespace BankTransferSlip.Entities
{
    public class BaseUser
    {
        public string FullName { get; private set; }
        public string Address { get; private set; }

        public BaseUser(string fullName, string address)
        {
            FullName = fullName;
            Address = address;
        }
    }
}
