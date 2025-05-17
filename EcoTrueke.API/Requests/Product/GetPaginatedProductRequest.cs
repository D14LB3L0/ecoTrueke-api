using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Product
{
    public class GetPaginatedProductRequest
    {
        [Required]
        public int Page { get; set; }

        [Required]
        public int AmountPage { get; set; }

        public bool MyProducts { get; set; }
    }
}
