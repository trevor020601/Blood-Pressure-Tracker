using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.User;
using SharedLibrary.BloodPressureDomain.ValueObjects;
using SharedLibrary.DataAccess.Converters;

namespace SharedLibrary.DataAccess.Configurations.Domain;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
            userId => userId.Value,
            value => new UserId(value)
        );

        /// According to RFC 3696 (https://www.rfc-editor.org/rfc/rfc3696#section-3):
        /// There is a length limit on email addresses. That limit is a maximum of 64 characters(octets)
        /// in the "local part"(before the "@") and a maximum of 255 characters (octets) in the domain 
        /// part(after the "@") for a total length of 320 characters.
        builder.Property(u => u.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value)
        ).HasMaxLength(320);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Password).IsRequired();

        builder.Property(u => u.Status).HasConversion(new EnumConverter<AccountStatus>()).IsRequired();
    }
}
