using EcoTrueke.Domain.Entities;

namespace EcoTrueke.API.Responses.Auth
{
    public class LoginUserResponse
    {
        public string Token { get; set; }

        public User User { get; set; }

        public Domain.Entities.Person Person { get; set; }

        public LoginUserResponse(string token, User user ,Domain.Entities.Person person)
        {
            Token = token;
            User = user;
            Person = person;
        }
    }
}
