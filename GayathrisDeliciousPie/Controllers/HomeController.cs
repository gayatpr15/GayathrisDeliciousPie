using GayathrisDeliciousPie.Models;
using GayathrisDeliciousPie.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GayathrisDeliciousPie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;   
        }
        public IActionResult Index()
        {
            var piesOfTheweek = _pieRepository.PiesOfTheWeek;
            var homeViewModel = new HomeViewModel(piesOfTheweek);
            return View(homeViewModel);
        }
    }
}
