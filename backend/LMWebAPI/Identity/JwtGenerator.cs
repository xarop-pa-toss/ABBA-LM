using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace LMWebAPI.Identity;

public class JwtGenerator
{
    public string GenerateToken(Guid userId, string email)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var key = "ChangeThisStringToSomethingSecurelySaved"u8.ToArray();

        // Claims are basically key-value pairs that are used for validation (https://datatracker.ietf.org/doc/html/rfc7519#section-10.4.1 and https://balta.io/blog/customizando-claims-no-aspnet)
        // JTI is a GUID that identifies the token itself.
        // Sub is the Subject of the token, in this case the user itself
        var claimsList = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claimsList),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "https://id.algarvebowl.org",
            Audience = "https://lm.algarvebowl.org",
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        return tokenHandler.CreateToken(tokenDescriptor);
    }
}