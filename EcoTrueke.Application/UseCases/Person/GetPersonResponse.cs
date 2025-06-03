namespace EcoTrueke.Application.UseCases.Person
{
    public class GetPersonResponse
    {
        public PersonResponse Person { get; set; }

        public GetPersonResponse(Domain.Entities.Person person)
        {
            Person = new PersonResponse(
                person.Name,
                person.PaternalSurname,
                person.MaternalSurname,
                person?.Address,
                person?.Gender,
                person?.ProfilePicture,
                person.CreatedAt
            );
        }

        public class PersonResponse
        {
            public string Name { get; set; }
            public string PaternalSurname { get; set; }
            public string MaternalSurname { get; set; }
            public string? Address { get; set; }
            public string? Gender { get; set; }
            public string? ProfilePicture { get; set; }
            public DateTime CreatedAt { get; set; }

            public PersonResponse(
                string name,
                string paternalSurname,
                string maternalSurname,
                string? address,
                string? gender,
                string? profilePicture,
                DateTime createdAt)
            {
                Name = name;
                PaternalSurname = paternalSurname;
                MaternalSurname = maternalSurname;
                Address = address;
                Gender = gender;
                ProfilePicture = profilePicture;
                CreatedAt = createdAt;
            }
        }
    }
}
