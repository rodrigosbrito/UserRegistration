﻿using Domain.Shared;
using MediatR;

namespace Application.User.RegisterUser
{
    public sealed record RegisterUserCommand(string Name, string Email, string Login, string Password) : IRequest<Result<Guid>>;
}
