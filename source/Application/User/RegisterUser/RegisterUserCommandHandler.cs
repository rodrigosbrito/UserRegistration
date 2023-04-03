using Azure.Core;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
using Infrastructure.AuthUser;
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

        public RegisterUserCommandHandler(IAuthUserRepository authRepository
            , IUserRepository userRepository
            , IUnitOfWork unitOfWork)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        
        public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var validationResult = new RegisterUserCommandValidator().Validate(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Guid>.Failure("One or more errors.1");
            }

            if (!await _userRepository.EmailExistsAsync(command.Email)) return Result<Guid>.Failure("EmailExists");

            if (!await _authRepository.LoginExistsAsync(command.Login)) return Result<Guid>.Failure("LoginExists");

            var user = new Domain.Entities.User(command.Name, command.Email);

            var authUser = new AuthUser(command.Login, command.Password, user);

            var passwordSalt = PasswordSaltHelper.CreatePasswordWithSalt(authUser.Password, authUser.Salt.ToString());
            authUser.UpdatePassword(passwordSalt);
            
            await _userRepository.AddAsync(user);

            await _authRepository.AddAsync(authUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(user.Id);

        }
    }
}
