using System;

using ECommerce.Domain.AggregateRoot;

namespace ECommerce.Domain.Builder
{
    public class CustomerBuilder
    {
        private string _email;
        private string _firstname;
        private string _lastname;
        private double _balance;

        public CustomerBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CustomerBuilder WithFirstname(string firstname)
        {
            _firstname = firstname;
            return this;
        }

        public CustomerBuilder WithLastname(string lastname)
        {
            _lastname = lastname;
            return this;
        }

        public CustomerBuilder WithBalance(double balance)
        {
            _balance = balance;
            return this;
        }

        public Customer BuildCreateCustomerWithAccount()
        {
            if (string.IsNullOrEmpty(_email))
            {
                return null; 
            }

            if (string.IsNullOrEmpty(_lastname))
            {
                return null; 
            }

            if (string.IsNullOrEmpty(_firstname))
            {
                return null; 
            }
            var account = new Account(_balance);

            return new Customer(_email, _firstname, _lastname, account);
        }

        public Customer BuildCreateCustomer()
        {
            if (string.IsNullOrEmpty(_email))
            {
                return null;
            }

            if (string.IsNullOrEmpty(_lastname))
            {
                return null;
            }

            if (string.IsNullOrEmpty(_firstname))
            {
                return null;
            }
            var account = new Account(_balance);

            return new Customer(_email, _firstname, _lastname, null);
        }

        public Customer BuildGetCustomer(Guid customerId)
        {
            if (string.IsNullOrEmpty(_email))
            {
                return null;
            }

            if (string.IsNullOrEmpty(_lastname))
            {
                return null;
            }

            if (string.IsNullOrEmpty(_firstname))
            {
                return null;
            }

            return new Customer(customerId,_email, _firstname, _lastname, null);
        }

        public Customer BuildGetCustomerWithAccount(Guid customerId)
        {
            if (string.IsNullOrEmpty(_email))
            {
                return null;
            }

            if (string.IsNullOrEmpty(_lastname))
            {
                return null;
            }

            if (string.IsNullOrEmpty(_firstname))
            {
                return null;
            }
            var account = new Account(_balance);

            return new Customer(customerId, _email, _firstname, _lastname, account);
        }
    }
}
