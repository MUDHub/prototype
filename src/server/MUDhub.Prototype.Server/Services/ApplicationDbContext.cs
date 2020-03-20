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
            Database.Migrate();
            //Database.EnsureCreated();
            if (Users.FirstOrDefault() is null)
            {
                CreateInitalUsers();
            }
            
        }


        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        private void CreateInitalUsers()
        {
            Users.Add(new User
            {
                Username = "Marvin",
                PasswordHash = UserManager.CreatePasswordHash("hallowelt")
            });
            SaveChanges();
        }
    }
}