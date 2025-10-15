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
                                     .Include(u => u.Cliente)
                                     .ThenInclude(c => c.Endereco)
                                     .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            if (user == null || user.Cliente == null)
            {
                return NotFound();
            }

            var cliente = user.Cliente;
            var viewModel = new ProfileViewModel
            {
                NomeFantasia = cliente.NomeFantasia,
                RazaoSocial = cliente.RazaoSocial,
                CNPJ = cliente.CNPJ,
                Telefone = cliente.Telefone,
                Email = user.Email,
                RamoDeAtividade = cliente.RamoDeAtividade,
                CEP = cliente.Endereco?.CEP,
                Logradouro = cliente.Endereco?.Logradouro,
                Numero = cliente.Endereco?.Numero,
                Complemento = cliente.Endereco?.Complemento,
                Bairro = cliente.Endereco?.Bairro,
                Localidade = cliente.Endereco?.Localidade,
                UF = cliente.Endereco?.UF
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
                var user = await _context.Users.Include(u => u.Cliente).FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));
                if (user == null || user.Cliente == null) return NotFound();

                var cliente = user.Cliente;
                cliente.NomeFantasia = model.NomeFantasia;
                cliente.RazaoSocial = model.RazaoSocial;
                cliente.CNPJ = model.CNPJ;
                cliente.Telefone = model.Telefone;
                cliente.RamoDeAtividade = model.RamoDeAtividade;

                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Dados da empresa atualizados com sucesso!";
                return RedirectToAction("Index");
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
                var user = await _context.Users.Include(u => u.Cliente).ThenInclude(c => c.Endereco).FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));
                if (user == null || user.Cliente == null) return NotFound();

                if (user.Cliente.Endereco == null)
                {
                    user.Cliente.Endereco = new Endereco();
                }

                user.Cliente.Endereco.CEP = model.CEP;
                user.Cliente.Endereco.Logradouro = model.Logradouro;
                user.Cliente.Endereco.Numero = model.Numero;
                user.Cliente.Endereco.Complemento = model.Complemento;
                user.Cliente.Endereco.Bairro = model.Bairro;
                user.Cliente.Endereco.Localidade = model.Localidade;
                user.Cliente.Endereco.UF = model.UF;

                _context.Update(user.Cliente.Endereco);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Endereço atualizado com sucesso!";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}