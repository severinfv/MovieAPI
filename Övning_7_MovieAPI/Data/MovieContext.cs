using Microsoft.EntityFrameworkCore;
using Övning_7_MovieAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Övning_7_MovieAPI.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                        .HasOne(s => s.MovieDetails)
                        .WithOne(a => a.Movie)
                        .HasForeignKey<MovieDetails>(a => a.MovieId);

            modelBuilder.Entity<MovieDetails>()
            .HasIndex(a => a.MovieId)
            .IsUnique();

            modelBuilder.Entity<Movie>()
            .HasMany(m => m.Actors)
            .WithMany(a => a.Movies);

            modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies);

        }
    }
}
