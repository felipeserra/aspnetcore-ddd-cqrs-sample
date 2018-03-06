using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using ECommerce.Application.Services;
using ECommerce.Contracts.Command.Account;
using ECommerce.Contracts.Queries;
using ECommerce.Contracts.Queries.Account;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Sample.Web.V1;

using Xunit;

namespace ECommerce.Web.UnitTests.V1
{
    public class AccountControllerTests
    {
        private readonly AccountsController _accountsController;
        private readonly Mock<IAccountService> accountService;
        public AccountControllerTests()
        {
            accountService = new Mock<IAccountService>();
            _accountsController = new AccountsController(accountService.Object);
        }

        public class Get : AccountControllerTests
        {
            [Fact]
            public async Task Empty_AccountId_Should_Return_BadRequest()
            {
                var result = (ObjectResult)await _accountsController.Get(Guid.Empty);
                result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            }

            [Fact]
            public async Task Set_AccountId_Should_Return_Ok_When_Exists()
            {
                var accountId = Guid.NewGuid();

                accountService.Setup(x => x.Get(accountId)).ReturnsAsync(new AccountDto(accountId,0));

                var result = (ObjectResult)await _accountsController.Get(accountId);

                result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            }

            [Fact]
            public async Task Set_AccountId_Should_Return_NotFound_When_Not_Exists()
            {
                var accountId = Guid.NewGuid();

                accountService.Setup(x => x.Get(accountId)).ReturnsAsync(new AccountDto(Guid.Empty,0));

                var result = (ObjectResult)await _accountsController.Get(accountId);

                result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            }
        }

        public class Deposit : AccountControllerTests
        {
            public async void Should_Return_BadRequest_If_Amount_Less_Than_Equal_Zero()
            {
                var accountId = Guid.NewGuid();
                var depositAccount = new DepositAccount(0);

                var result = (ObjectResult) await _accountsController.Deposit(accountId,depositAccount);

                result.StatusCode.Should().Be((int)HttpStatusCode.OK);

            }

            [Fact]
            public async Task Set_AccountId_Should_Return_Ok_When_Exists()
            {
                var accountId = Guid.NewGuid();

                accountService.Setup(x => x.Get(accountId)).ReturnsAsync(new AccountDto(accountId, 0));

                var result = (ObjectResult)await _accountsController.Get(accountId);

                result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            }
        }
    }
}
