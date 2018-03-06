using System;
using System.Threading.Tasks;

using ECommerce.Domain;

namespace ECommerce.Queries
{
    public interface IAccountQueryService
    {
        Task<ApplicationResponse<BalanceQueryResponse>> GetAccount(Guid accountId);
    }
}
