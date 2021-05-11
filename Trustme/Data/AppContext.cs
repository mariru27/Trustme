using Microsoft.EntityFrameworkCore;
using Trustme.Models;


namespace Trustme.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
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
        public DbSet<PendingRequest> PendingRequest { set; get; }
        public DbSet<AcceptedPending> AcceptedPending { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserKey>().HasMany(e => e.Keys).WithOne(e => e.UserKey).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Key>().ToTable("Key");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserSignedDocument>().ToTable("UserSignedDocument");
            modelBuilder.Entity<UserUnsignedDocument>().ToTable("UserUnsignedDocument");
            modelBuilder.Entity<SignedDocument>().ToTable("SignedDocument");
            modelBuilder.Entity<UnsignedDocument>().ToTable("UnsignedDocument");
            modelBuilder.Entity<PendingRequest>().ToTable("PendingRequest");
            modelBuilder.Entity<AcceptedPending>().ToTable("AcceptedPending");
        }

    }
}
