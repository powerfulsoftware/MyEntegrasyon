using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyEntegrasyon.Data.Entities;
using System.Security.Claims;

namespace MyEntegrasyon.Service
{
    public sealed class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        public ClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user).ConfigureAwait(false);
            //identity.AddClaim(new Claim("ContactName", "John Smith"));
            //return identity;
            if (!identity.HasClaim(x => x.Type == JwtClaimTypes.Subject))
            {
                var sub = user.Id;
                identity.AddClaim(new Claim(JwtClaimTypes.Subject, sub.ToString()));
            }

            return identity;
        }



    }
}
