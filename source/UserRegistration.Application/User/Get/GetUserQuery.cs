using UserRegistration.Domain.Model;
using UserRegistration.Domain.Shared;
using MediatR;

namespace UserRegistration.Application.User.Get
{
    public sealed record GetUserQuery(Guid Id) : IRequest<Result<UserModel>>;
}
