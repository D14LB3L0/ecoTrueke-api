namespace EcoTrueke.Application.UseCases.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string AccountStatus { get; set; }

        public LoginResponse(string token, string id, string email, string accountStatus)
        {
            Token = token;
            Id = id;
            Email = email;
            AccountStatus = accountStatus;
        }
    }
}
