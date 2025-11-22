using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Authentication.Policies;

namespace SharedLibrary.DataAccess.Configurations.Policy;

internal sealed class AdminPolicyConfiguration : IEntityTypeConfiguration<AdminPolicy>
{
    public void Configure(EntityTypeBuilder<AdminPolicy> builder)
    {
        builder.HasKey(ap => new { ap.AdminId, ap.PolicyId });

        builder.HasOne(ap => ap.Admin).WithMany().HasForeignKey(ap => ap.AdminId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(ap => ap.Policy).WithMany().HasForeignKey(ap => ap.PolicyId).OnDelete(DeleteBehavior.Cascade);
    }
}