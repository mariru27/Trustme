using Microsoft.EntityFrameworkCore;
using Trustme.Models;


namespace Trustme.Data
{
    public class TMDbContext : DbContext
    {
        public TMDbContext(DbContextOptions<TMDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { set; get; }
        public DbSet<Key> Key { set; get; }

        public DbSet<Role> Role { set; get; }
        public DbSet<UserKey> UserKey { set; get; }

        public DbSet<UserSignedDocument> UserSignedDocuments { set; get; }
        public DbSet<UserUnsignedDocument> UserUnsignedDocuments { set; get; }

        public DbSet<SignedDocument> SignedDocuments { set; get; }
        public DbSet<UnsignedDocument> UnsignedDocuments { set; get; }
        public DbSet<Pending> Pendings { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(e => e.UserSignedDocuments).WithOne(e => e.User).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(e => e.UserKeys).WithOne(e => e.User).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(e => e.UserUnsignedDocuments).WithOne(e => e.User).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(e => e.Pendings).WithOne(e => e.User).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<SignedDocument>().HasOne(e => e.Key).WithMany(e => e.SignedDocuments).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UnsignedDocument>().HasOne(e => e.Key).WithMany(e => e.UnsignedDocuments).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserKey>().HasMany(e => e.Keys).WithOne(e => e.UserKey).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Pending>().HasOne(u => u.User).WithMany(u => u.Pendings).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Key>().HasMany(u => u.SignedDocuments).WithOne(u => u.Key).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Key>().HasMany(u => u.UnsignedDocuments).WithOne(u => u.Key).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Key>().HasOne(u => u.UserKey).WithMany(u => u.Keys).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Key>().ToTable("Key");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserSignedDocument>().ToTable("UserSignedDocument");
            modelBuilder.Entity<UserUnsignedDocument>().ToTable("UserUnsignedDocument");
            modelBuilder.Entity<SignedDocument>().ToTable("SignedDocument");
            modelBuilder.Entity<UnsignedDocument>().ToTable("UnsignedDocument");
            modelBuilder.Entity<Pending>().ToTable("Pending");
        }

    }
}
