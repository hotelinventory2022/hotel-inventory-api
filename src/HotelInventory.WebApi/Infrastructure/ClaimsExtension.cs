using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelInventory.WebApi.Infrastructure
{
    public static class ClaimsExtension
    {
        public static int GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static int GetLoggedInUserRoleId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return int.Parse(principal.FindFirstValue(ClaimTypes.Role));
        }

        public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
