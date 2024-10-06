using GoogleAuth.ResponseModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly JwtTokenHandler _jwtTokenHandler;

    public AuthController(JwtTokenService jwtTokenService, JwtTokenHandler jwtTokenHandler)
    {
        _jwtTokenService = jwtTokenService;
        _jwtTokenHandler = jwtTokenHandler;
    }

    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var authenticationProperties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Challenge(authenticationProperties, "Google");
    }

    [HttpGet("google-response")]
    public async Task<ActionResult<IApiResponse>> GoogleResponse()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync("Google");

        if (!authenticateResult.Succeeded)
            return BadRequest(new ErrorResponse { Message = "Authentication failed" });

        var email = _jwtTokenHandler.GetEmailFromClaims(authenticateResult.Principal);
        var token = _jwtTokenService.GenerateJwtToken(email);

        return Ok(new GoogleLoginResponse { Token = token, Message = "Login successful" });
    }

    [HttpGet("validate-token")]
    public ActionResult<IApiResponse> ValidateToken([FromQuery] string token)
    {
        try
        {
            var isValid = _jwtTokenService.ValidateJwtToken(token);

            return Ok(new ValidateTokenResponse { Message = "Token is valid", TokenIsValid = isValid });
        }
        catch (SecurityTokenException ex)
        {
            return Ok(new ValidateTokenResponse { Message = "Token is invalid", TokenIsValid = false, Error = ex.Message });
        }
        catch (Exception ex)
        {
            return Unauthorized(new ErrorResponse { Message = "An error occurred", Error = ex.Message });
        }
    }


    [HttpGet("user-info")]
    public ActionResult<IApiResponse> GetUserInfo([FromQuery] string token)
    {
        try
        {
            var userEmail = _jwtTokenHandler.GetEmailFromToken(token);
            return Ok(new UserInfoResponse { Email = userEmail, Message = "User info retrieved successfully" });
        }
        catch (Exception ex)
        {
            return Unauthorized(new ErrorResponse { Message = "Token is invalid", Error = ex.Message });
        }
    }
}
