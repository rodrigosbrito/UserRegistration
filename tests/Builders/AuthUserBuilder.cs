namespace UserRegistration.Test.Builders
{
    public sealed class AuthUserBuilder
    {
        private string _login = "defaultlogin";
        private string _password = "defaultpassword";

        private Domain.Entities.User _user = new UserBuilder().Build();

        public AuthUserBuilder WithCustom(string login, string password, Domain.Entities.User user)
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
