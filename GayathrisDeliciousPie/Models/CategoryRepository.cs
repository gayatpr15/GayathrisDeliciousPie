
namespace GayathrisDeliciousPie.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GayathrisPieShopDbContext _gayathrisPieShopDbContext;
        public CategoryRepository(GayathrisPieShopDbContext gayathrisPieShopDbContext)
        {
            _gayathrisPieShopDbContext = gayathrisPieShopDbContext;
        }
        public IEnumerable<Category> AllCategories =>
           _gayathrisPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
