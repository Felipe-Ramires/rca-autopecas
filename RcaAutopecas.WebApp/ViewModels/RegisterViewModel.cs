using System.ComponentModel.DataAnnotations;

namespace RcaAutopecas.WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        [Display(Name = "Nome da Empresa")]
        public string NomeDaEmpresa { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "A rua é obrigatória.")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação de senha não correspondem.")]
        public string ConfirmarSenha { get; set; }
    }
}
