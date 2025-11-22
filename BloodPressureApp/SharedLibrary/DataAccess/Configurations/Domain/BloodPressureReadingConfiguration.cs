using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.BloodPressureReading;
using SharedLibrary.BloodPressureDomain.TrackingDevice;
using SharedLibrary.DataAccess.Converters;

namespace SharedLibrary.DataAccess.Configurations.Domain;

internal class BloodPressureReadingConfiguration : IEntityTypeConfiguration<BloodPressureReading>
{
    public void Configure(EntityTypeBuilder<BloodPressureReading> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasConversion(
            bloodPressureReadingId => bloodPressureReadingId.Value,
            value => new BloodPressureReadingId(value)
        );

        builder.Property(t => t.TrackingDeviceId).HasConversion(
            trackingDeviceId => trackingDeviceId.Value,
            value => new TrackingDeviceId(value)
        );

        builder.Property(b => b.Systolic).IsRequired();

        builder.Property(b => b.Diastolic).IsRequired();

        builder.Property(b => b.HeartRate).IsRequired();

        builder.Property(b => b.Weight).IsRequired();

        builder.Property(b => b.ReadingDate).IsRequired();

        builder.Property(b => b.Note);

        builder.Property(b => b.MeasurementSource).HasConversion(new EnumConverter<Source>()).IsRequired();

        builder.Property(b => b.Position).HasConversion(new EnumConverter<Position>());
    }
}
