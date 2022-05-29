using Exelia.exam.Data;

namespace Exelia.exam.Business.Helpers
{
    public static class RatingHelper
    {
        public static decimal CalculateRating(List<Rating> ratings)
        {

            if (ratings == null || ratings.Count <= 0) return 0;
            return Math.Round( ratings.Average(r => r.RatingValue), 2);
        } 
    }
}
