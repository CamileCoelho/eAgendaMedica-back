using System.ComponentModel.DataAnnotations;

namespace eAgendaMedica.WebApi.ViewModels.ModuloAutenticacao
{
    public class RegistrarUsuarioViewModel
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}
