using DigitalDownloads.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDownloads.Core.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products =>Set<Product>();
        public DbSet<Customer> Customers =>Set<Customer>();
        public DbSet<Order> Orders =>Set<Order>();
        public DbSet<DownloadLink> DownloadLinks =>Set<DownloadLink>();

    }
}
