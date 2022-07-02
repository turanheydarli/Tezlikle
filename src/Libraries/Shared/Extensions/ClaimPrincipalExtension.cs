using System.Security.Claims;

namespace Shared.Extensions;


public static class ClaimPrincipalExtensions
{
    public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType) 
    {
        List<string> result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();

        return result;
    }

    public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.Role);
    }

    public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
    {
        if (!claimsPrincipal.Identity.IsAuthenticated)
        {
            return "";
        }

        return claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}