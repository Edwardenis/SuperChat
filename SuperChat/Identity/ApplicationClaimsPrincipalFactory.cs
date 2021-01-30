using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SuperChat.Datamodel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperChat.Identity
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
        UserManager<AppUser> userManager
        , RoleManager<IdentityRole> roleManager
        , IOptions<IdentityOptions> optionsAccessor)
    : base(userManager, roleManager, optionsAccessor)
        { 
        }

        protected override Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            return base.GenerateClaimsAsync(user);
        }
        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
        new Claim(ClaimTypes.GivenName, user.Name)
    });
            }

            return principal;
        }

    }
}
