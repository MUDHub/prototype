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
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "marvin",
                PasswordHash = UserManager.CreatePasswordHash("hallowelt")
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

        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;    
    }
}