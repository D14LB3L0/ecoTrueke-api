using EcoTrueke.Domain.Constants;
using EcoTrueke.Util.Security;

namespace EcoTrueke.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string PersonId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string AccountStatus { get; set; }  // "active", "suspended"

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public static User Create(string personId, string email, string password)
        {
            return new()
            {
                PersonId = personId,
                Email = email,
                Password = Encryptor.SHA256Hash(password),
                AccountStatus = Types.AccountStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
