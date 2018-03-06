namespace ECommerce.ApplicationSerivce
{
    interface ICommand<T> : ICommand
    {
        T Id { get;   }
    }

    interface ICommand 
    {
        
    }
}
