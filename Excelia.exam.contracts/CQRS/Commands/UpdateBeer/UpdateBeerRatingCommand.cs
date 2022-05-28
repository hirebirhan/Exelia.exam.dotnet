using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exelia.exam.Business.CQRS.DTO;
using MediatR;

namespace Exelia.exam.Business.CQRS.Commands.UpdateBeer
{
    public class UpdateBeerRatingCommand: IRequest<UpdateBeerRatingResponse>
    {
        public int Rating { get; set; }
        public int Id { get; set; }

        public UpdateBeerRatingCommand(int rating, int id)
        {
            Rating = rating;
            Id = id;
        }
    }
}
