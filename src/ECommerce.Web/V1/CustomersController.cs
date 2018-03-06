using System;
using System.Net;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sample.Web.V1
{
    /// <summary>
    ///     The customer controller...
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/customers/")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerQueryService _customerQueryService;

        public CustomersController(ICustomerService customerService, ICustomerQueryService customerQueryService)
        {
            _customerService = customerService;
            _customerQueryService = customerQueryService;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CustomerDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string), Description = "customerReference is not valid")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string), Description = "Customer reference not found")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return BadRequest("User input not valid");
            }

            var result = await _customerQueryService.Get(customerId);

            if (result.StatusCode == ECommerce.Shared.StatusCode.NotFound)
            {
                return NotFound("Customer reference not found");
            }

            return Ok(new CustomerDto
            {
                Lastname = result.Data.Lastname,
                Firstname = result.Data.Firstname,
                Email = result.Data.Email,
                AccountId = result.Data.AccountId,
                Id = result.Data.Id
            });
        }

        [HttpPut]
        [Route("{customerId}/update")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OkResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string), Description = "User input not valid")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string), Description = "customer reference not found")]
        public async Task<IActionResult> Update(Guid customerId, [FromBody] UpdateProfile updateProfile)
        {
            if (!updateProfile.IsValid() && customerId == Guid.Empty)
            {
                return BadRequest("User input not valid");
            }

            var result = await _customerService.Update(customerId, updateProfile.ToDomain(customerId));

            if (result.StatusCode == ECommerce.Shared.StatusCode.NotFound)
            {
                return NotFound("customer reference not found");
            }

            return Ok();
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string), Description = "Invalid request. Customer reference and email must be set")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string), Description = "customer reference not found")]
        public async Task<IActionResult> Post([FromBody] CreateCustomer command)
        {
            if (!command.IsValid())
            {
                return BadRequest("User input not valid");
            }

            var result = await _customerService.Create(command.ToDomain());

            if (result.StatusCode == ECommerce.Shared.StatusCode.NotFound)
            {
                return NotFound("customer reference not found");
            }

            return Ok(result.Data);
        }

        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OkResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string), Description = "User input not valid")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string), Description = "customer reference not found")]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return BadRequest("User input not valid");
            }

            var result = await _customerService.Delete(customerId);

            if (result.StatusCode == ECommerce.Shared.StatusCode.NotFound)
            {
                return NotFound("customer reference not found");
            }

            return Ok();
        }
    }
}
