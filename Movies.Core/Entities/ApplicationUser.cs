namespace Movies.Core.Entities;
public class ApplicationUser: Entity
{
    public string UserName { get; set; } = null!;
    public DateOnly DateRegistered { get; set; }
    public Guid? ReviewId { get; set; }
    public ICollection<Review>? Reviews { get; set; } = new List<Review>();
}
