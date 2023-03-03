using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PickItEasy.Identity.Data;
using PickItEasy.Identity.Date;
using PickItEasy.Identity.Models;

namespace PickItEasy.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetValue<string>("DbConnection");

            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiScopes(Configuration.ApiScoopes)
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "PickItEasy.Identity.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            builder.Services.AddControllersWithViews();
                        
            var app = builder.Build();

            InitializeDb(app);
                        
            app.UseRouting();
            
            app.UseIdentityServer();
            // https://localhost:32768/.well-known/openid-configuration - in Dockr run
            // https://localhost:7109/.well-known/openid-configuration - in https run

            //app.MapGet("/", () => "Hello World!");
            app.MapDefaultControllerRoute();

            app.Run();
        }

        private static void InitializeDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var dbContext = serviceProvider.GetRequiredService<AuthDbContext>();
                DbInitializer.Initialize(dbContext);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error ocured while db initialize.");
                throw;
            }
        }
    }
}