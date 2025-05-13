namespace EcoTrueke.API.Responses.Product
{
    public class GetProductResponse
    {
        public string? ProductPicture { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string TypeTranscription { get; set; } // exchange - donation - sale

        public IEnumerable<string> Category { get; set; }  // clothes - toys

        public string Condition { get; set; }

        public string Status { get; set; } // pending - traded - sold - donnated

        public int Quantity { get; set; }
    }
}
