using DziennikUcznia.Services;
using Microsoft.AspNetCore.Mvc;

namespace DziennikUcznia.Controllers
{
    public class DziennikController : Controller
    {
        
        private readonly TestService _testService;
        public DziennikController(TestService testService) 
        {
            _testService = testService;
        }
        public IActionResult Index()
        {
            return View("Dziennik");
        }

        
        [HttpGet("WWW")]
        public ActionResult AA()
        {
            _testService.Test();
            return Ok();
        }
    }
}
