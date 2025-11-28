using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Authentication.Policies;
using SharedLibrary.BloodPressureDomain.User;

namespace SharedLibrary.DataAccess.Configurations.Policies;

internal sealed class UserPolicyConfiguration : IEntityTypeConfiguration<UserPolicy>
{
    public void Configure(EntityTypeBuilder<UserPolicy> builder)
    {
        builder.HasKey(up => new { up.UserId, up.PolicyId });

        builder.Property(u => u.UserId).HasConversion(
        userId => userId.Value,
          value => new UserId(value)
        );

        builder.HasOne(up => up.User).WithMany().HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(up => up.Policy).WithMany().HasForeignKey(up => up.PolicyId).OnDelete(DeleteBehavior.Cascade);
    }
}
