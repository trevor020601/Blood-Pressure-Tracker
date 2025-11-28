using Microsoft.EntityFrameworkCore;
using SharedLibrary.Authentication.RefreshToken;
using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.DataAccess.Configurations.RefreshToken;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<Authentication.RefreshToken.RefreshToken>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Authentication.RefreshToken.RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).HasConversion(
            refreshTokenId => refreshTokenId.Value,
            value => new RefreshTokenId(value)
        );

        builder.Property(r => r.UserId).HasConversion(
        userId => userId.Value,
          value => new UserId(value)
        );

        builder.Property(r => r.Token).HasMaxLength(200);

        builder.HasIndex(r => r.Token).IsUnique();

        builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
    }
}
