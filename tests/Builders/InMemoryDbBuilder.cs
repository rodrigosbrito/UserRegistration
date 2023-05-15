using UserRegistration.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace UserRegistration.Test.Builders
{
    public class InMemoryDbBuilder
    {
        private readonly List<object> _entities = new List<object>();

        public InMemoryDbBuilder WithEntity(object entity)
        {
            _entities.Add(entity);
            return this;
        }

        public ApplicationDbContext Build()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "UserRegistrationTest")
                .Options;

            var context = new ApplicationDbContext(options);

            foreach (var entity in _entities)
                context.Add(entity);

            context.SaveChanges();

            return context;
        }
    }
}
