using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LinkShortener.Entities;

namespace LinkShortener.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Link> Link {  get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });


            modelBuilder.Entity<Link>().HasIndex(d => d.Id).IsUnique();

            modelBuilder.Entity<Link>().Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired(true).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Link>().Property(x => x.UpdatedAt).HasColumnType("datetime2").IsRequired(true).HasDefaultValue(DateTime.Now);


            base.OnModelCreating(modelBuilder);

        }
     }
}
