namespace EcoTrueke.Infrastructure.Mappers
{
    public static class PersonMapper
    {
        public static MongoModels.Person ToMongo(Domain.Entities.Person person)
        {
            return new MongoModels.Person
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                Address = person.Address,
                DocumentNumber = person.DocumentNumber,
                DocumentType = person.DocumentType,
                Gender = person.Gender,
                ProfilePictureUrl = person.ProfilePictureUrl,
                CreatedAt = person.CreatedAt,
                UpdatedAt = person.UpdatedAt,
            };
        }

        public static Domain.Entities.Person ToDomain(MongoModels.Person mongoPerson)
        {
            return new Domain.Entities.Person
            {
                Id = mongoPerson.Id,
                FirstName = mongoPerson.FirstName,
                LastName = mongoPerson.LastName,
                PhoneNumber = mongoPerson.PhoneNumber,
                Address = mongoPerson.Address,
                DocumentNumber = mongoPerson.DocumentNumber,
                DocumentType = mongoPerson.DocumentType,
                Gender = mongoPerson.Gender,
                ProfilePictureUrl = mongoPerson.ProfilePictureUrl,
                CreatedAt = mongoPerson.CreatedAt,
                UpdatedAt = mongoPerson.UpdatedAt,
            };
        }
    }
}
