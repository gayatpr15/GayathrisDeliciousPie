using GayathrisDeliciousPie.Models;

namespace GayathrisDeliciousPie.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
        public HomeViewModel(IEnumerable<Pie> pies) {
        PiesOfTheWeek = pies;
        }
    }
}
