using System;

namespace Sample.IntegrationTests
{
    public class RepositoryTestBase : IDisposable
    {
        //private readonly TransactionScope _transactionScope;

        protected RepositoryTestBase()
        {
            //_transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew); //supported in sqlclient preview  package
        }

        public void Dispose()
        {
            //_transactionScope.Dispose();
        }
    }
}
