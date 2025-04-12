namespace EcoTrueke.Domain.Entities
{
    public class Person
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateOnly BirthDate { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentType { get; set; }    // "dni"

        public string Gender { get; set; }  // "female", "male", "other"

        public string ProfilePictureUrl { get; set; }

        public DateOnly UpdatedAt { get; set; }

        public DateOnly CreatedAt { get; set; }
    }
}
