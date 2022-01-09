using catalogapp.Models;
using Microsoft.EntityFrameworkCore;

namespace catalogapp
{
    public class CatalogDbRepository: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public CatalogDbRepository(DbContextOptions options):base(options) {
        }
    }
}