using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharedLibrary.DataAccess.Configurations.Policies;

internal sealed class PolicyConfiguration : IEntityTypeConfiguration<Authentication.Policies.Policy>
{
    public void Configure(EntityTypeBuilder<Authentication.Policies.Policy> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();

        builder.HasData(
            new Authentication.Policies.Policy { Id = Authentication.Policies.Policy.AdminId, Name = Authentication.Policies.Policy.Admin },
            new Authentication.Policies.Policy { Id = Authentication.Policies.Policy.UserId, Name = Authentication.Policies.Policy.User });
    }
}
