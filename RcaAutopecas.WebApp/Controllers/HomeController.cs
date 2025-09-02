using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; // ADICIONE ESTE USING
using Microsoft.AspNetCore.Mvc;
using RcaAutopecas.WebApp.Models; // ADICIONE ESTE USING
using System.Threading.Tasks; // ADICIONE ESTE USING

namespace RcaAutopecas.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        // Injeta o UserManager para poder buscar os dados do usuário
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Busca o usuário completo logado
            var user = await _userManager.GetUserAsync(User);

            // Passa o nome da empresa para a View
            ViewData["NomeDaEmpresa"] = user?.NomeDaEmpresa;

            return View();
        }
    }
}
