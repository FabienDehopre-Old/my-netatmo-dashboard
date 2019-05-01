using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.Code);
            builder.Property(e => e.Code).IsRequired().IsUnicode().IsFixedLength().HasMaxLength(2);
            builder.Property(e => e.Flag).IsRequired().IsUnicode().HasMaxLength(40);
            builder.Property(n => n.NameEN).IsRequired().IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameBR).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NamePT).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameNL).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameHR).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameFA).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameDE).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameES).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameFR).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameJA).IsRequired(false).IsUnicode().HasMaxLength(256);
            builder.Property(n => n.NameIT).IsRequired(false).IsUnicode().HasMaxLength(256);
        }
    }
}
