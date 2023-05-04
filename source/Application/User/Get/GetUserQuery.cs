using Domain.Model;
using Domain.Shared;
using MediatR;

namespace Application.User.Get
{
    public sealed record GetUserQuery(Guid Id) : IRequest<Result<UserModel>>;
}
