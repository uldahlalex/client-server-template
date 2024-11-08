using System.Security.Claims;
using DataAccess.Entities;

namespace Service.Security;

public static class ClaimExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }

    public static IEnumerable<Claim> ToClaims(this User user, IEnumerable<string> roles)
    {
        return
        [
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.NameIdentifier, user.Id),
            .. roles.Select(role => new Claim(ClaimTypes.Role, role))
        ];
    }

    public static IEnumerable<Claim> ToClaims(this User user, params string[] roles)
    {
        return ToClaims(user, roles.AsEnumerable());
    }

    public static ClaimsPrincipal ToPrincipal(this User user, IEnumerable<string> roles)
    {
        return new ClaimsPrincipal(new ClaimsIdentity(user.ToClaims(roles)));
    }

    public static ClaimsPrincipal ToPrincipal(this User user, params string[] roles)
    {
        return new ClaimsPrincipal(new ClaimsIdentity(user.ToClaims(roles.AsEnumerable())));
    }
}