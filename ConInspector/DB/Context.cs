using ConInspector.DB.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ConInspector.DB
{
    public class Context : DbContext
    {
        public DbSet<Voltage> Voltages { get; set; }
        public DbSet<Current> Currents { get; set; }
        public DbSet<Power> Powers { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=inspector.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            base.OnModelCreating(modelBuilder);
        }
    }


}
