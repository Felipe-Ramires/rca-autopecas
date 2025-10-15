using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RcaAutopecas.WebApp.Models
{
    public class Vendedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        // Foreign key for ApplicationUser
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
