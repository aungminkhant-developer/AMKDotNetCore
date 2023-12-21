using Microsoft.AspNetCore.Mvc;

namespace AMKDotNetCore.MvcApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }

        public IActionResult HorizontalBarChart()
        {
            return View();
        }
    }
}
