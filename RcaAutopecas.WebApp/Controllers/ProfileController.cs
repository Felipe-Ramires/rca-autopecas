using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RcaAutopecas.WebApp.Models;
using System.Threading.Tasks;

namespace RcaAutopecas.WebApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
              
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewData["NomeFantasia"] = user?.NomeFantasia;
            
            return View();
        }

    }
}
