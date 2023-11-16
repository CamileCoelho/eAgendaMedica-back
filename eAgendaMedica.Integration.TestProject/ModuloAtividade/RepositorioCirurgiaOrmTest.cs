namespace eAgendaMedica.Integration.TestProject.ModuloAtividade
{
    [TestClass]
    public class RepositorioCirurgiaOrmTest : RepositorioBaseTest
    {
        private readonly eAgendaMedicaDbContext dbContext;
        public RepositorioCirurgiaOrmTest()
        {
            dbContext = CreateDbContext();
        }
    }
}

            