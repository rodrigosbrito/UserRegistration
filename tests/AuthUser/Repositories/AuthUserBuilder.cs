using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Builders;

namespace Test.AuthUser.Repositories
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

        public Domain.Entities.AuthUser Build()
            => new Domain.Entities.AuthUser(_login, _password, _user);
    }
}
