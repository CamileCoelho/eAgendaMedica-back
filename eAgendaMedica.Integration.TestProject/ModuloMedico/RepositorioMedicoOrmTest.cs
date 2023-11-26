using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.ModuloMedico;

namespace eAgendaMedica.Integration.TestProject.ModuloMedico
{
    [TestClass]
    public class RepositorioMedicoOrmTest : RepositorioBaseTest
    {
        Medico medicoInserir;
        Medico medicoEditar;
        Medico medicoExcluir;

        public RepositorioMedicoOrmTest()
        {
            medicoInserir = new Medico("Camile", "65489-SP", "Dev");
            medicoEditar = new Medico("Tales", "12345-SC", "Dev");
            medicoExcluir = new Medico("Rech", "85496-SC", "Dev");
        }

        [TestMethod]
        public async Task Deve_inserir_Medico()
        {
            // Arrange

            var repositorio = new RepositorioMedicoOrm(dbContext);
            
            // Act

            await repositorio.InserirAsync(medicoInserir);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(medicoInserir.Id);

            Assert.IsNotNull(resposta);
            Assert.AreEqual(medicoInserir.Id, resposta.Id);
        }

        [TestMethod]
        public void Deve_editar_medico()
        {
            // Arrange

            var repositorio = new RepositorioMedicoOrm(dbContext);

            repositorio.InserirAsync(medicoEditar).Wait();

            dbContext.SaveChanges();

            // Act

            medicoEditar.Nome = "Camile Editado";
            medicoEditar.Especialidade = "Programação";

            repositorio.Editar(medicoEditar);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(medicoEditar.Id);

            Assert.AreEqual("Camile Editado", resposta.Nome);
            Assert.AreEqual("Programação", resposta.Especialidade);
        }

        [TestMethod]
        public void Deve_excluir_medico()
        {
            // Arrange

            var repositorio = new RepositorioMedicoOrm(dbContext);

            repositorio.InserirAsync(medicoExcluir).Wait();

            dbContext.SaveChanges();

            // Act

            repositorio.Excluir(medicoExcluir);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(medicoExcluir.Id);

            Assert.IsNull(resposta);
        }
    }
}
