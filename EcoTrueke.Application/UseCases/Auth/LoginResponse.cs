namespace EcoTrueke.Application.UseCases.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string AccountStatus { get; set; }

        public LoginResponse(string token,  string email, string accountStatus)
        {
            Token = token;
            Email = email;
            AccountStatus = accountStatus;
        }
    }
}
