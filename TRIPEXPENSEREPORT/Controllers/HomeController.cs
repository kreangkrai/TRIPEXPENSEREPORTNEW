using Microsoft.AspNetCore.Mvc;

namespace TRIPEXPENSEREPORT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
