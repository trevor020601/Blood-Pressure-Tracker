using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.BloodPressureReading;
using SharedLibrary.BloodPressureDomain.TrackingDevice;

namespace SharedDataSource.Configurations;

internal class BloodPressureReadingConfiguration : IEntityTypeConfiguration<BloodPressureReading>
{
    public void Configure(EntityTypeBuilder<BloodPressureReading> builder)
    {
        builder.HasKey(b =>  b.Id);

        builder.Property(b => b.Id).HasConversion(
            bloodPressureReadingId => bloodPressureReadingId.Value,
            value => new BloodPressureReadingId(value)
        );

        builder.Property(t => t.TrackingDeviceId).HasConversion(
            trackingDeviceId => trackingDeviceId.Value,
            value => new TrackingDeviceId(value)
        );
    }
}
