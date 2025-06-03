namespace EcoTrueke.Domain.DTOs
{
    public class GetUserRatingResponse
    {
        public double AverageStars { get; set; }

        public GetUserRatingResponse(double averageStars)
        {
            AverageStars = averageStars;
        }
    }
}
