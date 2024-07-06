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
        //modelBuilder.Entity<Publication>(entity => { entity.Property(e => e.Name).IsRequired(); });

        #region PublicationSeed
        modelBuilder.Entity<Publication>().HasData(
            new Publication { Name = "Acme EF Coding For Dummies", Description = "You'd better off using the internet", Id = 1 },
            new Publication { Name = "How To Lead Teams", Description = "Practical Advice For Getting Burned", Id = 2 },
            new Publication { Name = "The Customer Is Not Right ", Description = "But don't tell them", Id = 3 }
            );
        #endregion

        #region CountrySeed
        modelBuilder.Entity<Country>().HasData(
                new Country { Name = "Afghanistan", CountryCode = "AFG", Id = 1 }, //Id's should be a sequence on the database...
                new Country { Name = "Albania", CountryCode = "SLB", Id = 2 },
                new Country { Name = "Algeria", CountryCode = "DZA", Id = 3 },
                new Country { Name = "Andorra", CountryCode = "AND", Id = 4 },
                new Country { Name = "Angola", CountryCode = "AGO", Id = 5 },
                new Country { Name = "Antigua and Barbuda", CountryCode = "ATG", Id = 6 },
                new Country { Name = "Argentina", CountryCode = "ARG", Id = 7 },
                new Country { Name = "Armenia", CountryCode = "ARM", Id = 8 },
                new Country { Name = "Aruba", CountryCode = "ABW", Id = 9 },
                new Country { Name = "Australia", CountryCode = "AUS", Id = 10 },
                new Country { Name = "Austria", CountryCode = "AUT", Id = 11 },
                new Country { Name = "New Zealand", CountryCode = "NZL", Id = 12 }
            );
        #endregion


        //#region Customer
        #region CustomerSeed
        modelBuilder.Entity<Customer>().HasData(
            new Publication { Name = "Dave Davies", Id = 1 },
            new Publication { Name = "Bob Smith", Id = 2 },
            new Publication { Name = "Rachel Jenkins", Id = 3 }
            );
        #endregion

        //#region Seed
        //modelBuilder.Entity<Post>().HasData(
        //    new Post { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" });
        //#endregion

        //#region AnonymousPostSeed
        //modelBuilder.Entity<Post>().HasData(
        //    new { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" });
        //#endregion

        //#region OwnedTypeSeed
        //modelBuilder.Entity<Post>().OwnsOne(p => p.AuthorName).HasData(
        //    new { PostId = 1, First = "Andriy", Last = "Svyryd" },
        //    new { PostId = 2, First = "Diego", Last = "Vega" });
        //#endregion
    }
}

//public class Blog
//{
//    public int BlogId { get; set; }
//    public string Url { get; set; }

//    public List<Post> Posts { get; } = new();
//}

//public class Post
//{
//    public int PostId { get; set; }
//    public string Title { get; set; }
//    public string Content { get; set; }

//    public int BlogId { get; set; }
//    public Blog Blog { get; set; }
//}