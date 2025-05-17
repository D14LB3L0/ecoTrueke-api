namespace EcoTrueke.API.Responses.Product
{
    public class GetPaginatedProductResponse
    {
        public List<ProductsResponse> Products { get; set; }
        public int TotalPages { get; set; }

        public GetPaginatedProductResponse(List<Domain.Entities.Product> products, int totalPages)
        {
            Products = new();
            foreach (var product in products)
                Products.Add(new ProductsResponse(product));
            TotalPages = totalPages;
        }
    }

    public class ProductsResponse
    {
        public string Id { get; set; }
        public string ProductPicture { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string TypeTranscription { get; set; }
        public string Condition { get; set; }
        public string Status { get; set; }

        public ProductsResponse(Domain.Entities.Product product)
        {
            Id = product.Id;
            ProductPicture = product.ProductPicture;
            Name = product.Name;
            Quantity = product.Quantity;
            TypeTranscription = product.TypeTranscription;
            Condition = product.Condition;
            Status = product.Status;
        }
    }
}
