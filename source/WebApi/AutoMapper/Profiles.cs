using Application.User.RegisterUser;
using AutoMapper;
using WebApi.Model;

namespace WebApi.AutoMapper
{
    public class RegisterModelToRegisterUserCommandProfile : Profile
    {
        public RegisterModelToRegisterUserCommandProfile()
        {
            CreateMap<RegisterUserModel, RegisterUserCommand>();
        }
    }
}
