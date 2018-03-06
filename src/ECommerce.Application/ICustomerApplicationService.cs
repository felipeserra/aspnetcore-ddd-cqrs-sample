using System;
using System.Threading.Tasks;

using ECommerce.ApplicationSerivce.Command;
using ECommerce.Domain;

namespace ECommerce.ApplicationSerivce
{
    public interface ICustomerApplicationService
    {
        Task<ApplicationResponse<Guid>> When(CreateCustomer command);

        Task<ApplicationResponse> When(UpdateProfile command);

        Task<ApplicationResponse> When(DeleteCustomer command);

        Task<ApplicationResponse> When(Deposit command);
    }
}
