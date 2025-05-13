namespace EcoTrueke.Application.UseCases.Product
{
    public class GetProductResponse
    {
        public ProductResponse Product {  get; set; }

        public GetProductResponse(ProductResponse product)
        {
            Product = product;
        }
    }

    public class ProductResponse
    {
        public string? ProductPicture { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string TypeTranscription { get; set; } // exchange - donation - sale

        public IEnumerable<string> Category { get; set; }  // clothes - toys

        public string Condition { get; set; }

        public string Status { get; set; } // pending - traded - sold - donnated

        public int Quantity { get; set; }

        public ProductResponse(Domain.Entities.Product product)
        {
            ProductPicture = product.ProductPicture;
            Name = product.Name;
            Description = product.Description;
            TypeTranscription = product.TypeTranscription;
            Category = product.Category;
            Condition = product.Condition;
            Status = product.Status;
            Quantity = product.Quantity;
        }
    }
}
