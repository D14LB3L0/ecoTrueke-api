namespace EcoTrueke.Domain.Entities
{
    public class Person
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string PaternalSurname { get; set; }
        
        public string MaternalSurname { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentType { get; set; }    // "dni"

        public string Gender { get; set; }  // "female", "male", "other"

        public string ProfilePicture { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public static Person Create(string firstName, string paternalSurname, string maternalSurname)
        {
            return new()
            {
                FirstName = firstName,
                PaternalSurname = paternalSurname,
                MaternalSurname = maternalSurname,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
