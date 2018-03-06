using System;

namespace ECommerce.Domain
{
    public sealed class Account : Entity<Guid>
    {
        public double Balance { get; private set; }

        internal Account(double balance)
        {
            Id = Guid.NewGuid();
            Balance = balance;
        }

        internal Account(Guid accountId, double balance)
        {
            Id = accountId;
            Balance = balance;
        }

        private void UpdateBalance(double balance)
        {
            Balance = balance;
        }

        public void Withdraw(double amount)
        {
            if (Balance < amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "insufficient funds");
            }

            var newBalance = Balance - amount;
            UpdateBalance(newBalance);
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "can't deposit with less than or zero funds");
            }

            UpdateBalance(Balance + amount);
        }

    }
}
