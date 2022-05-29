namespace Excelia.exam.Application.CQRS.DTO;

public class BeerResource
{
    public BeerResource(int id, string name, decimal rating)
    {
        Id = id;
        Name = name;
        Rating = rating>0?rating: 0;

    }
   public BeerResource()
    {
        
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Rating { get; set; }
}