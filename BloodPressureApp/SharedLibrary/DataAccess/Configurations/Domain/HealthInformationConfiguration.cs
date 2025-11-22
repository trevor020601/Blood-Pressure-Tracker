using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.HealthInformation;
using SharedLibrary.BloodPressureDomain.Patient;
using SharedLibrary.DataAccess.Converters;

namespace SharedLibrary.DataAccess.Configurations.Domain;

internal class HealthInformationConfiguration : IEntityTypeConfiguration<HealthInformation>
{
    public void Configure(EntityTypeBuilder<HealthInformation> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id).HasConversion(
            healthInformationId => healthInformationId.Value,
            value => new HealthInformationId(value)
        );

        builder.Property(h => h.PatientId).HasConversion(
            patientId => patientId.Value,
            value => new PatientId(value)
        );

        builder.Property(h => h.FirstName).IsRequired();

        builder.Property(h => h.LastName).IsRequired();

        builder.Property(h => h.MiddleName);

        builder.Property(h => h.Gender).HasConversion(new EnumConverter<Sex>()).IsRequired();

        builder.Property(h => h.DateOfBirth).IsRequired();

        builder.HasMany(h => h.Medications)
               .WithOne()
               .HasForeignKey(m => m.Id)
               .IsRequired();
    }
}
