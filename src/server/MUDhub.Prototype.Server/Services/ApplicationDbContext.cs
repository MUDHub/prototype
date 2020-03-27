using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using MUDhub.Prototype.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MUDhub.Prototype.Server.Services
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "marvin",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "mario",
                PasswordHash = UserManager.CreatePasswordHash("password")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "moris",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "benita",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "paul",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "sven",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "stephan",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "avh",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "cc",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "test",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });

        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
    }
}