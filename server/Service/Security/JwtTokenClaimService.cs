using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Service.Security;

public class JwtTokenClaimService : ITokenClaimsService
{
    public const string SignatureAlgorithm = SecurityAlgorithms.HmacSha512;

    private readonly AppOptions _options;
    private readonly UserManager<IdentityUser> _userManager;

    public JwtTokenClaimService(IOptions<AppOptions> options, UserManager<IdentityUser> userManager)
    {
        _options = options.Value;
        _userManager = userManager;
    }

    public async Task<string> GetTokenAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName)
                   ?? throw new Exception("Could not find user");
        var roles = await _userManager.GetRolesAsync(user);

        var key = Convert.FromBase64String(_options.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SignatureAlgorithm
            ),
            Subject = new ClaimsIdentity(user.ToClaims(roles)),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _options.Address,
            Audience = _options.Address
        };
        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    public static TokenValidationParameters ValidationParameters(AppOptions options)
    {
        var key = Convert.FromBase64String(options.JwtSecret);
        return new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidAlgorithms = [SignatureAlgorithm],

            // These are important when we are validating tokens from a
            // different system
            ValidIssuer = options.Address,
            ValidAudience = options.Address,

            // Set to 0 when validating on the same system that created the token
            ClockSkew = TimeSpan.FromSeconds(0),

            // Default value is true already.
            // They are just set here to emphasis the importance.
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
        };
    }
}