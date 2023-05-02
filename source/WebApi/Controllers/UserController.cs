using Application.AuthUser;
using Application.User.Get;
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
        [Authorize]
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

            return Unauthorized(result.Errors);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserQuery(id), cancellationToken);

            if (result.IsSuccess)
                return Ok(new { user = result.Value });

            return BadRequest(result.Errors);
        }
    }
}
