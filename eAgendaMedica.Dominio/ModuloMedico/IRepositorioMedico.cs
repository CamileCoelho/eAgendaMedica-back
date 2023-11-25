namespace eAgendaMedica.Dominio.ModuloMedico
{
    public interface IRepositorioMedico : IRepositorio<Medico>
    {
        List<Medico> SelecionarMuitos(List<Guid> medicoIds);
        Medico SelecionarPorCrm(string crm);
        bool MedicoNaoPodeSerExcluido(Medico medico);
    }
}
