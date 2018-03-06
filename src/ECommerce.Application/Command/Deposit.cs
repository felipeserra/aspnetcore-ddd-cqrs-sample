using System;

namespace ECommerce.ApplicationSerivce.Command
{
    public class Deposit : ICommand<Guid>
    {
        public Deposit(Guid id, double balance)
        {
            Id = id;
            Balance = balance;
        }

        public Guid Id { get; }
        public double Balance { get; }
    }
}
