using Application.AuthUser;
using Application.User.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public sealed class UserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(new { userId = result.Value });

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(new { token = result.Value });

            return BadRequest(result.Errors);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            return true ? Ok() : NotFound();
        }
    }
}
