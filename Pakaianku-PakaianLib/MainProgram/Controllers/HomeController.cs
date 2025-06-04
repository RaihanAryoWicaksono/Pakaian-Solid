using Microsoft.AspNetCore.Mvc;

namespace Pakaianku.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}