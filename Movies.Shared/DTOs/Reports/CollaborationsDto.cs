using Movies.Shared.DTOs;

namespace Movies.Shared.DTOs.Reports
{
    public class CollaborationsDto
    { public string Director { get; set; } = null!;
      public string Actor {  get; set; } = null!;
      public List<MovieDto>? Movies { get; set; } = new();
      public int? Count { get; set; }
    }

}
