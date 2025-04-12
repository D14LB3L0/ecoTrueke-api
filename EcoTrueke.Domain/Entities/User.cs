namespace EcoTrueke.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string PersonId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string AccountStatus { get; set; }  // "active", "suspended", "pending"

        public string Plan { get; set; } // "standard", "premium"

        public DateOnly UpdatedAt { get; set; }
        
        public DateOnly CreatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
