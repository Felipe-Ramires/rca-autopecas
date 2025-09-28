using Microsoft.AspNetCore.Identity;

namespace RcaAutopecas.WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string RamoDeAtividade { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
