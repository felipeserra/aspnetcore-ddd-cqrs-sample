using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.ApplicationSerivce.Command
{
    public class DeleteCustomer : ICommand<Guid>
    {
        public DeleteCustomer(Guid customerId)
        {
            Id = customerId;
        }
        public Guid Id { get; }
    }
}
