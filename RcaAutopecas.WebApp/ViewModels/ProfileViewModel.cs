using System.ComponentModel.DataAnnotations;

namespace RcaAutopecas.WebApp.ViewModels
{
    public class ProfileViewModel
    {
        // Aba: Dados da Empresa
        [Display(Name = "Nome Fantasia")]
        public string? NomeFantasia { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Razão Social")]
        public string? RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? CNPJ { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Ramo de Atividade")]
        public string? RamoDeAtividade { get; set; }

        // Aba: Endereço
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? CEP { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Logradouro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Número")]
        public string? Numero { get; set; }

        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Bairro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Cidade")]
        public string? Localidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Estado")]
        public string? UF { get; set; }
    }
}
