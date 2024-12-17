using Microsoft.AspNetCore.Mvc;

namespace GayathrisDeliciousPie.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
