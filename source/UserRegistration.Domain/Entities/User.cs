namespace UserRegistration.Domain.Entities
{
    public sealed class User : Entity
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public User(Guid id) => Id = id;
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public string Email { get; private set; }

        public void UpdateName(string name) => Name = name;

        public void UpdateEmail(string email) => Email = email;
    }
}
