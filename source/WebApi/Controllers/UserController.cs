using Application.User.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public sealed class UserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(new { userId = result.Value });

            return BadRequest(result.Errors);
        }
    }
}
