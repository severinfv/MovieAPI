using Domain.Contracts.Repositories;
using Domain.Models.Entities;
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

        public async Task<MovieDto> GetMovieAsync(int id, bool trackChanges = false)
        {
            var movie = await uow.MovieRepository.GetMovieAsync(id, trackChanges);

            if (movie == null) return null!;

            var dto = new MovieDto { Id = movie.Id, Title = movie.Title, Year = movie.Year.Year, Runtime = movie.Runtime, IMDBRating = movie.IMDBRating };

            return dto;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync(bool includeActors, bool trackChanges = false)
        {
            var movies = await uow.MovieRepository.GetMoviesAsync(includeActors, trackChanges);
            var dtos = movies.Select(m => new MovieDto { Id = m.Id, Title = m.Title, Year = m.Year.Year, Runtime = m.Runtime, IMDBRating = m.IMDBRating }); //.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return dtos;
        }
    }
}