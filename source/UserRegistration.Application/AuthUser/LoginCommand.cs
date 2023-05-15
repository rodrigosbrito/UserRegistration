using UserRegistration.Domain.Shared;
using MediatR;

namespace UserRegistration.Application.AuthUser
{
    public sealed record LoginCommand(string Login, string Password) : IRequest<Result<string>>;
}