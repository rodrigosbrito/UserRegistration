using FluentValidation;

namespace Application.AuthUser
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {           
            RuleFor(command => command.Login).NotEmpty().WithMessage("Login is empty.");
            RuleFor(command => command.Password).NotEmpty().WithMessage("Password is empty.");
        }
    }
}
