using Exelia.exam.Data;

namespace Exelia.exam.Business.Helpers
{
    public static class RatingHelper
    {
        public static decimal CalculateRating(List<Rating> ratings)
        {
            return ratings is not { Count: > 0 } ? 0 : Math.Round( ratings.Average(r => r.RatingValue), 2);
        } 
    }
}
