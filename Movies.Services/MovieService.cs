using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Domain.Models.Exceptions;
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
        public async Task<bool> MovieExistsAsync(int id) => await uow.MovieRepository.ExistsAsync(id);
        public async Task<MovieDto> GetMovieAsync(int id, bool trackChanges = false)
        {
            
            var movie = await uow.MovieRepository.GetByIdAsync(id, trackChanges) ?? throw new MovieNotFoundException(id);

            var dto = new MovieDto 
            {
                Id = movie.Id, 
                Title = movie.Title, 
                Year = movie.Year.Year, 
                Runtime = movie.Runtime, 
                IMDBRating = movie.IMDBRating 
            };

            return dto;
        }
        public async Task<MovieDetailsDto> GetMovieWithDetailsAsync (int id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false) 
        {
            var movie = await uow.MovieRepository.GetMovieWithDetailsAsync(id, includeGenres, includeActors, includeReviews, trackChanges) ?? throw new MovieNotFoundException(id);

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
                    ? movie.Actors.Select(a => new ActorDto { Name = a.Name })
                    : Enumerable.Empty<ActorDto>(),
                Reviews = includeReviews
                    ? movie.Reviews.Select(r => new ReviewDto(r.UserName, r.Comment, r.Rating))
                    : Enumerable.Empty<ReviewDto>()
            };

            return dto;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync(bool trackChanges = false)
        {
            var movies = await uow.MovieRepository.GetAllAsync(trackChanges);
            var dtos = movies.Select(m => new MovieDto { Id = m.Id, Title = m.Title, Year = m.Year.Year, Runtime = m.Runtime, IMDBRating = m.IMDBRating });
            return dtos;
        }

        public async Task<MovieDto> AddMovieAsync(MovieCreateDto dto, bool trackChanges = false)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Runtime = dto.Runtime,
                IMDBRating = dto.IMDBRating,
            };
            movie.DirectorId = 1; //ToDo

            uow.MovieRepository.Create(movie);
            await uow.CompleteAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year.Year,
                Runtime = movie.Runtime,
                IMDBRating = movie.IMDBRating
            };

            return movieDto;
        }

        public async Task UpdateMovieAsync(int id, MovieUpdateDto dto, bool trackChanges=true)
        {

            var movie = await uow.MovieRepository.GetByIdAsync(id, trackChanges);
            if (movie is null)
            {
                throw new KeyNotFoundException($"Movie with id {id} not found");
            }

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Runtime = dto.Runtime;
            movie.IMDBRating = dto.IMDBRating;

            uow.MovieRepository.Update(movie);
            await uow.CompleteAsync();
        }

        public async Task DeleteMovieAsync(int id, bool trackChanges = false)
        {
            var movie = await uow.MovieRepository.GetByIdAsync(id, trackChanges);

            if (movie != null)
                uow.MovieRepository.Delete(movie);

            await uow.CompleteAsync();
        }

        public async void SaveChangesAsync()
        {
            await uow.CompleteAsync();
        }

    }
}

