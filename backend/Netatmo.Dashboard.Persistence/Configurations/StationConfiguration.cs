using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Persistence.Configurations
{
    public class StationConfiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseSqlServerIdentityColumn();
            builder.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(256);
            builder.Property(e => e.Altitude).IsRequired();
            builder.Property(e => e.City).IsRequired().IsUnicode().HasMaxLength(256);
            builder.Property(e => e.CountryCode).IsRequired().IsUnicode().IsFixedLength().HasMaxLength(2);
            builder.Property(e => e.Latitude).IsRequired().HasColumnType("decimal(9,7)");
            builder.Property(e => e.Longitude).IsRequired().HasColumnType("decimal(10,7)");
            builder.Property(e => e.Timezone).IsRequired().IsUnicode().HasMaxLength(32);
            builder.Property(e => e.StaticMap).IsRequired(false).IsUnicode().HasMaxLength(1024);
            builder.HasOne(e => e.User).WithMany(e => e.Stations).HasForeignKey(e => e.UserId);
            builder.HasOne(e => e.Country).WithMany(e => e.Stations).HasForeignKey(e => e.CountryCode);
        }
    }
}
