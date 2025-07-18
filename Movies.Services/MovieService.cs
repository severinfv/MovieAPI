using Service.Contracts;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Domain.Models.Exceptions;
using Movies.Shared.DTOs;
using Movies.Shared.DTOs.ActorDTOs;
using Movies.Shared.DTOs.MovieDTOs;
using Movies.Shared.DTOs.ReviewDTOs;
using Movies.Shared.Parameters;


namespace Movies.Services
{
    public class MovieService : IMovieService
    {
        private IUnitOfWork uow;
        public MovieService(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task<bool> MovieExistsAsync(Guid id) => await uow.MovieRepository.ExistsAsync(id);
        public async Task<MovieDto> GetMovieAsync(Guid id, bool trackChanges = false)
        {

            var movie = await uow.MovieRepository.GetByIdAsync(id, trackChanges) ?? throw new MovieNotFoundException(id);

            var dto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year.Year,
                Runtime = movie.Runtime,
                IMDBRating = movie.IMDB
            };

            return dto;
        }
        public async Task<MovieDetailsDto> GetMovieWithDetailsAsync(Guid id, bool includeGenres, bool includeActors, bool includeReviews, bool trackChanges = false)
        {
            var movie = await uow.MovieRepository.GetMovieWithDetailsAsync(id, includeGenres, includeActors, includeReviews, trackChanges)
                ?? throw new MovieNotFoundException(id);

            var dto = new MovieDetailsDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year.Year,
                DirectorName = movie.Director.Name,
                Runtime = movie.Runtime,
                IMDBRating = movie.IMDB,
                Genres = includeGenres
                    ? movie.Genres.Select(g => new GenreDto(g.MovieGenre))
                    : Enumerable.Empty<GenreDto>(),
                Actors = includeActors
                    ? movie.Actors.Select(a => new ActorDto { Name = a.Name })
                    : Enumerable.Empty<ActorDto>(),
                Reviews = includeReviews
                    ? movie.Reviews.Select(r => new ReviewDto(r.ApplicationUserId, r.UserComment, r.UserRating))
                    : Enumerable.Empty<ReviewDto>()
            };

            return dto;
        }

        public async Task<PagedList<MovieDto>> GetMoviesAsync(MovieParameters parameters, bool trackChanges = false)
        {
            var movies = await uow.MovieRepository.GetAllAsync(parameters, trackChanges);
            var dtos = movies.Select(m => new MovieDto { Id = m.Id, Title = m.Title, Year = m.Year.Year, Runtime = m.Runtime, IMDBRating = m.IMDB }).ToList();

            return new PagedList<MovieDto>(
            dtos,
            movies.TotalCount,
            movies.CurrentPage,
            movies.PageSize);
        }

        public async Task<MovieDto> AddMovieAsync(MovieManipulationDto dto, bool trackChanges = false)
        {
            ValidateDto(dto);

            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Runtime = dto.Runtime,
                IMDB = dto.IMDBRating,
            };
            movie.DirectorId = dto.DirectorId;

            uow.MovieRepository.Create(movie);
            await uow.CompleteAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year.Year,
                Runtime = movie.Runtime,
                IMDBRating = movie.IMDB
            };

            return movieDto;
        }

        public async Task UpdateMovieAsync(Guid id, MovieUpdateDto dto, bool trackChanges = true)
        {
            var movie = await uow.MovieRepository.GetByIdAsync(id, trackChanges) ?? throw new MovieNotFoundException(id);

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Runtime = dto.Runtime;
            movie.IMDB = dto.IMDBRating;

            uow.MovieRepository.Update(movie);
            await uow.CompleteAsync();
        }

        public async Task DeleteMovieAsync(Guid id, bool trackChanges = false)
        {
            var movie = await uow.MovieRepository.GetByIdAsync(id, trackChanges) ?? throw new MovieNotFoundException(id);

            uow.MovieRepository.Delete(movie);

            await uow.CompleteAsync();
        }

        public void ValidateDto(MovieManipulationDto dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Title))
                errors.Add("Title, ");

            if (dto.Year == new DateOnly(0001, 01, 01))
                errors.Add("Year, ");

            if (dto.Runtime == 0)
                errors.Add("Runtime, ");

            if (dto.IMDBRating == 0)
                errors.Add("IMDB rating, ");

            //if (dto.DirectorId is null)
               // errors.Add("DirectorId.");

            if (errors.Any())
                throw new DtoBadRequestException(string.Join(" ", errors));
        }

        public async void SaveChangesAsync()
        {
            await uow.CompleteAsync();
        }

    }
}

