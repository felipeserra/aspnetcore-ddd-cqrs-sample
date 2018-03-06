namespace ECommerce.ApplicationSerivce.Command
{
    public class CreateCustomer : ICommand
    {
        public CreateCustomer(string email, string firstname, string lastname, double balance)
        {
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Balance = balance;
        }

        public string Email { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public double Balance { get; }
    }
}
