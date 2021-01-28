using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trustme.Models;


namespace Trustme.Data
{
    public class UserKeyContext : DbContext
    {
        public UserKeyContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<User> User { set; get; }
        public DbSet<Key> Key { set; get; }
        public DbSet<UserKey> UserKey { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Key>().ToTable("Key");

            modelBuilder.Entity<UserKey>().ToTable("UserKey");
        }
    }
}
