using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class ProfileUserClaimsPrincipalFactory:UserClaimsPrincipalFactory<Profile>
    {
        public ProfileUserClaimsPrincipalFactory(UserManager<Profile> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Profile user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FavouriteColor", user.FavouriteColor));
            return identity;
        }
    }
}
