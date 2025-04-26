namespace EcoTrueke.API.Responses.Auth
{
    public class LoginUserResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string AccountStatus { get; set; }

        public LoginUserResponse(string token, string email, string accountStatus)
        {
            Token = token;
            Email = email;
            AccountStatus = accountStatus;
        }
    }
}
