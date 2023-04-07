using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;

namespace Gymby.Identity.Configurations
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("GymbyWebApi", "Web API")
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("GymbyWebAPI","Web API", new[]
                {
                    JwtClaimTypes.Name
                })
                {
                    Scopes = {"GymbyWebAPI"}
                }
            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client()
                {
                    ClientId = "gymby-web-api",
                    ClientName = "Gymby Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://.../signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http:// ..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://.../signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "GymbyWebAPI"

                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
