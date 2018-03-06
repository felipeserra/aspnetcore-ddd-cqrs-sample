using System;
using System.Linq;
using System.Threading.Tasks;

using Dapper;

using ECommerce.Domain.AggregateRoot;
using ECommerce.Infrastructure.DataAccess.Customer.Interfaces;
using ECommerce.Infrastructure.Infrastructure;
using ECommerce.Infrastructure.Repositories.sql;
using ECommerce.Queries;

namespace ECommerce.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConnectionFactory _factory;

        public CustomerRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<Guid> Create(Domain.AggregateRoot.Customer customer)
        {

            var customerId = Guid.NewGuid();
            using (var db = _factory.GetConnection())
            {
                try
                {
                    await db.ExecuteAsync(CustomerRepositorySql.Create, new
                    {
                        Id = customerId,
                        AccountId = customer.Account.Id,
                        Email = customer.Email,
                        Firstname = customer.Firstname,
                        Lastname = customer.Lastname,
                        Balance = customer.Account.Balance,
                    });
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
              
            }
            return customerId;
        }

        public async Task Update(Domain.AggregateRoot.Customer customer)
        {
            using (var db = _factory.GetConnection())
            {
                await db.ExecuteAsync(CustomerRepositorySql.Update, new
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    Firstname = customer.Firstname,
                    Lastname = customer.Lastname
                });
            }
        }


        public async Task<Customer> Get(Guid id)
        {
            using (var db = _factory.GetConnection())
            {
                var response = await db.QueryAsync<Customer>(CustomerRepositorySql.GetCustomer, new { id = id });

                if (response.Count() > 1)
                {
                    throw new Exception("To many rows found");
                }

                return response.ElementAt(0);
            }
        }


       

        public async Task Delete(Guid id)
        {
            using (var db = _factory.GetConnection())
            {
                await db.ExecuteAsync(CustomerRepositorySql.Delete, new
                {
                    id = id
                });
            }
        }
    }
}
