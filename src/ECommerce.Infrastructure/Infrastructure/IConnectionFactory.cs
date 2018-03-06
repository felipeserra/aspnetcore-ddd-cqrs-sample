using System.Data;

namespace ECommerce.Infrastructure.Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
