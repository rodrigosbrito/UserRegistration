namespace UserRegistration.Test.Builders
{
    public sealed class UserBuilder
    {
        private string _name = "defaultname";
        private string _email = "defaultemail@example.com";

        public UserBuilder WithCustom(string name, string email)
        {
            _name = name;
            _email = email;
            return this;
        }

        public Domain.Entities.User Build()
            => new Domain.Entities.User(_name, _email);
    }
}
