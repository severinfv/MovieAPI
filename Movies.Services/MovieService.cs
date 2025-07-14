using Domain.Contracts.Repositories;
using Movies.Shared.DTOs;
using Service.Contracts;

namespace Movies.Services
{
    public class MovieService : IMovieService
    {
        private IUnitOfWork uow;
        public MovieService(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<bool> MovieExistsAsync(int id) => await uow.MovieRepository.AnyAsync(id);
        public async Task<MovieDto> GetMovieAsync(int id, bool trackChanges = false)
        {
            var movie = await uow.MovieRepository.GetMovieAsync(id, trackChanges);
            if (movie == null) return null!;
            var dto = new MovieDto { Id = movie.Id, Title = movie.Title, Year = movie.Year.Year, Runtime = movie.Runtime, IMDBRating = movie.IMDBRating };
            return dto;
        }
        public async Task<MovieDetailsDto> GetMovieWithDetailsAsync (int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false)
        {
            var movie = await uow.MovieRepository.GetMovieWithDetailsAsync(id, includeGenres, includeActors, includeReviews, trackChanges);
            if (movie == null) return null!;

            var dto = new MovieDetailsDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year.Year,
                DirectorName = movie.Director.Name,
                Runtime = movie.Runtime,
                IMDBRating = movie.IMDBRating,
                Genres = includeGenres
                    ? movie.Genres.Select(g => new GenreDto(g.MovieGenre))
                    : Enumerable.Empty<GenreDto>(),
                Actors = includeActors
                    ? movie.Actors.Select(a => new ActorDto(a.Name))
                    : Enumerable.Empty<ActorDto>(),
                Reviews = includeReviews
                    ? movie.Reviews.Select(r => new ReviewDto(r.ReviewerName, r.Comment, r.Rating))
                    : Enumerable.Empty<ReviewDto>()
            };

            return dto;
        }


    }
}