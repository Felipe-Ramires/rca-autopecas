using Microsoft.AspNetCore.Identity;

namespace RcaAutopecas.WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NomeDaEmpresa { get; set; }
        public string CNPJ { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Numero { get; set; }
        public string Rua { get; set; }
    }
}
