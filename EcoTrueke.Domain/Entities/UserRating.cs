
namespace EcoTrueke.Domain.Entities
{
    public class UserRating
    {
        public string Id { get; set; }

        public string ProposalId { get; set; }

        public string RatedUserId { get; set; }

        public string QualifiedUserId { get; set; }

        public int Stars { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public static UserRating CreateUserRating(string ratedById, string qualifiedUserId, int stars)
        {
            return new()
            {
                RatedUserId = ratedById,
                QualifiedUserId = qualifiedUserId,
                Stars = stars,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
