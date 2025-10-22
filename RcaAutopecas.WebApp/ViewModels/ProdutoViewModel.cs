using RcaAutopecas.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace RcaAutopecas.WebApp.ViewModels
{
    public class ProdutoViewModel
    {
        public IEnumerable<Produto> ListaProdutos { get; set; }
    }
}
