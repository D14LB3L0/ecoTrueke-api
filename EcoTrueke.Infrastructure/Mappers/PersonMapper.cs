namespace EcoTrueke.Infrastructure.Mappers
{
    public static class PersonMapper
    {
        public static MongoModels.Person ToMongo(Domain.Entities.Person person)
        {
            return new MongoModels.Person
            {
                Id = person.Id,
                name = person.name,
                PaternalSurname = person.PaternalSurname,
                MaternalSurname = person.MaternalSurname,
                Phone = person.Phone,
                Address = person.Address,
                DocumentNumber = person.DocumentNumber,
                DocumentType = person.DocumentType,
                Gender = person.Gender,
                ProfilePicture = person.ProfilePicture,
                CreatedAt = person.CreatedAt,
                UpdatedAt = person.UpdatedAt,
                IsDeleted = person.IsDeleted
            };
        }

        public static Domain.Entities.Person ToDomain(MongoModels.Person mongoPerson)
        {
            return new Domain.Entities.Person
            {
                Id = mongoPerson.Id,
                name = mongoPerson.name,
                PaternalSurname = mongoPerson.PaternalSurname,
                MaternalSurname = mongoPerson.MaternalSurname,
                Phone = mongoPerson.Phone,
                Address = mongoPerson.Address,
                DocumentNumber = mongoPerson.DocumentNumber,
                DocumentType = mongoPerson.DocumentType,
                Gender = mongoPerson.Gender,
                ProfilePicture = mongoPerson.ProfilePicture,
                CreatedAt = mongoPerson.CreatedAt,
                UpdatedAt = mongoPerson.UpdatedAt,
                IsDeleted = mongoPerson.IsDeleted
            };
        }
    }
}
