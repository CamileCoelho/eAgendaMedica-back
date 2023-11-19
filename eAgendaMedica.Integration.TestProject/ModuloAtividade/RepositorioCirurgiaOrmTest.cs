using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.ModuloAtividade;

namespace eAgendaMedica.Integration.TestProject.ModuloAtividade
{
    [TestClass]
    public class RepositorioCirurgiaOrmTest : RepositorioBaseTest
    {
        Cirurgia cirurgiaInserir;
        Cirurgia cirurgiaEditar;
        Cirurgia cirurgiaExcluir;

        public RepositorioCirurgiaOrmTest()
        {
            cirurgiaInserir = new Cirurgia("", DateTime.Now, TimeSpan.FromHours(8), TimeSpan.FromHours(12), new List<Medico>());
            cirurgiaEditar = new Cirurgia("", DateTime.Now, TimeSpan.FromHours(14), TimeSpan.FromHours(18), new List<Medico>());
            cirurgiaExcluir = new Cirurgia("", DateTime.Now, TimeSpan.FromHours(20), TimeSpan.FromHours(22), new List<Medico>());
        }

        [TestMethod]
        public async Task Deve_inserir_Cirurgia()
        {
            // Arrange

            var repositorio = new RepositorioCirurgiaOrm(dbContext);

            // Act

            await repositorio.InserirAsync(cirurgiaInserir);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(cirurgiaInserir.Id);

            Assert.IsNotNull(resposta);
            Assert.AreEqual(cirurgiaInserir.Id, resposta.Id);
        }

        [TestMethod]
        public void Deve_editar_cirurgia()
        {
            // Arrange
            var repositorio = new RepositorioCirurgiaOrm(dbContext);

            repositorio.InserirAsync(cirurgiaEditar).Wait();

            dbContext.SaveChanges();

            // Act

            cirurgiaEditar.HoraInicio = TimeSpan.FromHours(10);
            cirurgiaEditar.HoraTermino = TimeSpan.FromHours(14);

            repositorio.Editar(cirurgiaEditar);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(cirurgiaEditar.Id);

            Assert.AreEqual(TimeSpan.FromHours(10), resposta.HoraInicio);
            Assert.AreEqual(TimeSpan.FromHours(14), resposta.HoraTermino);
            //fazer algo que verifique se o medico 3 foi removido e o medico 1 foi adicionado 
        }

        [TestMethod]
        public void Deve_excluir_cirurgia()
        {
            // Arrange

            var repositorio = new RepositorioCirurgiaOrm(dbContext);

            repositorio.InserirAsync(cirurgiaExcluir).Wait();

            dbContext.SaveChanges();

            // Act

            repositorio.Excluir(cirurgiaExcluir);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(cirurgiaExcluir.Id);

            Assert.IsNull(resposta);
        }
    }
}
