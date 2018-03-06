namespace ECommerce.Infrastructure.Repositories.sql
{
    public class CustomerRepositorySql
    {

        //Load the account too
        public const string GetCustomer = @"
                            SELECT [Id],[AccountId],[Email],[Firstname],[Lastname]
                            FROM tCustomer c
                            JOIN tAccount a on c.id = a.Id 
                            WHERE [Id] = @id";


        public const string Update = @"
                            UPDATE tCustomer
                            SET [Email] = @email, [Firstname] = @firstname, [Lastname] = @lastname
                            WHERE [Id]= @id";
        //We need logic here to update the account too

        public const string Create = @"
                            INSERT INTO tCustomer ([Id],[AccountId],[Email],[Firstname],[Lastname])
                            VALUES(@id,@accountId,@email,@firstname,@lastname)

                            INSERT INTO tAccount ([Id],[Balance])
                            VALUES(@accountId,@balance)";

        //We need to delete the account here too
        public const string Delete = @"
                            DELETE FROM tCustomer
                            WHERE [Id] = @id";
    }
}
