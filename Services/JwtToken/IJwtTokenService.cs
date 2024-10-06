public interface IJwtTokenService
{
    string GenerateJwtToken(string email);
    bool ValidateJwtToken(string authToken);
}
