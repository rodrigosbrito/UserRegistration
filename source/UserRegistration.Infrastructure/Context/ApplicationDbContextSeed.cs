using UserRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserRegistration.Infrastructure.Context
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(this ModelBuilder builder) => builder.SeedUsers();

        private static void SeedUsers(this ModelBuilder builder)
        {
            var idUser = Guid.NewGuid();
            builder.Entity<UserRegistration.Domain.Entities.User>(entity => entity.HasData(new
            {
                Id = idUser,
                Name = "Administrator",
                Email = "administrator@administrator.com"
            }));

            builder.Entity<UserRegistration.Domain.Entities.AuthUser>(entity => entity.HasData(new
            {
                Id = Guid.NewGuid(),
                Login = "admin",
                Password = "0/Wuq+GXbdWaYjWczfhh+eGp44gr55gHwk5eSn/PkOaFSbVa73zSnQBKwLA6ZLipxylj2F2bq5hcjtoNT0tQn1r8ep9opGCaO+GW9Ja8tIhYR/GI3etahHJ25iQiF1v+dC6orTjVBJToWQCxrS9WnCKe3YLsQjuAhO3dN8Mw4eUiJ2DFsYgGunUWOIM4ZrWtbXurtHlaWW2fv5AgmZZ9uBP/O3oQSvbqOwtN7P7/9d07K/Gxxpe6kK/joTD+OSgeN/RctLhlk9Oc+dgQF+oTeT3RPmtyw1Ir3M8aJh2sK0AIbqw8ccsfRfIGWJ4h/YhtXsLBLv1qWR6e3WeJaxw79lTvBHRrxgnkHcokpnNtlNnlGpBgkw5TLbM76fQaiHOU8pAu0yHGTYwlcreA6p5f0c9pPmq+tpPRuhnCaO+2aaoNQRmx3thZRHA1/8AYcNdEWJ1xryBNara3jhPh6o+UNCYu5Vx6a942LqeJ21VUKeQdo39JXZJxopQuPnS/Dsdl7fsOQkECkOYUPZDJUJ3iP2Io4vZSt4ThbZIllw4gzaV+OOsCmVeZ4/JhmvDWVG/IxYueaIHpBvHoNqz4gUFtpSSv37+dxlh+rVbXtDdSVNOe3ART2U6zcMQoJfBBc9w8OBFXB/fuCcUKpjGu2Z/yjm6Nw8hq63k3VfR3DbZqtVw=",
                Salt = new Guid("f9e35b06-d292-4546-aabc-0aa33121b4ec"),
                Roles = Roles.User | Roles.Admin,
                UserId = idUser
            }));
        }
    }
}
