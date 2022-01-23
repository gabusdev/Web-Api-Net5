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

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Hotel>()
                .HasOne(h => h.Country)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h => h.CountryId);

            model.Entity<Country>()
                .Property(c => c.ShortName)
                    .HasMaxLength(3);
        }
    }
}
