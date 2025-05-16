using EcoTrueke.Domain.Constants;

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

        public int Quantity { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }


        public static Product RegisterProduct(string userId, string name, string typeTranscription, IEnumerable<string> category, string condition, string quantity, string? description = null, string? productPicture = null)
        {
            return new()
            {
                UserId = userId,
                Name = name,
                TypeTranscription = typeTranscription,
                Category = category,
                Status = Types.ProductStatus.Pending,
                Description = description,
                Condition = condition,
                Quantity = int.Parse(quantity),
                ProductPicture = productPicture,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }

        public bool IsSameData(string name, string typeTranscription, IEnumerable<string> category, string condition, string quantity, string? description = null, string? productPicture = null)
        {
            return
                Name == name &&
                TypeTranscription == typeTranscription &&
                Category.SequenceEqual(category) &&
                Description == description &&
                Condition == condition &&
                Quantity == int.Parse(quantity) &&
                ProductPicture == productPicture;
        }

        public void EditProduct(string name, string typeTranscription, IEnumerable<string> category, string condition, string quantity, string? description = null, string? productPicture = null)
        {
            Name = name;
            TypeTranscription = typeTranscription;
            Category = category;
            Condition = condition;
            Quantity = int.Parse(quantity);
            Description = description;
            ProductPicture = productPicture;
            
        }
    }
}
