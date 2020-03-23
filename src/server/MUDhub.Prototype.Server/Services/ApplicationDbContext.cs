using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using MUDhub.Prototype.Server.Models;
using System;
using System.Linq;

namespace MUDhub.Prototype.Server.Services
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.Migrate();
            //Database.EnsureCreated();
            if (Users.FirstOrDefault() is null)
            {
                CreateInitalUsers();
            }
            
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        private void CreateInitalUsers()
        {
            Users.Add(new User
            {
                Username = "marvin",
                PasswordHash = UserManager.CreatePasswordHash("hallowelt")
            });
            Users.Add(new User
            {
                Username = "mario",
                PasswordHash = UserManager.CreatePasswordHash("password")
            });
            Users.Add(new User
            {
                Username = "moris",
                PasswordHash = UserManager.CreatePasswordHash("test")
            });
            SaveChanges();
        }
    }
}