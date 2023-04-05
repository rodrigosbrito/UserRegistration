using Domain.Shared;
using MediatR;

namespace Application.AuthUser
{
    public sealed record LoginCommand(string Login, string Password) : IRequest<Result<string>>;
}