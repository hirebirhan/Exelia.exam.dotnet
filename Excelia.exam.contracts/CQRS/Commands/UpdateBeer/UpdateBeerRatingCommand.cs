using Exelia.exam.Business.CQRS.DTO;
using MediatR;

namespace Exelia.exam.Business.CQRS.Commands.UpdateBeer
{
    public class UpdateBeerRatingCommand : IRequest<UpdateBeerRatingResponse>
    {
        public UpdateBeerRatingCommand(int rating, int id)
        {
            Rating = rating;
            Id = id;
        }

        public int Rating { get; set; }
        public int Id { get; set; }
    }
}
