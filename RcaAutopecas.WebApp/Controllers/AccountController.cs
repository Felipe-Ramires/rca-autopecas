using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RcaAutopecas.WebApp.Models; 
using RcaAutopecas.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RcaAutopecas.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                // No login, o UserName deve ser o Email para o Identity encontrar o usuário
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
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
                // Crie um ApplicationUser e mapeie TODOS os campos
                var user = new ApplicationUser
                {
                    UserName = model.Email, // UserName é obrigatório e geralmente é o email
                    Email = model.Email,
                    NomeDaEmpresa = model.NomeDaEmpresa,
                    CNPJ = model.CNPJ,
                    CEP = model.CEP,
                    Estado = model.Estado,
                    Numero = model.Numero,
                    Rua = model.Rua
                };

                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
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
