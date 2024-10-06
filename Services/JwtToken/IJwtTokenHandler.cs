using System.Security.Claims;

public interface IJwtTokenHandler
{
    string GetEmailFromToken(string token);
    string GetEmailFromClaims(ClaimsPrincipal claimsPrincipal);
}
