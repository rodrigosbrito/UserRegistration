using Application.Jwt;
using Application.User.RegisterUser;
using Domain.Interfaces;
using Domain.Shared;
using Infrastructure.AuthUser;
using Infrastructure.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AuthUser
{
    public sealed class LoginCommandHandler
        : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IAuthUserRepository _authRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IAuthUserRepository authRepository, IJwtProvider jwtProvider)
        {
            _authRepository = authRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(LoginCommand command
            , CancellationToken cancellationToken)
        {
            var validationResult = new LoginCommandValidator().Validate(command);
            
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<string>.Failure(errors);
            }

            var authUser = await _authRepository.GetByEmailOrLogin(command.Login);

            if (authUser is null) { return Result<string>.Failure("Invalid Credentials."); }

            var hashedPassword = PasswordSaltHelper.CreatePasswordWithSalt(command.Password, authUser.Salt.ToString());

            var isValidPassword = authUser.Password == hashedPassword;

            if (!isValidPassword) { return Result<string>.Failure("Invalid Credentials."); }

            var token = _jwtProvider.Generate(authUser);

            return Result<string>.Success(token);
        }
    }
}
