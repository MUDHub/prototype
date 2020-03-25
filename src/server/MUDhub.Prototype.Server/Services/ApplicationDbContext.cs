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

        public List<User> List { get; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            List = new List<User>();
            Database.EnsureDeleted();
            //Database.Migrate();
            Database.EnsureCreated();
            if (Users.FirstOrDefault() is null)
            {
                CreateInitalUsers();
            }

        }

        private void CreateInitalUsers()
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "marvin",
                PasswordHash = UserManager.CreatePasswordHash("hallowelt")
            };
            List.Add(user);
            Users.Add(user);
            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "mario",
                PasswordHash = UserManager.CreatePasswordHash("password")
            };
            List.Add(user);
            Users.Add(user);
            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = "moris",
                PasswordHash = UserManager.CreatePasswordHash("test")
            };
            List.Add(user);
            Users.Add(user);
            SaveChanges();
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;    
    }
}