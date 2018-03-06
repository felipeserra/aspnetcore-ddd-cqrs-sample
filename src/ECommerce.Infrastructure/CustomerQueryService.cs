using ECommerce.Domain;
using ECommerce.Infrastructure.DataAccess.Customer.Interfaces;
using ECommerce.Queries;
using System;
using System.Threading.Tasks;

using ECommerce.Domain.AggregateRoot;

namespace ECommerce.Infrastructure
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerQueryService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<ApplicationResponse<CustomerQueryReponse>> GetCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
