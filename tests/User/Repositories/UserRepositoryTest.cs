using Microsoft.EntityFrameworkCore;
using Moq;
using UserRegistration.Infrastructure.Context;
using UserRegistration.Infrastructure.User;
using UserRegistration.Test.Builders;

namespace UserRegistration.Test.User.Repositories
{
    public class UserRepositoryTest : IDisposable
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        private readonly Domain.Entities.User _userExisting;

        public UserRepositoryTest()
        {
            _userExisting = new UserBuilder()
                .WithCustom("existing", "existingemail@example.com")
                .Build();

            _context = new InMemoryDbBuilder()
                .WithEntity(_userExisting)
                .Build("UserRepositoryTest");

            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task GetByEmail_ExistingEmail_ReturnsUser()
        {
            var result = await _userRepository.GetByEmailAsync("existingemail@example.com", 
                new CancellationToken());

            Assert.NotNull(result);
            Assert.Equal("existingemail@example.com", result.Email);
        }

        [Fact]
        public async Task GetByEmail_NonExistingEmail_ReturnsNull()
        {
            var result = await _userRepository.GetByEmailAsync("nonexistingemail@example.com",
                new CancellationToken());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAsync_ExistingId_ReturnsUser()
        {
            // Act
            var result = await _userRepository.GetAsync(_userExisting.Id, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_userExisting.Id, result.Id);
            Assert.Equal(_userExisting.Email, result.Email);
        }

        [Fact]
        public async Task GetAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            var nonExistingUserId = Guid.NewGuid();

            // Act
            var result = await _userRepository.GetAsync(nonExistingUserId, new CancellationToken());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUserToContext()
        {
            // Arrange
            var userToAdd = new UserBuilder()
                .WithCustom("userToAddTest", "usertoaddtest@example.com")
                .Build();

            // Act
            await _userRepository.AddAsync(userToAdd, new CancellationToken());
            await _context.SaveChangesAsync();

            // Assert
            var addedUser = await _context.Users.FirstOrDefaultAsync(x => x.Name == "userToAddTest");
            Assert.Equal(userToAdd, addedUser);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowExceptionWhenUserIsNull()
        {
            // Arrange
            Domain.Entities.User user = null;

            // Act e Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userRepository.AddAsync(user, new CancellationToken()));
        }

        [Fact]
        public async Task EmailExists_ExistingEmail_ReturnsTrue()
        {
            var result = await _userRepository.EmailExistsAsync("existingemail@example.com", new CancellationToken());

            Assert.True(result);
        }

        [Fact]
        public async Task EmailExists_NonExistingEmail_ReturnsFalse()
        {
            var result = await _userRepository.EmailExistsAsync("nonexistingemail@example.com", new CancellationToken());

            Assert.False(result);
        }


        public async void Dispose() => await _context.Database.EnsureDeletedAsync();
    }
}
