using UserRegistration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Test.Builders;

namespace UserRegistration.Test.AuthUser.Repositories
{
    public sealed class AuthUserBuilder
    {
        private string _login = "defaultlogin";
        private string _password = "defaultpassword";

        private User _user = new UserBuilder().Build();

        public AuthUserBuilder WithCustom(string login, string password, User user)
        {
            _login = login;
            _password = password;
            _user = user;
            return this;
        }

        public UserRegistration.Domain.Entities.AuthUser Build()
            => new UserRegistration.Domain.Entities.AuthUser(_login, _password, _user);
    }
}
