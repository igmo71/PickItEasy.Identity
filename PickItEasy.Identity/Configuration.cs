using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace PickItEasy.Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScoopes => new List<ApiScope>
        {
            new ApiScope("PickItEasyWebApi", "PickItEasy Web API")
        };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("PickItEasyWebApi", "PickItEasy Web API", new[] { JwtClaimTypes.Name })
            {
                Scopes = { "PickItEasyWebApi" }
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client()
            {
                ClientId = "pickiteasy-web-api",
                ClientName = "PickItEasyWebApi",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://localhost:3000/signin-oidc"
                },
                PostLogoutRedirectUris =
                {
                    "http://localhost:3000/signout-oidc"
                },
                AllowedCorsOrigins =
                {
                    "http://localhost:3000"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId, 
                    IdentityServerConstants.StandardScopes.Profile,
                    "PickItEasyWebApi"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
    }
}
