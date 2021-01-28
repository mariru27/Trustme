using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trustme.Models;


namespace Trustme.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {

        }

        public DbSet<User> User {set; get;}
        public DbSet<Key> Key { set; get; }

        public DbSet<Role> Role { set; get; }
        public DbSet<UserKey> UserKey { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Key>().ToTable("Key");

            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserKey>().ToTable("UserKey");
            // modelBuilder
            //.Entity<UserKey>()
            //.HasOne(e => e.User)
            //.WithOne(e => e.)
            //.OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Key>().HasOne(e => e.UserKey).WithOne(e => e.Key).OnDelete(DeleteBehavior.ClientCascade);

        }

    }
}
