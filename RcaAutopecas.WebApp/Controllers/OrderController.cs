using AspNetCoreGeneratedDocument;
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
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Context _context;

        public OrderController(UserManager<ApplicationUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _context.Users
                .Include(u => u.Vendedor)
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if(await _userManager.IsInRoleAsync(user, "Vendedor") || await _userManager.IsInRoleAsync(user, "AdminVendedor"))
            {
                var ViewName = "PedidosVendedor";
                return View(ViewName);
            }

            return View();
        }
    }
}
