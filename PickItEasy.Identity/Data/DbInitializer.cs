using PickItEasy.Identity.Date;

namespace PickItEasy.Identity.Data
{
    public class DbInitializer
    {
        public static void Initialize(AuthDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
