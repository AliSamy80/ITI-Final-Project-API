using APIFinalProject.Configurations.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace APIFinalProject.Models
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<UnitBuilding> UnitBuildings { get; set; }
        public DbSet<HistoryOfunitBuilding> HistoryOfunitBuildings { get; set; }
        public DbSet<MenuPrice> MenuPrice { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.ApplyConfiguration(new RoleConfiguration());

        }
        //protected virtual void OnModelCreating(ModelBuilder builder) {
        //    base.OnModelCreating(builder);

        //    builder.ApplyConfiguration(new RoleConfiguration());

        //    builder.Entity<Favorite>(eb =>
        //    {
        //        eb.HasNoKey();
        //    });
        //}
    }
}
