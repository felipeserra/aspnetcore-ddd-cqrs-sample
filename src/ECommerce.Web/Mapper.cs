
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sample.Web
{
    public static class Mapper
    {
        public static UpdateProfileCommand ToDomain(this UpdateProfile contract, Guid customerId)
        {
            return new UpdateProfileCommand(customerId, contract.Email, contract.Firstname, contract.Lastname);
        }

        public static CreateCustomerCommand ToDomain(this CreateCustomer contract)
        {
            return new CreateCustomerCommand(contract.Email, contract.Firstname, contract.Lastname,contract.Balance);
        }

        public static DepositCommand ToDomain(this Deposit contract, Guid accountId)
        {
            return new DepositCommand(accountId, contract.Amount);
        }
    }
}
