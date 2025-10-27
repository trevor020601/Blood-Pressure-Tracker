using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.Patient;

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
    }
}
