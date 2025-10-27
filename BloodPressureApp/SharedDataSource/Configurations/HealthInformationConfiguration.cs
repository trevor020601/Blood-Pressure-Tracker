using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.HealthInformation;

namespace SharedDataSource.Configurations;

internal class HealthInformationConfiguration : IEntityTypeConfiguration<HealthInformation>
{
    public void Configure(EntityTypeBuilder<HealthInformation> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id).HasConversion(
            healthInformationId => healthInformationId.Value,
            value => new HealthInformationId(value)
        );
    }
}
