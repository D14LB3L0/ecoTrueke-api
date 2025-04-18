namespace EcoTrueke.API.Responses.Auth
{
    public class LoginUserResponse
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string AccountStatus { get; set; }

        public LoginUserResponse(string token, string id, string email, string accountStatus)
        {
            Token = token;
            Id = id;
            Email = email;
            AccountStatus = accountStatus;
        }
    }
}
