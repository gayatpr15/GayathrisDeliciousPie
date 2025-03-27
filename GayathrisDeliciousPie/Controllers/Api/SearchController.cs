using GayathrisDeliciousPie.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GayathrisDeliciousPie.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [HttpGet()]
        public IActionResult GetAll() {
            var allPies = _pieRepository.AllPies;
            return Ok(allPies);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var allPies = _pieRepository.GetPieById(id);
      
            if(allPies is null ) { return NotFound(); }
            return Ok(allPies);
        }
        [HttpPost]
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
            IEnumerable<Pie> filteredPies = new List<Pie>();
            if (!string.IsNullOrEmpty(searchQuery))
                filteredPies = _pieRepository.SearchPies(searchQuery);
            if (!filteredPies.Any()) { return NotFound(); }
            return new JsonResult(filteredPies);
        }
    }
}
