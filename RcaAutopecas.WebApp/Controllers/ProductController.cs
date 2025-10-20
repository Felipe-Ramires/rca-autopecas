using Microsoft.AspNetCore.Mvc;

namespace RcaAutopecas.WebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
