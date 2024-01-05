using CityFlims.Entity;
using CityFlims.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CityFlims.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CityfilmsDataContext _context = new CityfilmsDataContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult _Layout()
        {
            var layoutModel = new LayoutModel()
            {
                LogoLink = _context.Images.Where(x => x.ImageTypeId == 1).Select(x => x.ImageLocation).FirstOrDefault() == "" ? "images/city1.jpg": _context.Images.Where(x => x.ImageTypeId == 1).Select(x => x.ImageLocation).FirstOrDefault(),
            };
            //if(layoutModel.LogoLink == "")
            //{
            //    layoutModel.LogoLink = "images/city1.jpg";
            //}
            return View(layoutModel);
        }
    }
}
