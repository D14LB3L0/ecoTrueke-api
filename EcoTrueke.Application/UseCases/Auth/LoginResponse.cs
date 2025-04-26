using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Application.UseCases.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public Domain.Entities.User User { get; set; }
        public Person Person { get; set; }

        public LoginResponse(string token, Domain.Entities.User user, Person person)
        {
            Token = token;
            User = user;
            Person = person;
        }
    }
}
