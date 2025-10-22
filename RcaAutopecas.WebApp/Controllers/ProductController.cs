using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RcaAutopecas.WebApp.Data;
using RcaAutopecas.WebApp.Models;
using RcaAutopecas.WebApp.ViewModels;

namespace RcaAutopecas.WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Context _context;

        public ProductController(UserManager<ApplicationUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _context.Produtos.ToListAsync();

            var viewModel = new ProdutoViewModel
            {
                ListaProdutos = produtos,
            };

            return View(viewModel);
        }
    }
}
 