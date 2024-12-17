using GayathrisDeliciousPie.Models;
using Microsoft.AspNetCore.Mvc;

namespace GayathrisDeliciousPie.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View(_categoryRepository.AllCategories);
        }
    }
}
