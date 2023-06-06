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
                new ApiScope("GymbyWebAPI", "Web API")
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
                    ClientId = "gymby",
                    ClientName = "Gymby Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://localhost:3000/authentication/callback",
                        "http://localhost:3000/authentication/silent-callback",
                        "https://gymby-web.azurewebsites.net/authentication/callback",
                        "https://gymby-web.azurewebsites.net/authentication/silent-callback"

                    },
                    AllowedCorsOrigins =
                    {   
                        "http://localhost:3000",
                        "https://gymby-web.azurewebsites.net"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:3000/",
                        "https://gymby-web.azurewebsites.net/"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "GymbyWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true
                },
                new Client()
                {
                    ClientId = "gymby-m",
                    ClientName = "Gymby Mobile",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "gymby://gymby.com/redirect",
                        "http://localhost:4000/"
                    },

                    PostLogoutRedirectUris =
                    {
                        "gymby://gymby.com/logout_redirect",
                        "http://localhost:4000/"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "GymbyWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true
                    
                },
                new Client
                {
                    ClientId = "test",
                    ClientName = "testUI",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowAccessTokensViaBrowser = true,

                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "GymbyWebAPI"
                    }
                },
            };
    }
}
    