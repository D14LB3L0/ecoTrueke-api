namespace EcoTrueke.Domain.Entities
{
    public class Product
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string? ProductPicture { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string TypeTranscription { get; set; } // exchange - donation - sale

        public IEnumerable<string> Category { get; set; }  // clothes - toys

        public string Condition { get; set; }

        public string Status { get; set; } // pending - traded - sold - donnated

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }


        public static Product RegisterProduct(string userId, string name, string typeTranscription, IEnumerable<string> category, string status, string? description = null, string? productPicture = null)
        {
            return new()
            {
                UserId = userId,
                Name = name,
                TypeTranscription = typeTranscription,
                Category = category,
                Status = status,
                Description = description,
                ProductPicture = productPicture,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
