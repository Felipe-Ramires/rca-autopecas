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
                .Include(u => u.Vendedor)
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Challenge();
            }

            var viewModel = new HomeViewModel();
            string greeting;

            if (await _userManager.IsInRoleAsync(user, "Cliente") && user.Cliente != null)
            {
                greeting = $"Olá, {user.Cliente.NomeFantasia}!";
                viewModel.Perfil = new PerfilAbaViewModel
                {
                    Nome = user.Cliente.NomeFantasia,
                    Email = user.Email,
                    Documento = user.Cliente.CNPJ,
                    Telefone = user.Cliente.Telefone,
                    TipoPerfil = "Cliente"
                };
                viewModel.PodeEditarPerfil = true;
            }
            else if ((await _userManager.IsInRoleAsync(user, "Vendedor") || await _userManager.IsInRoleAsync(user, "AdminVendedor")) && user.Vendedor != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.Contains("AdminVendedor") ? "Admin Vendedor" : "Vendedor";
                
                greeting = $"Olá, {user.Vendedor.Nome}!";
                viewModel.Perfil = new PerfilAbaViewModel
                {
                    Nome = user.Vendedor.Nome,
                    Email = user.Email,
                    Documento = "N/A",
                    Telefone = "N/A",
                    TipoPerfil = role
                };
                viewModel.PodeEditarPerfil = false;
            }
            else
            {
                greeting = "Olá!";
                // Populate with default/empty data to avoid null reference in the view
                viewModel.Perfil = new PerfilAbaViewModel { Nome = "Usuário", Email = user.Email, TipoPerfil = "Indefinido" };
                viewModel.PodeEditarPerfil = false;
            }

            ViewData["Greeting"] = greeting;
            return View(viewModel);
        }
    }
}
