using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataStoreEF.Configurations.Entities
{
    internal class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasOne(h => h.Country)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h => h.CountryId);
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    CountryId = 2,
                    Name = "Hotel Central",
                    Address = "Varadero",
                    Rating = 4.8
                }
            );
        }
    }
}
