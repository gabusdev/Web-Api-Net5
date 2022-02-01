using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreEF.Configurations.Entities
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.ShortName)
                .HasMaxLength(2);
            
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Cuba",
                    ShortName = "CU"
                },
                new Country
                {
                    Id = 3,
                    Name = "Bahamas",
                    ShortName = "BH"
                }
            );
        }
    }
}
