using Microsoft.EntityFrameworkCore;
using VizitkaOnline.Models;

namespace VizitkaOnline.AppData
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<AccountModel> AccountModel { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkTempBd;Trusted_Connection=True;");
        }
    }
}