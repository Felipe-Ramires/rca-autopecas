using System.ComponentModel.DataAnnotations;

namespace RcaAutopecas.WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação de senha não correspondem.")]
        public string ConfirmarSenha { get; set; }

        // --- Dados da Empresa ---
        [Required(ErrorMessage = "A Razão Social é obrigatória.")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O Nome Fantasia é obrigatório.")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        public string CNPJ { get; set; }

        [Display(Name = "Ramo de Atividade")]
        public string? RamoDeAtividade { get; set; }
        
        public string? Telefone { get; set; }

        // --- Endereço ---
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string CEP { get; set; }

        [Display(Name = "Logradouro (Rua, Av.)")]
        public string? Logradouro { get; set; }

        [Display(Name = "Bairro")]
        public string? Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string? Localidade { get; set; }

        [Display(Name = "Estado (UF)")]
        public string? UF { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        public string? Complemento { get; set; }
    }
}