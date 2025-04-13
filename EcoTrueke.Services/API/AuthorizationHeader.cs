using System.Text;

namespace EcoTrueke.Services.API
{
    public class AuthorizationHeader
    {
        public string Type { get; set; }
        public string AuthenticationString { get; set; }

        public AuthorizationHeader(string token)
        {
            Type = TYPE_BEARER_TOKEN;
            AuthenticationString = token;
        }
        public AuthorizationHeader(string username, string password)
        {
            Type = TYPE_BASIC;
            AuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        }

        public const string TYPE_BEARER_TOKEN = "Bearer";
        public const string TYPE_BASIC = "Basic";
    }
}
