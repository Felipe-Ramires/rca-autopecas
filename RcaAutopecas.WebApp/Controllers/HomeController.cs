using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RcaAutopecas.WebApp.Data;
using RcaAutopecas.WebApp.Models;
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "Cliente"))
                {
                    var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
                    ViewData["Greeting"] = $"Olá, {cliente?.NomeFantasia}!";
                }
                else if (await _userManager.IsInRoleAsync(user, "Vendedor") || await _userManager.IsInRoleAsync(user, "AdminVendedor"))
                {
                    var vendedor = await _context.Vendedores.FirstOrDefaultAsync(v => v.ApplicationUserId == userId);
                    ViewData["Greeting"] = $"Olá, {vendedor?.Nome}!";
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
