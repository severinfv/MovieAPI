using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies.Infrastructure.Data.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(c => c.Runtime)
                .IsRequired()
                .HasMaxLength(600);

            builder.Property(c => c.IMDB)
                .HasMaxLength(10);

            builder.HasMany(c => c.Genres)
                .WithMany(e => e.Movies);

            builder.HasMany(c => c.Actors)
                .WithMany(e => e.Movies);

            builder.HasOne(c => c.Director)
                .WithMany(e => e.Movies)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.MovieDetails)
                .WithOne(a => a.Movie)
                .HasForeignKey<MovieDetails>(a => a.MovieId)
                .OnDelete(DeleteBehavior.Cascade); // Obs!

            builder.HasMany(c => c.Reviews)
                   .WithOne(e => e.Movie)
                   .HasForeignKey(e => e.MovieId)
                   .OnDelete(DeleteBehavior.Cascade); //Obs!

            builder.HasMany(s => s.Actors)
               .WithMany(c => c.Movies)
               .UsingEntity<MovieActor>(
               e => e.HasOne(e => e.Actor).WithMany(c => c.Roles),
               e => e.HasOne(e => e.Movie).WithMany(s => s.Roles));

            builder.ToTable("Movies");
        }
    }
}
