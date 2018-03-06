using System;
using System.Threading.Tasks;

using ECommerce.ApplicationSerivce.Command;
using ECommerce.Domain;
using ECommerce.Domain.Builder;
using ECommerce.Infrastructure.DataAccess.Customer.Interfaces;

namespace ECommerce.ApplicationSerivce
{
    public class CustomerApplicationApplicationService : ICustomerApplicationService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerApplicationApplicationService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
      

        public async Task<ApplicationResponse<Guid>> When(CreateCustomer command)
        {
            var customer = new CustomerBuilder().WithEmail(command.Email)
                                                .WithFirstname(command.Firstname)
                                                .WithLastname(command.Lastname)
                                                .WithBalance(command.Balance)
                                                .BuildCreateCustomer();
            if (customer == null)
            {
                return ApplicationResponse<Guid>.Fail(StatusCode.BadRequest, "could not create create customer");
            }

            await _customerRepository.Create(customer);

            return ApplicationResponse<Guid>.Success(customer.Id);
        }

        public async Task<ApplicationResponse> When(Deposit command)
        {
            var customer = await _customerRepository.Get(command.Id);

            if (customer == null)
            {
                return ApplicationResponse.Fail(StatusCode.NotFound, "Customer not found");
            }

            customer.Account.Deposit(command.Balance);

            await _customerRepository.Update(customer);

            return ApplicationResponse.Success();
        }

        public async Task<ApplicationResponse> When(UpdateProfile command)
        {
            var customer = await _customerRepository.Get(command.Id);

            if (customer == null)
            {
                return ApplicationResponse.Fail(StatusCode.NotFound, "Customer not found");
            }

            customer.UpdateProfile(command.Email, command.Firstname, command.Lastname);

            await _customerRepository.Update(customer);

            return ApplicationResponse.Success();
        }

        public async Task<ApplicationResponse> When(DeleteCustomer command)
        {
            var customer = await _customerRepository.Get(command.Id);

            if (customer == null)
            {
                return ApplicationResponse.Fail(StatusCode.NotFound, "Customer not found");
            }

            await _customerRepository.Delete(customer.Id);

            return ApplicationResponse.Success();
        }
    }
}
