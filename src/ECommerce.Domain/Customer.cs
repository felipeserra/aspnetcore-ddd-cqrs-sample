using System;

namespace ECommerce.Domain.AggregateRoot
{
    public sealed class Customer : AggregateRoot<Guid>
    {
        public string Email { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }

        public Account Account { get; private set; }

         internal Customer(Guid customerId, string email, string firstname, string lastname,Account account)
         {
            Id = customerId;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Account = account;
        }

        internal Customer(string email, string firstname, string lastname, Account account)
        {
            Id = Guid.NewGuid();
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Account = account;
        }

        public void UpdateProfile(string email, string firstname, string lastname)
        {
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
        }
    }

    
}
