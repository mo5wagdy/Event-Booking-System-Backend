using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Core.Models;

namespace Booking.Repo.Config
{

    internal class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            // Configure the Id property as the primary key
            builder.HasKey(u => u.Id);

            // Configure the Id as an identity column (auto-increment)
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd(); // This tells EF Core to generate the Id automatically (auto-increment).

            // Configure other properties
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);
        }
    }



}
