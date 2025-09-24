
using System.ComponentModel.DataAnnotations;

namespace RcaAutopecas.WebApp.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Logradouro { get; set; }

        public string? Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Localidade { get; set; }

        [Required]
        public string UF { get; set; }

        [Required]
        public string Numero { get; set; }

        // Campos adicionais da API ViaCEP que podem ser úteis
        public string? Ibge { get; set; }
        public string? Gia { get; set; }
        public string? Ddd { get; set; }
        public string? Siafi { get; set; }

        // Chave estrangeira para ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
