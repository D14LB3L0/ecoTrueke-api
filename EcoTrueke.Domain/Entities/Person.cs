    namespace EcoTrueke.Domain.Entities
{
    public class Person
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentType { get; set; }    // "dni"

        public string Gender { get; set; }  // "female", "male", "other"

        public string ProfilePictureUrl { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public static Person Create (string firstName, string lastName)
        {
            return new()
            {
                FirstName = firstName,
                LastName = lastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
