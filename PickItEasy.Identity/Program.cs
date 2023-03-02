using IdentityServer4.Models;

namespace PickItEasy.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentityServer()
                .AddInMemoryApiResources(new List<ApiResource>())
                .AddInMemoryIdentityResources(new List<IdentityResource>())
                .AddInMemoryApiScopes(new List<ApiScope>())
                .AddInMemoryClients(new List<Client>())
                .AddDeveloperSigningCredential();

            var app = builder.Build();

            app.UseIdentityServer();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}