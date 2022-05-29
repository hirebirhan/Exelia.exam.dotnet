using Exelia.exam.Business.CQRS.DTO;
using MediatR;

namespace Exelia.exam.Business.CQRS.Commands.UpdateBeer
{
    public class AddBeerRatingCommand : IRequest<AddBeerRatingResponse>
    {
        public AddBeerRatingCommand(decimal rating, int beerId)
        {
            Rating = rating;
            BeerId = beerId;
        }

        public decimal Rating { get; set; }
        public int BeerId { get; set; }
    }
}
