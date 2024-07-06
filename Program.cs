using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata;

Console.WriteLine("Hello, World!");


using var db = new AcmeContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Read
Console.WriteLine("Querying for a publictions");
foreach ( var publication in db.Publications.OrderBy(publication => publication.Id))
{
    Console.WriteLine($"Publication: '{publication.Name}', can be said:'{publication.Description}' " );
}

// Update
//Console.WriteLine("Updating the blog and adding a post");
//blog.Url = "https://devblogs.microsoft.com/dotnet";
//blog.Posts.Add(
//    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
//db.SaveChanges();

//// Delete
//Console.WriteLine("Delete the blog");
//db.Remove(blog);
//db.SaveChanges();
