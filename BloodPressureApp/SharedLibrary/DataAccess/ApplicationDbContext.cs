using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.Authentication.Policies;
using SharedLibrary.BloodPressureDomain.BloodPressureReading;
using SharedLibrary.BloodPressureDomain.HealthInformation;
using SharedLibrary.BloodPressureDomain.Medication;
using SharedLibrary.BloodPressureDomain.Patient;
using SharedLibrary.BloodPressureDomain.TrackingDevice;
using SharedLibrary.BloodPressureDomain.User;
using SharedLibrary.UnitOfWork;

namespace SharedLibrary.DataAccess;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<UserPolicy> UserPolicies { get; set; }

    public DbSet<AdminPolicy> AdminPolicies { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<HealthInformation> HealthInformations { get; set; }

    public DbSet<BloodPressureReading> BloodPressureReadings { get; set; }

    public DbSet<Medication> Medications { get; set; }

    public DbSet<TrackingDevice> TrackingDevices { get; set; }

    public ChangeTracker ChangeTracker { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public sealed class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Policy> Policies { get; set; }

    public DbSet<UserPolicy> UserPolicies { get; set; }

    public DbSet<AdminPolicy> AdminPolicies { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<HealthInformation> HealthInformations { get; set; }

    public DbSet<BloodPressureReading> BloodPressureReadings { get; set; }

    public DbSet<Medication> Medications { get; set; }

    public DbSet<TrackingDevice> TrackingDevices { get; set; }
}
