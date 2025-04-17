namespace EcoTrueke.Application.UseCases.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }

        public LoginResponse(string token, string id, string email)
        {
            Token = token;
            Id = id;
            Email = email;
        }
    }
}
