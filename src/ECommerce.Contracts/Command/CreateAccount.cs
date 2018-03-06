namespace ECommerce.Contracts.Command
{
    public class CreateAccount
    {
        public float Balance { get; }

        public CreateAccount(float balance)
        {
            Balance = balance;
        }

        public bool IsValid()
        {
            if (Balance <= 0)
            {
                return false;
            }

            return true;

        }

    }
}
