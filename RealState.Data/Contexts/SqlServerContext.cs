using Microsoft.EntityFrameworkCore;
using RealState.Common.Entities;

namespace RealState.Data.Contexts
{
    public class SqlServerContext : DbContext
    {
        public string _sqlConnectionString;

        public DbSet<Owner> Owner { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyImage> PropertyImage { get; set; }
        public DbSet<PropertyTrace> PropertyTrace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Owner
            modelBuilder.Entity<Owner>().HasKey(x => x.Id);
            modelBuilder.Entity<Owner>().HasIndex(x => x.Name).IsUnique();
            #endregion

            #region Property
            modelBuilder.Entity<Property>().HasKey(x => x.Id);
            modelBuilder.Entity<Property>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Property>().HasIndex(x => x.Address).IsUnique();
            modelBuilder.Entity<Property>().HasIndex(x => x.CodeInternal).IsUnique();
            modelBuilder.Entity<Property>().HasOne(x => x.Owner).WithMany(y => y.Properties).HasForeignKey(x => x.OwnerId);
            #endregion

            #region PropertyImage
            modelBuilder.Entity<PropertyImage>().HasKey(x => x.Id);
            modelBuilder.Entity<PropertyImage>().HasOne(x => x.Property).WithMany(y => y.PropertyImages).HasForeignKey(x => x.PropertyId);
            #endregion

            #region PropertyTrace
            modelBuilder.Entity<PropertyTrace>().HasKey(x => x.Id);
            modelBuilder.Entity<PropertyTrace>().HasOne(x => x.Property).WithMany(y => y.PropertyTraces).HasForeignKey(x => x.PropertyId);
            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqlConnectionString);
        }
    }
}
