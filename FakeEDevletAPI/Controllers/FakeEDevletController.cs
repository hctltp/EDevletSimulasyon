using CommonLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeEDevletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeEDevletController : ControllerBase
    {
        // Endpoint for authorization
        [HttpGet("authorize")]
        public IActionResult Authorize(string response_type, string client_id, string redirect_uri)
        {
            if (string.IsNullOrEmpty(response_type) || response_type != "code" ||
                string.IsNullOrEmpty(client_id) || string.IsNullOrEmpty(redirect_uri))
            {
                return BadRequest(new { error = "Invalid authorization request." });
            }

            string authorizationCode = Guid.NewGuid().ToString();

            // Simulate redirect to the provided redirect_uri
            string redirectUrl = $"{redirect_uri}?code={authorizationCode}";
            return Redirect(redirectUrl); // Redirects the user to the generated URL//return Ok(new { redirect_url = redirectUrl });
        }

        // Endpoint for token exchange
        [HttpPost("token")]
        public IActionResult Token([FromBody] TokenRequest request)
        {
            if (request == null ||
                string.IsNullOrEmpty(request.GrantType) || request.GrantType != "authorization_code" ||
                string.IsNullOrEmpty(request.Code) ||
                string.IsNullOrEmpty(request.ClientId) ||
                string.IsNullOrEmpty(request.ClientSecret) ||
                string.IsNullOrEmpty(request.RedirectUri))
            {
                return BadRequest(new { error = "Invalid token request." });
            }

            // Simulate token generation
            string accessToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            string tokenType = "Bearer";
            int expiresIn = 3600; // Token expires in 1 hour

            return Ok(new TokenResponse
            {
                AccessToken = accessToken,
                TokenType = tokenType,
                ExpiresIn = expiresIn
            });
        }
    }
}
