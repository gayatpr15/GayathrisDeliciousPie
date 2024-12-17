using Microsoft.EntityFrameworkCore;

namespace GayathrisDeliciousPie.Models
{
    public class GayathrisPieShopDbContext : DbContext
    {
        public GayathrisPieShopDbContext(DbContextOptions<GayathrisPieShopDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
