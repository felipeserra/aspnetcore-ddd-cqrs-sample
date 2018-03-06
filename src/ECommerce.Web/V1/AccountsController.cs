using System;
using System.Net;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sample.Web.V1
{

    /// <summary>
    /// The account controller...
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/accounts/")]
    public class AccountsController : Controller
    {
        private readonly IAccountQueryService _accountQueryService;

        public AccountsController(IAccountQueryService accountQueryService)
        {
            _accountQueryService = accountQueryService;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AccountDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string),
            Description = "Invalid request. AccountId must be set")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string),
            Description = "AccountId not found")]
        public async Task<IActionResult> Get(Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                return BadRequest("accountId not valid");
            }

            var result = await _accountQueryService.Get(accountId);

            if (result.StatusCode == ECommerce.Shared.StatusCode.NotFound)
            {
                return NotFound("account not found");
            }

            return Ok(new AccountDto
            {
                Balance = result.Data.Balance,
                Id = result.Data.Id
            });
        }
    }
}
