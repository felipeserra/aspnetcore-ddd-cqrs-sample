using System;

using ECommerce.Domain;

namespace ECommerce.Queries
{
    public class CustomerQueryReponse
    {
        public Guid Id { get; set; }
        public Account Account { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
