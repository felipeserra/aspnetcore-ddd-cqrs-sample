namespace ECommerce.Contracts.Command
{
    public class Deposit
    {
        public Deposit(double amount)
        {
            Amount = amount;
        }
        public bool IsValid()
        {
            return !(Amount <= 0);
        }
        public double Amount { get;  }
    }
}
