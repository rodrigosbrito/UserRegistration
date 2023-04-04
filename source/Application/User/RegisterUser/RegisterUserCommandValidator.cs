using FluentValidation;

namespace Application.User.RegisterUser
{
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name is empty.");
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Email is incorrect.");
            RuleFor(command => command.Login).NotEmpty().WithMessage("Login is empty.");
            RuleFor(command => command.Password).NotEmpty().WithMessage("Password is empty.");
        }
    }
}
