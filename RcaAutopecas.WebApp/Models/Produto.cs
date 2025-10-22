using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RcaAutopecas.WebApp.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Rca { get; set; } = string.Empty;        
        public string Fabricante { get; set; } = string.Empty;  
        public decimal Venda { get; set; }   
        public decimal Custo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Familia { get; set; } = string.Empty;
        public int Estoque { get; set; }
    }
}
