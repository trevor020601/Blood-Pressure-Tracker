using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.HealthInformation;
using SharedLibrary.BloodPressureDomain.Patient;
using SharedLibrary.BloodPressureDomain.User;

namespace SharedDataSource.Configurations;

internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            patientId => patientId.Value,
            value => new PatientId(value)
        );

        builder.HasMany(p => p.BloodPressureReadings)
               .WithOne()
               .HasForeignKey(b => b.Id)
               .IsRequired();

        builder.HasMany(p => p.TrackingDevices)
               .WithOne()
               .HasForeignKey(t => t.Id)
               .IsRequired();

        builder.HasOne<User>()
               .WithOne()
               .HasForeignKey<Patient>(u => u.UserId)
               .IsRequired();

        builder.HasOne<HealthInformation>()
               .WithOne()
               .HasForeignKey<Patient>(h => h.HealthInformationId)
               .IsRequired();
    }
}
