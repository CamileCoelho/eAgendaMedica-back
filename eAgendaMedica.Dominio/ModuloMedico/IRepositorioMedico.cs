using eAgendaMedica.Dominio.Compartilhado;

namespace eAgendaMedica.Dominio.ModuloMedico
{
    public interface IRepositorioMedico : IRepositorio<Medico>
    {
        List<Medico> SelecionarMuitos(List<Guid> medicoIds);
    }
}
