
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RcaAutopecas.WebApp.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [Required]
        public DateTime DataPedido { get; set; }

        [Required]
        public string Status { get; set; }

        public virtual ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();
    }
}
