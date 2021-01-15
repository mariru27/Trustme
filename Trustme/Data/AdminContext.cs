using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trustme.Models;


namespace Trustme.Data
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {

        }
        public DbSet<User> User { set; get; }
        public DbSet<Role> Role { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Role>().ToTable("Role");
        }
    }
}
