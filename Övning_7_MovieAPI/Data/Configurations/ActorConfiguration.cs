using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Övning_7_MovieAPI.Models.Entities;

namespace Övning_7_MovieAPI.Data.Configurations
{
    public class ActorConfiguration: IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Actors");

        }
    }
}
