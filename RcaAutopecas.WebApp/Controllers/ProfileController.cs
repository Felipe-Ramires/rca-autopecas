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
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Context _context;

        public ProfileController(UserManager<ApplicationUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.Users
                                     .Include(u => u.Endereco)
                                     .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new ProfileViewModel
            {
                NomeFantasia = user.NomeFantasia,
                RazaoSocial = user.RazaoSocial,
                CNPJ = user.CNPJ,
                Telefone = user.Telefone,
                Email = user.Email,
                RamoDeAtividade = user.RamoDeAtividade,
                CEP = user.Endereco?.CEP,
                Logradouro = user.Endereco?.Logradouro,
                Numero = user.Endereco?.Numero,
                Complemento = user.Endereco?.Complemento,
                Bairro = user.Endereco?.Bairro,
                Localidade = user.Endereco?.Localidade,
                UF = user.Endereco?.UF
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCompanyData(ProfileViewModel model)
        {
            ModelState.Remove("CEP");
            ModelState.Remove("Logradouro");
            ModelState.Remove("Numero");
            ModelState.Remove("Complemento");
            ModelState.Remove("Bairro");
            ModelState.Remove("Localidade");
            ModelState.Remove("UF");
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return NotFound();

                user.NomeFantasia = model.NomeFantasia;
                user.RazaoSocial = model.RazaoSocial;
                user.CNPJ = model.CNPJ;
                user.Telefone = model.Telefone;
                user.RamoDeAtividade = model.RamoDeAtividade;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Dados da empresa atualizados com sucesso!";
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddressData(ProfileViewModel model)
        {
            ModelState.Remove("RazaoSocial");
            ModelState.Remove("CNPJ");
            ModelState.Remove("Telefone");
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                var user = await _context.Users.Include(u => u.Endereco).FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));
                if (user == null) return NotFound();

                if (user.Endereco == null)
                {
                    user.Endereco = new Endereco();
                }

                user.Endereco.CEP = model.CEP;
                user.Endereco.Logradouro = model.Logradouro;
                user.Endereco.Numero = model.Numero;
                user.Endereco.Complemento = model.Complemento;
                user.Endereco.Bairro = model.Bairro;
                user.Endereco.Localidade = model.Localidade;
                user.Endereco.UF = model.UF;

                _context.Update(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Endereço atualizado com sucesso!";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}