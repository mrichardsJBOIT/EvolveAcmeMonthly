using AcmeMonthly.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

public class AcmeContext : DbContext
{
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Publication> Publications { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<CompanyDeliveryCountry> CountriesDelivery { get; set; }

    public string DbPath { get; }

    public AcmeContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "acme.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
 
    }
}
