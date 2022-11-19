using Microsoft.EntityFrameworkCore;
using PaparaThirdWeek.Data.Configurations;
using PaparaThirdWeek.Domain.Entities;
using System;

namespace PaparaThirdWeek.Data.Context
{
    public class PaparaAppDbContext : DbContext
    {
        public PaparaAppDbContext(DbContextOptions<PaparaAppDbContext> options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            //DbContext'deki tüm table configurationları bulup register.
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaparaAppDbContext).Assembly);

        }
        public DbSet<Company> Companies { get; set; }

        internal string GetConnectionString(string v)
        {
            throw new NotImplementedException();
        }
    }
}
