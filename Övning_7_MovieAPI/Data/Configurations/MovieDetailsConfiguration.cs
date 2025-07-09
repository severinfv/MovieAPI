using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Övning_7_MovieAPI.Models.Entities;

namespace Övning_7_MovieAPI.Data.Configurations
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
