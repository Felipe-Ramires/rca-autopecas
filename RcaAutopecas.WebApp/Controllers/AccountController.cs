using Microsoft.AspNetCore.Mvc;
using RcaAutopecas.WebApp.ViewModels;

namespace RcaAutopecas.WebApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Lógica de autenticação com o banco de dados
                return RedirectToAction("Index", "Dashboard"); // Redireciona para a página principal após o login
            }
            return View(model);
        }

        // Adicione estes métodos ao seu AccountController.cs

        // GET: /Account/Register
        // Ação para MOSTRAR a página de cadastro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        // Ação para PROCESSAR os dados do formulário de cadastro
        [HttpPost]
        [ValidateAntiForgeryToken] // Importante para segurança
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Adicione sua lógica para criar o usuário no banco de dados.
                // Se o cadastro for bem-sucedido, você pode redirecionar para o login
                // ou para a página principal do sistema.

                // Por enquanto, vamos apenas redirecionar para a página de Login
                // para que o novo usuário possa entrar.
                return RedirectToAction("Login");
            }

            // Se o modelo não for válido (algum campo está com erro),
            // retorna para a tela de cadastro para exibir as mensagens de erro.
            return View(model);
        }

    }
}

