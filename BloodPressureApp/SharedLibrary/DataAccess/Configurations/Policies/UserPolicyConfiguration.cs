using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Authentication.Policies;

namespace SharedLibrary.DataAccess.Configurations.Policy;

internal sealed class UserPolicyConfiguration : IEntityTypeConfiguration<UserPolicy>
{
    public void Configure(EntityTypeBuilder<UserPolicy> builder)
    {
        builder.HasKey(up => new { up.UserId, up.PolicyId });

        builder.HasOne(up => up.User).WithMany().HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(up => up.Policy).WithMany().HasForeignKey(up => up.PolicyId).OnDelete(DeleteBehavior.Cascade);
    }
}
