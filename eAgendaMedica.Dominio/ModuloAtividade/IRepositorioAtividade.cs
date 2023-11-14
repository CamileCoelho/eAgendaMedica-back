namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public interface IRepositorioAtividade : IRepositorio<Atividade>
    {
        List<Atividade> SelecionarAtividadesFuturas(DateTime dataInicial, DateTime dataFinal);

        List<Atividade> SelecionarAtividadesPassadas(DateTime dataDeHoje);
    }
}
