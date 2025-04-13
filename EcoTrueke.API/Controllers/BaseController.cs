using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string Token
        {
            get
            {
                // Get token
                var schemaAuthentication = "Bearer";
                var authorization = Request.Headers["Authorization"].ToString();
                return string.IsNullOrEmpty(Request.Headers["Authorization"].ToString()) || authorization.Length <= 7
                        ? null
                        : Request.Headers["Authorization"].ToString()[(schemaAuthentication.Length + 1)..];
            }
        }
        protected string LoggedUserId
        {
            get
            {
                // Read token
                JwtSecurityTokenHandler handler = new();
                JwtSecurityToken token = handler.ReadToken(Token) as JwtSecurityToken;

                // Get id
                var claim = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrWhiteSpace(claim))
                    return (claim);

                return "";
            }
        }
        protected string LoggedUserRole
        {
            get
            {
                // Read token
                JwtSecurityTokenHandler handler = new();
                JwtSecurityToken token = handler.ReadToken(Token) as JwtSecurityToken;

                // Get role
                var claim = token.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;
                if (!string.IsNullOrWhiteSpace(claim))
                    return (claim);

                return "";
            }
        }
    }

}
