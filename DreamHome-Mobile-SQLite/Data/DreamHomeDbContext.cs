using DreamHome_Mobile_SQLite.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamHome_Mobile_SQLite.Data
{
    /// <summary>
    /// DreamHome DbContext class
    /// </summary>
    public class DreamHomeDbContext: DbContext
    {
        public DreamHomeDbContext(DbContextOptions<DreamHomeDbContext> options) : base(options) { }

        // Define DbSets for base tables
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Staff> Staff => Set<Staff>();
        public DbSet<PrivateOwner> PrivateOwners => Set<PrivateOwner>();
        public DbSet<PropertyForRent> PropertiesForRent => Set<PropertyForRent>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Viewing> Viewings => Set<Viewing>();
        public DbSet<Registration> Registrations => Set<Registration>();


        /// <summary>
        /// Configure the mapping between the entity classes and the database schema, including tables, keys, and relationships
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table names (optional; matches the SQL)
            modelBuilder.Entity<Branch>().ToTable("Branch");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<PrivateOwner>().ToTable("PrivateOwner");
            modelBuilder.Entity<PropertyForRent>().ToTable("PropertyForRent");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Viewing>().ToTable("Viewing");
            modelBuilder.Entity<Registration>().ToTable("Registration");

            // Branch
            modelBuilder.Entity<Branch>()
                .HasKey(b => b.BranchNo);

            // Staff
            modelBuilder.Entity<Staff>()
                .HasKey(s => s.StaffNo);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Salary)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Staff)
                .HasForeignKey(s => s.BranchNo)
                .OnDelete(DeleteBehavior.Restrict);

            // PrivateOwner
            modelBuilder.Entity<PrivateOwner>()
                .HasKey(o => o.OwnerNo);

            // Client
            modelBuilder.Entity<Client>()
                .HasKey(c => c.ClientNo);

            modelBuilder.Entity<Client>()
                .Property(c => c.MaxRent)
                .HasPrecision(5, 1);

            // PropertyForRent
            modelBuilder.Entity<PropertyForRent>()
                .HasKey(p => p.PropertyNo);

            modelBuilder.Entity<PropertyForRent>()
                .Property(p => p.Rent)
                .HasPrecision(5, 1);

            modelBuilder.Entity<PropertyForRent>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Properties)
                .HasForeignKey(p => p.OwnerNo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PropertyForRent>()
                .HasOne(p => p.Staff)
                .WithMany(s => s.Properties)
                .HasForeignKey(p => p.StaffNo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PropertyForRent>()
                .HasOne(p => p.Branch)
                .WithMany(b => b.Properties)
                .HasForeignKey(p => p.BranchNo)
                .OnDelete(DeleteBehavior.Restrict);

            // Viewing (composite PK)
            modelBuilder.Entity<Viewing>()
                .HasKey(v => new { v.PropertyNo, v.ClientNo });

            modelBuilder.Entity<Viewing>()
                .HasOne(v => v.Property)
                .WithMany(p => p.Viewings)
                .HasForeignKey(v => v.PropertyNo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Viewing>()
                .HasOne(v => v.Client)
                .WithMany(c => c.Viewings)
                .HasForeignKey(v => v.ClientNo)
                .OnDelete(DeleteBehavior.Restrict);

            // Registration (composite PK)
            modelBuilder.Entity<Registration>()
                .HasKey(r => new { r.ClientNo, r.BranchNo });

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.ClientNo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Branch)
                .WithMany(b => b.Registrations)
                .HasForeignKey(r => r.BranchNo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Staff)
                .WithMany(s => s.Registrations)
                .HasForeignKey(r => r.StaffNo)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
