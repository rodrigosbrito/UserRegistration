using Domain.Shared;
using Infrastructure.AuthUser;
using Infrastructure.Jwt;
using Infrastructure.Security;
using MediatR;

namespace Application.AuthUser
{
    public sealed class LoginCommandHandler
        : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IAuthUserRepository _authRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly ICryptographyService _cryptographyService;

        public LoginCommandHandler(IAuthUserRepository authRepository, IJwtProvider jwtProvider, ICryptographyService cryptographyService)
        {
            _authRepository = authRepository;
            _jwtProvider = jwtProvider;
            _cryptographyService = cryptographyService;
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

            var hashedPassword = _cryptographyService.GenerateHashPassword(command.Password, authUser.Salt.ToString());

            var isValidPassword = authUser.Password == hashedPassword;

            if (!isValidPassword) { return Result<string>.Failure("Invalid Credentials."); }

            var token = _jwtProvider.Generate(authUser);

            return Result<string>.Success(token);
        }
    }
}
