using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PickItEasy.Identity.Data;
using PickItEasy.Identity.Models;

namespace PickItEasy.Identity.Date
{
    public class AuthDbContext : IdentityDbContext<AppUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));

            builder.ApplyConfiguration(new AppUserConfiguration());
        }
    }
}
