using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Authentication.Policies;

namespace SharedLibrary.DataAccess.Configurations;

internal sealed class PolicyConfiguration : IEntityTypeConfiguration<Policy>
{
    public void Configure(EntityTypeBuilder<Policy> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();

        builder.HasData(
            new Policy { Id = Policy.AdminId, Name = Policy.Admin },
            new Policy { Id = Policy.UserId, Name = Policy.User });
    }
}
