

namespace EcoTrueke.Application.UseCases.Person
{
    public class EditPersonResponse
    {
        public Domain.Entities.Person Person { get; set; }

        public EditPersonResponse(Domain.Entities.Person person)
        {
            Person = person;
        }
    }
}
