using UserRegistration.Infrastructure.AuthUser;
using UserRegistration.Infrastructure.Context;
using UserRegistration.Test.Builders;

namespace UserRegistration.Test.AuthUser.Repositories
{
    public class AuthUserRepositoyTest : IDisposable
    {
        private readonly IAuthUserRepository _authUserRepository;
        private readonly ApplicationDbContext _context;

        public AuthUserRepositoyTest()
        {
            var user = new UserBuilder()
                .WithCustom("existing", "existingemail@example.com")
                .Build();

            var authUser = new AuthUserBuilder()
                .WithCustom("existinglogin", "testing", user)
                .Build();

            _context = new InMemoryDbBuilder()
                .WithEntity(authUser)
                .Build();

            _authUserRepository = new AuthUserRepository(_context);
        }
        [Fact]
        public async Task GetByEmailOrLogin_ExistingLogin_ReturnsAuthUser()
        {
            var result = await _authUserRepository.GetByEmailOrLogin("existinglogin", new CancellationToken());

            Assert.NotNull(result);
            Assert.Equal("existinglogin", result.Login);
        }

        [Fact]
        public async Task GetByEmailOrLogin_ExistingEmail_ReturnsAuthUser()
        {
            var result = await _authUserRepository.GetByEmailOrLogin("existingemail@example.com", new CancellationToken());

            Assert.NotNull(result);
            Assert.Equal("existingemail@example.com", result.User.Email);
        }

        [Fact]
        public async Task GetByEmailOrLogin_NonExistingLoginAndEmail_ReturnsNull()
        {
            var result = await _authUserRepository.GetByEmailOrLogin("nonexistinglogin", new CancellationToken());

            Assert.Null(result);
        }

        public void Dispose() => _context.Database.EnsureDeleted();
    }
}
