using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RcaAutopecas.WebApp.Data;
using RcaAutopecas.WebApp.Models;
using RcaAutopecas.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RcaAutopecas.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Context _context;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, Context context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Verifica se o usuário é um cliente
                    var isCliente = await _userManager.IsInRoleAsync(user, "Cliente");
                    if (isCliente)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Senha, isPersistent: false, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Acesso negado. Verifique se você está na página de login correta.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LoginVendedor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginVendedor(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Verifica se o usuário é um vendedor
                    var isVendedor = await _context.Vendedores.AnyAsync(v => v.ApplicationUserId == user.Id);
                    if (isVendedor)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Senha, isPersistent: false, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            // TODO: Redirect to a seller-specific dashboard
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Acesso negado. Verifique suas credenciais ou contate o suporte.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    // Assign the 'Cliente' role
                    await _userManager.AddToRoleAsync(user, "Cliente");

                    // Create the Cliente profile
                    var cliente = new Cliente
                    {
                        ApplicationUserId = user.Id,
                        NomeFantasia = model.NomeFantasia,
                        RazaoSocial = model.RazaoSocial,
                        CNPJ = model.CNPJ,
                        Telefone = model.Telefone,
                        RamoDeAtividade = model.RamoDeAtividade,
                        Endereco = new Endereco
                        {
                            CEP = model.CEP,
                            Logradouro = model.Logradouro,
                            Numero = model.Numero,
                            Complemento = model.Complemento,
                            Bairro = model.Bairro,
                            Localidade = model.Localidade,
                            UF = model.UF
                        }
                    };
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();

                    // Após o cadastro, redirecione para o login para garantir a segurança
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
