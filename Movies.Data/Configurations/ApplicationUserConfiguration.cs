﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Core.Entities;


namespace Movies.Data.Configurations;
internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(c => c.Id);
        builder.ToTable("ApplicationUser");

        builder.HasMany(c => c.Reviews)
                   .WithOne(e => e.ApplicationUser)
                   .HasForeignKey(e => e.ApplicationUserId);
                   //.OnDelete(DeleteBehavior.Cascade); 
    }
}
