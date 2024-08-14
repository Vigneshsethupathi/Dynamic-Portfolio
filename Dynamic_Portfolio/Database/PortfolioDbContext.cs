using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Database
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {

        }
        public DbSet<Project> projects { get; set; }
        public DbSet<Skills> skills { get; set; }
        public DbSet<Users> users { get; set; }
    }

}
