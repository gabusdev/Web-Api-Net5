using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataStoreEF
{
    public class HotelsContext: DbContext
    {
        public HotelsContext(DbContextOptions option) : base(option) {}

        public DbSet<Country> Countries { get; set; }
        
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hotel>(entity =>
            {
                entity.HasOne(h => h.Country)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h => h.CountryId);
                entity.HasData(
                    new Hotel
                    {
                        Id=1,
                        CountryId=2,
                        Name="Hotel Central",
                        Address="Varadero",
                        Rating=4.8
                    }
                );
            });


            builder.Entity<Country>(entity =>
            {
                entity.Property(c => c.ShortName)
                    .HasMaxLength(3);
                entity.HasData(
                    new Country
                    {
                        Id = 1,
                        Name = "Jamaica",
                        ShortName = "JMC"
                    },
                    new Country
                    {
                        Id=2,
                        Name ="Cuba",
                        ShortName ="CUB"
                    },
                    new Country
                    {
                        Id=3,
                        Name ="Bahamas",
                        ShortName ="BAH"
                    }
                );
            });
                
        }
    }
}
