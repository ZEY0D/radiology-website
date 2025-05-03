using System;
using Microsoft.EntityFrameworkCore;
using RadiologyHIS.API.Models;


namespace RadiologyHIS.API.Data;

public class HISContext : DbContext
{
    public HISContext(DbContextOptions<HISContext> options) : base(options){}

    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Scan> Scans { get; set; }
    public DbSet<Inquiry> Inquiries { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    foreach (var entity in modelBuilder.Model.GetEntityTypes())
    {
        foreach (var property in entity.GetProperties())
        {
            if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
            {
                property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                    v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                ));
            }
        }
    }

    base.OnModelCreating(modelBuilder);
}
}
