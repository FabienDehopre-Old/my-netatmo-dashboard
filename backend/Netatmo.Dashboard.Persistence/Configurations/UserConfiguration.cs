using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseSqlServerIdentityColumn();
            builder.Property(e => e.Uid).IsRequired().IsUnicode().HasMaxLength(64);
            builder.Property(e => e.AccessToken).IsUnicode().HasMaxLength(64);
            builder.Property(e => e.ExpiresAt).IsRequired(false);
            builder.Property(e => e.RefreshToken).IsUnicode().HasMaxLength(64);
            builder.Property(e => e.UpdateJobId).IsUnicode().HasMaxLength(100).IsRequired(false);
            builder.Property(e => e.FeelLike).IsRequired(false);
            builder.Property(e => e.PressureUnit).IsRequired(false);
            builder.Property(e => e.Unit).IsRequired(false);
            builder.Property(e => e.WindUnit).IsRequired(false);
            builder.HasIndex(e => e.Uid).IsUnique();
        }
    }
}
