using Microsoft.EntityFrameworkCore;

namespace VizitkaOnline.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> userModel { get; set; }
        public DbSet<AccountModel> accountModel { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {          
            Database.EnsureCreated();
        }
    }
}