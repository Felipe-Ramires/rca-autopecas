using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RcaAutopecas.WebApp.Data;
using RcaAutopecas.WebApp.Models;
using RcaAutopecas.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RcaAutopecas.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Context _context;

        public HomeController(UserManager<ApplicationUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _context.Users
                .Include(u => u.Vendedor)  // Eagerly load the Vendedor profile
                .Include(u => u.Cliente)   // Eagerly load the Cliente profile
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "Cliente"))
                {
                    ViewData["Greeting"] = $"Olá, {user.Cliente?.NomeFantasia}!";
                }
                else if (await _userManager.IsInRoleAsync(user, "Vendedor") || await _userManager.IsInRoleAsync(user, "AdminVendedor"))
                {
                    // Now we use the included Vendedor object, no separate query needed
                    ViewData["Greeting"] = $"Olá, {user.Vendedor?.Nome}!";
                }
                else
                {
                    ViewData["Greeting"] = "Olá!";
                }
            }

            return View();
        }
    }
}
