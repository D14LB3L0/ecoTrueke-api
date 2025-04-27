namespace EcoTrueke.Domain.Entities
{
    public class Person
    {
        public string Id { get; set; }

        public string name { get; set; }

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
                name = firstName,
                PaternalSurname = paternalSurname,
                MaternalSurname = maternalSurname,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }

        public void EditPerson(string firstName, string paternalSurname, string maternalSurname, string phone,
            string address, string documentNumber, string documentType, string gender)
        {
            name = firstName;
            PaternalSurname = paternalSurname;
            MaternalSurname = maternalSurname;
            Phone = phone;
            Address = address;
            DocumentNumber = documentNumber;
            DocumentType = documentType;
            Gender = gender;
            //ProfilePicture = profilePicture;
        }

        public bool IsSameData(string firstName, string paternalSurname, string maternalSurname, string phone,
            string address, string documentNumber, string documentType, string gender)
        {
            return
                name == firstName &&
                PaternalSurname == paternalSurname &&
                MaternalSurname == maternalSurname &&
                Phone == phone &&
                Address == address &&
                DocumentNumber == documentNumber &&
                DocumentType == documentType &&
                Gender == gender;
        }
    }
}
