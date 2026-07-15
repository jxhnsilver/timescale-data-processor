using Microsoft.EntityFrameworkCore;
using TimescaleDataProcessor.Api.Data.Configurations;
using TimescaleDataProcessor.Api.Entities;

namespace TimescaleDataProcessor.Api.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ValueRecord> Values { get; set; }
        public DbSet<IntegralResult> Results { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ValueRecordConfiguration());
            modelBuilder.ApplyConfiguration(new IntegralResultConfiguration());
        }
    }
}
