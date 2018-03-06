using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.AggregateRoot
{
    public abstract class AggregateRoot<T>  : IAggregateRoot
    {
        public T Id { get; protected set; }
    }

    public interface IAggregateRoot
    {
    }
}
