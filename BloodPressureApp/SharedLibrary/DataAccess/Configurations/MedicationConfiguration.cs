using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.Medication;
using SharedLibrary.DataAccess.Converters;

namespace SharedLibrary.DataAccess.Configurations;

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

        builder.OwnsOne(
            m => m.Dosage,
            dosageBuilder =>
            {
                dosageBuilder.Property(p => p.Value);
                dosageBuilder.Property(p => p.Unit).HasConversion(new EnumConverter<Unit>());
            }
        );

        builder.Property(m => m.StartDate).IsRequired();

        builder.Property(m => m.EndDate);
    }
}
