namespace eAgendaMedica.Dominio.Compartilhado
{
    public interface IContextoPersistencia
    {
        void DesfazerAlteracoes();
        void GravarDados();
        Task<bool> GravarDadosAsync();
    }
}
