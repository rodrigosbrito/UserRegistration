using Application.User.RegisterUser;
using Domain.Model;
using Domain.Shared;
using Infrastructure.User;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.User.Get
{
    public sealed class GetUserQueryHandler
        : IRequestHandler<GetUserQuery, Result<UserModel>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task<Result<UserModel>> Handle(GetUserQuery request
            , CancellationToken cancellationToken)
        {
            var validationResult = new GetUserQueryValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<UserModel>.Failure(errors);
            }
            
            var user = await _userRepository.GetAsync(request.Id, cancellationToken);

            return Result<UserModel>.Success(user);
        }
    }
}
