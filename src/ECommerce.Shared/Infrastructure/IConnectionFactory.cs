using System.Data;

namespace ECommerce.Shared.Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
