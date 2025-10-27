using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.Medication;

namespace SharedDataSource.Configurations;

internal class MedicationConfiguration : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id).HasConversion(
            medicationId => medicationId.Value,
            value => new MedicationId(value)
        );

        builder.Property(m => m.Name).IsRequired();

        builder.Property(m => m.Schedule).IsRequired();

        // This is questionable...
        builder.ComplexProperty(m => m.Dosage);

        builder.Property(m => m.StartDate).IsRequired();

        builder.Property(m => m.EndDate);
    }
}
