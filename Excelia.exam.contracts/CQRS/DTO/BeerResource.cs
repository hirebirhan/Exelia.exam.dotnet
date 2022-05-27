namespace Excelia.exam.Application.CQRS.DTO;

public class BeerResource
{
    public BeerResource(int id, string name, int rating)
    {
        this.Id= id;
        this.Name=name;
        this.Rating = rating;

    }

    public long Id { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
}