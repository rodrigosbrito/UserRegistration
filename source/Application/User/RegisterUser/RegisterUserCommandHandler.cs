using Domain.Interfaces;
using Domain.Shared;
using Infrastructure.AuthUser;
using Infrastructure.Security;
using Infrastructure.User;
using MediatR;

namespace Application.User.RegisterUser
{
    public sealed class RegisterUserCommandHandler 
        : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IAuthUserRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptographyService _cryptographyService;

        public RegisterUserCommandHandler(IAuthUserRepository authRepository, 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork,
            ICryptographyService cryptographyService)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _cryptographyService = cryptographyService;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var validationResult = new RegisterUserCommandValidator().Validate(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Guid>.Failure(errors);
            }

            if (await _userRepository.EmailExistsAsync(command.Email, cancellationToken)) return Result<Guid>.Failure("EmailExists");

            if (await _authRepository.LoginExistsAsync(command.Login, cancellationToken)) return Result<Guid>.Failure("LoginExists");

            var user = new Domain.Entities.User(command.Name, command.Email);

            var authUser = new Domain.Entities.AuthUser(command.Login, command.Password, user);

            var passwordSalt = _cryptographyService.GenerateHashPassword(authUser.Password, authUser.Salt.ToString());
            authUser.UpdatePassword(passwordSalt);
            
            await _userRepository.AddAsync(user, cancellationToken);

            await _authRepository.AddAsync(authUser, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(user.Id);

        }
    }
}
