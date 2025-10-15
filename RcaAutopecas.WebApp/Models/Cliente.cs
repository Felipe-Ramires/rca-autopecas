using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RcaAutopecas.WebApp.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeFantasia { get; set; } = string.Empty;
        
        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required]
        public string CNPJ { get; set; } = string.Empty;

        [Required]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        public string RamoDeAtividade { get; set; } = string.Empty;
        
        public virtual Endereco? Endereco { get; set; }

        // Foreign key for ApplicationUser
        public string ApplicationUserId { get; set; } = string.Empty;

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
