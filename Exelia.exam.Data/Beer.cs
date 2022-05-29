using Exelia.exam.Data.common;

namespace Exelia.exam.Data;

public class Beer : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<Rating> Ratings { get; set; }
}