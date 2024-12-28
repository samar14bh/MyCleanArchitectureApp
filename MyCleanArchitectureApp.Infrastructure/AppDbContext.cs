using MyCleanArchitectureApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCleanArchitectureApp.Domain.Entities;
using System.Runtime.Remoting.Contexts;
namespace MyCleanArchitectureApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Customer> Customers { get; set; }
        // Other DbSets for additional entities

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Optionally, override OnModelCreating to configure the model if needed
       protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Genres table with some initial data
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" },
            new Genre { Id = 3, Name = "Drama" },
            new Genre { Id = 4, Name = "Horror" },
            new Genre { Id = 5, Name = "Sci-Fi" }
        );
    }
    }
}
}
