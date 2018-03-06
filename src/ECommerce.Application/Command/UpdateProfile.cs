using System;

namespace ECommerce.ApplicationSerivce.Command
{
    public class UpdateProfile : ICommand<Guid>
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Firstname { get; }
        public string Lastname { get; }


        public UpdateProfile(Guid id, string email, string firstname, string lastname)
        {
            Id = id;
            Lastname = lastname;
            Firstname = firstname;
            Email = email;
        }
    }
}
