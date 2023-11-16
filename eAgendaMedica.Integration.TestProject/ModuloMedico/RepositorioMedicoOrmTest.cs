using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.ModuloMedico;

namespace eAgendaMedica.Integration.TestProject.ModuloMedico
{
    [TestClass]
    public class RepositorioMedicoOrmTest : RepositorioBaseTest
    {
        private readonly eAgendaMedicaDbContext dbContext;
        public RepositorioMedicoOrmTest() 
        {
            dbContext = CreateDbContext();
        }

        [TestMethod]
        public void Deve_inserir_Medico()
        {
            //arrange
            Medico novoMedico = new Medico("Camile", "65489-SC", "Dev");

            var repositorio = new RepositorioMedicoOrm(dbContext);

            //action
            repositorio.InserirAsync(novoMedico);
            dbContext.SaveChanges();

            //assert
            Medico resposta = repositorio.SelecionarPorId(novoMedico.Id);

            Assert.IsNotNull(resposta);
            Assert.AreEqual(novoMedico.Id, resposta.Id);
        }
    }
}
