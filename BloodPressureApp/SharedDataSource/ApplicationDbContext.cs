﻿using Microsoft.EntityFrameworkCore;
using SharedLibrary.BloodPressureDomain.BloodPressureReading;
using SharedLibrary.BloodPressureDomain.HealthInformation;
using SharedLibrary.BloodPressureDomain.Medication;
using SharedLibrary.BloodPressureDomain.Patient;
using SharedLibrary.BloodPressureDomain.TrackingDevice;
using SharedLibrary.BloodPressureDomain.User;
using SharedLibrary.UnitOfWork;

namespace SharedDataSource;

public class ApplicationDbContext : DbContext, IUnitOfWork
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

    public DbSet<Patient> Patients { get; set; }

    public DbSet<HealthInformation> HealthInformations { get; set; }

    public DbSet<BloodPressureReading> BloodPressureReadings { get; set; }

    public DbSet<Medication> Medications { get; set; }

    public DbSet<TrackingDevice> TrackingDevices { get; set; }
}
