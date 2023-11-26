using System.ComponentModel.DataAnnotations;

namespace eAgendaMedica.WebApi.ViewModels.ModuloAutenticacao
{
    public class AutenticarUsuarioViewModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
