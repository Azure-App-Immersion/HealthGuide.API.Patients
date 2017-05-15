using Microsoft.AspNetCore.Mvc;

namespace HealthGuide.API.Patients.Controllers
{
    public class HomeController : Controller 
    {        
        public IActionResult Index()
        {
            return View();
        }
    }
}