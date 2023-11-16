namespace eAgendaMedica.Integration.TestProject.ModuloAtividade
{
    [TestClass]
    public class RepositorioConsultaOrmTest : RepositorioBaseTest
    {
        private readonly eAgendaMedicaDbContext dbContext;
        public RepositorioConsultaOrmTest()
        {
            dbContext = CreateDbContext();
        }
    }
}

            