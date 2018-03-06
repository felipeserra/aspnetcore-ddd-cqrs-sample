using System;
using System.Threading.Tasks;

using ECommerce.Queries;

namespace ECommerce.Infrastructure.DataAccess.Customer.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Guid> Create(Domain.AggregateRoot.Customer customer);
        Task Update(Domain.AggregateRoot.Customer customer);
        Task<Domain.AggregateRoot.Customer> Get(Guid id);
        Task Delete(Guid id);
    }
}
