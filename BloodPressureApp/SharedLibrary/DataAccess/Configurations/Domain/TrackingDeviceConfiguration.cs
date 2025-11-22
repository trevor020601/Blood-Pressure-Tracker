using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.BloodPressureDomain.TrackingDevice;
using SharedLibrary.DataAccess.Converters;

namespace SharedLibrary.DataAccess.Configurations.Domain;

internal class TrackingDeviceConfiguration : IEntityTypeConfiguration<TrackingDevice>
{
    public void Configure(EntityTypeBuilder<TrackingDevice> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasConversion(
            trackingDeviceId => trackingDeviceId.Value,
            value => new TrackingDeviceId(value)
        );

        builder.Property(t => t.Manufacturer).IsRequired();

        builder.Property(t => t.Model).IsRequired();

        builder.Property(t => t.SerialNumber).IsRequired();

        builder.Property(t => t.ConnectionType).HasConversion(new EnumConverter<ConnectionType>()).IsRequired();

        builder.Property(t => t.LastSyncDate).IsRequired();
    }
}
