using Domain.Contracts.Repositories;
using Movies.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lazy<IMovieRepository> movieRepository;
    public IMovieRepository MovieRepository => movieRepository.Value;

    // .. more repos

    private readonly MovieContext context;
    public UnitOfWork(MovieContext context)
    {
        movieRepository = new Lazy<IMovieRepository>(() => new MovieRepository(context));
        this.context = context;
    }
    public async Task CompleteAsync() => await context.SaveChangesAsync();

}
