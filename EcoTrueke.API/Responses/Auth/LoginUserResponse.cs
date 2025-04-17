namespace EcoTrueke.API.Responses.Auth
{
    public class LoginUserResponse
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }

        public LoginUserResponse(string token, string id, string email)
        {
            Token = token;
            Id = id;
            Email = email;
        }
    }
}
