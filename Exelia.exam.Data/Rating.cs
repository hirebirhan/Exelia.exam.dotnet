using Exelia.exam.Data.common;

namespace Exelia.exam.Data
{
    public class Rating:BaseEntity
    {
        public int Id { get; set; }
        public int BeerId { get; set; }
        public decimal RatingValue { get; set; }
    }
}
