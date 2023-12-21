using Microsoft.AspNetCore.Mvc;

namespace AMKDotNetCore.MvcApp.Controllers
{
    public class HighChartsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
