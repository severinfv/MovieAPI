using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies.Infrastructure.Configurations
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {

            builder.HasKey(c => new { c.MovieId, c.ActorId });

            builder.HasOne(e => e.Movie)
              .WithMany(s => s.Roles)
              .HasForeignKey(e => e.MovieId);

            builder.HasOne(e => e.Actor)
                   .WithMany(c => c.Roles)
                   .HasForeignKey(e => e.ActorId);

            builder.ToTable("MovieActor");



        }
    }
}
