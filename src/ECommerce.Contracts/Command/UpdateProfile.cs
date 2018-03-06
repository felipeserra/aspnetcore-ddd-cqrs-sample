using System;

namespace ECommerce.Contracts.Command
{
    public class UpdateProfile
    {
        public string Email { get; }
        public string Firstname { get; }
        public string Lastname { get; }

        public UpdateProfile(string email, string firstname, string lastname)
        {
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Firstname))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Lastname))
            {
                return false;
            }

            return true;
        }

   

    }
}
