using Movies.Shared.DTOs.ActorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Shared.DTOs.MovieDTOs;
public class MovieCreateDto : MovieManipulationDto
{
    public IEnumerable<ActorDto>? Actors { get; init; }
}

