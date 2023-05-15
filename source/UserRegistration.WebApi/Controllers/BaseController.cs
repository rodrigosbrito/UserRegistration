using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserRegistration.WebApi.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
