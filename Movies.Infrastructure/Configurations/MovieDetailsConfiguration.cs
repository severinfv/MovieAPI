using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies.Infrastructure.Configurations
{
    public class MovieDetailsConfiguration : IEntityTypeConfiguration<MovieDetails>
    {
        public void Configure(EntityTypeBuilder<MovieDetails> builder)
        {
            builder.ToTable("MovieDetails");

            builder.HasKey(c => c.Id);

            builder.HasIndex(a => a.MovieId)
            .IsUnique();

        }
    }
}
