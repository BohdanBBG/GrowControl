using GrowControl.Models.Domain;
using Microsoft.EntityFrameworkCore;
using GrowControl.Models.Enums;
using System.Numerics;

namespace GrowControl.DbContexts
{
    public class MainContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            Plant testPlant = new Plant 
            {
                Id = 1,
                Name = "Test Plant",
                Height = 0.1,
                Price = 100,
                Substrate = Substrate.Linen,
                GerminationTime = TimeSpan.Zero,
                GrowthTime = TimeSpan.Zero,
                Planted = DateTime.Now,
            };

            modelBuilder.Entity<Plant>().HasData(new Plant[] { testPlant});

            base.OnModelCreating(modelBuilder);
        }
    }
}
