namespace RcaAutopecas.WebApp.ViewModels
{
    public class HomeViewModel
    {
        public PerfilAbaViewModel Perfil { get; set; }
        public bool PodeEditarPerfil { get; set; }

        public HomeViewModel()
        {
            Perfil = new PerfilAbaViewModel();
        }
    }

    public class PerfilAbaViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string TipoPerfil { get; set; }
    }
}
