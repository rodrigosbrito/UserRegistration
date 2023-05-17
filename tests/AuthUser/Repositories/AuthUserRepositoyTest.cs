using Microsoft.EntityFrameworkCore;
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
                .Build("AuthUserRepositoyTest");

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
        public async Task GetByEmailOrLogin_NonExistingEmail_ReturnsNull()
        {
            var result = await _authUserRepository.GetByEmailOrLogin("nonexistingemail@example.com", new CancellationToken());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByEmailOrLogin_NonExistingLogin_ReturnsNull()
        {
            var result = await _authUserRepository.GetByEmailOrLogin("nonexistinglogin", new CancellationToken());

            Assert.Null(result);
        }

        [Fact]
        public async Task LoginExists_ExistingLogin_ReturnsTrue()
        {
            var result = await _authUserRepository.LoginExistsAsync("existinglogin", new CancellationToken());

            Assert.True(result);
        }

        [Fact]
        public async Task LoginExists_NonExistingLoginAndEmail_ReturnsFalse()
        {
            var result = await _authUserRepository.LoginExistsAsync("nonexistinglogin", new CancellationToken());

            Assert.False(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAuthUserToContext()
        {
            // Arrange
            var userToAdd = new UserBuilder()
                .WithCustom("loginToAddTest", "logintoaddtest@example.com")
                .Build();

            var authUserToAdd = new AuthUserBuilder()
                .WithCustom("loginToAddTest", "testing", userToAdd)
                .Build();

            // Act
            await _authUserRepository.AddAsync(authUserToAdd, new CancellationToken());
            await _context.SaveChangesAsync();

            // Assert
            var addedAuthUser = await _context.AuthUsers.FirstOrDefaultAsync(x => x.Login == "loginToAddTest");
            Assert.Equal(authUserToAdd, addedAuthUser);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowExceptionWhenUserIsNull()
        {
            // Arrange
            Domain.Entities.AuthUser authUser = null;

            // Act e Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _authUserRepository.AddAsync(authUser, new CancellationToken()));
        }

        public async void Dispose() => await _context.Database.EnsureDeletedAsync();
    }
}
