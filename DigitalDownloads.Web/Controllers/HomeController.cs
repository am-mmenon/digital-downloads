using DigitalDownloads.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDownloads.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)=>_context = context;
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
