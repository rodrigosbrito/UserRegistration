using FluentValidation;

namespace UserRegistration.Application.User.Get
{
    public sealed class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator() => RuleFor(request => request.Id).NotEmpty().WithMessage("Id is incorrect.");
    }
}
