using MarsRover.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarsRover.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Rover> Rover { get; set; }
        public DbSet<MarsGrid> Grid { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Rover>()
            .HasOne(a => a.MarsGrid)
            .WithMany(u => u.Rovers)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
