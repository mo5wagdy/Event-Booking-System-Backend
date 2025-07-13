using Booking.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repo.Config
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Bookings>
    {

        public void Configure(EntityTypeBuilder<Bookings> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BookingDate)
                .IsRequired();

            builder.HasOne(b => b.Event)
               .WithMany(e => e.Bookings)
              .HasForeignKey(b => b.EventId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.User)
             .WithMany()
           .HasForeignKey(b => b.UserId)
           .OnDelete(DeleteBehavior.Cascade);
        }






    }




    }    


