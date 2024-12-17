
using Microsoft.EntityFrameworkCore;

namespace GayathrisDeliciousPie.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly GayathrisPieShopDbContext _gayathrisPieShopDbContext;
        public PieRepository(GayathrisPieShopDbContext gayathrisPieShopDbContext) {
            _gayathrisPieShopDbContext = gayathrisPieShopDbContext;
        }
        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _gayathrisPieShopDbContext.Pies.Include(p => p.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _gayathrisPieShopDbContext.Pies.Include(p => p.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return _gayathrisPieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
