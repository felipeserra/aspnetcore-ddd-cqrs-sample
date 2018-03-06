using System;
using System.Threading.Tasks;

using ECommerce.Domain;

namespace ECommerce.Queries
{
    public interface ICustomerQueryService
    {
        Task<ApplicationResponse<CustomerQueryReponse>> GetCustomer(Guid customerId);
    }
}
