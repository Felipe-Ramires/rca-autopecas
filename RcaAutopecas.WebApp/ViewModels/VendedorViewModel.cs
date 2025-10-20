using System.ComponentModel.DataAnnotations;

namespace RcaAutopecas.WebApp.ViewModels
{
    public class VendedorViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "É Administrador?")]
        public bool IsAdmin { get; set; }
    }
}
