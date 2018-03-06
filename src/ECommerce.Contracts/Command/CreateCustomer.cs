namespace ECommerce.Contracts.Command
{
    public class CreateCustomer
    {
        public CreateCustomer(double balance, string lastname, string firstname, string email)
        {
            Balance = balance;
            Lastname = lastname;
            Firstname = firstname;
            Email = email;
        }

        public string Email { get;  }
        public string Firstname { get; }
        public string Lastname { get; }
        public double Balance { get;  }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Firstname))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Lastname))
            {
                return false;
            }
            if (Balance <= 0)
            {
                return false;
            }


            return true;
        }
    }
}
