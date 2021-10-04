using Application.Accounts.Commands.CreateAccount;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AccountsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
