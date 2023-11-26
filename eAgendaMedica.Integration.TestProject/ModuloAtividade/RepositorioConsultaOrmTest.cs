using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.ModuloAtividade;

namespace eAgendaMedica.Integration.TestProject.ModuloAtividade
{
    [TestClass]
    public class RepositorioConsultaOrmTest : RepositorioBaseTest
    {
        Consulta consultaInserir;
        Consulta consultaEditar;
        Consulta consultaExcluir;

        public RepositorioConsultaOrmTest()
        {
            var medico = new Medico("Médico Consulta", "96478-PA", "Especialidade");

            consultaInserir = new Consulta("", DateTime.Now, TimeSpan.FromHours(8), TimeSpan.FromHours(9), medico);
            consultaEditar = new Consulta("", DateTime.Now, TimeSpan.FromHours(10), TimeSpan.FromHours(12), medico);
            consultaExcluir = new Consulta("", DateTime.Now, TimeSpan.FromHours(14), TimeSpan.FromHours(15), medico);
        }

        [TestMethod]
        public async Task Deve_inserir_Consulta()
        {
            // Arrange

            var repositorio = new RepositorioConsultaOrm(dbContext);

            // Act

            await repositorio.InserirAsync(consultaInserir);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(consultaInserir.Id);

            Assert.IsNotNull(resposta);
            Assert.AreEqual(consultaInserir.Id, resposta.Id);
        }

        [TestMethod]
        public void Deve_editar_consulta()
        {
            // Arrange

            var repositorio = new RepositorioConsultaOrm(dbContext);

            repositorio.InserirAsync(consultaEditar).Wait();

            dbContext.SaveChanges();

            // Act

            consultaEditar.HoraInicio = TimeSpan.FromHours(13);
            consultaEditar.HoraTermino = TimeSpan.FromHours(15);

            repositorio.Editar(consultaEditar);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(consultaEditar.Id);

            Assert.AreEqual(TimeSpan.FromHours(13), resposta.HoraInicio);
            Assert.AreEqual(TimeSpan.FromHours(15), resposta.HoraTermino);
        }

        [TestMethod]
        public void Deve_excluir_consulta()
        {
            // Arrange

            var repositorio = new RepositorioConsultaOrm(dbContext);

            repositorio.InserirAsync(consultaExcluir).Wait();

            dbContext.SaveChanges();

            // Act

            repositorio.Excluir(consultaExcluir);

            dbContext.SaveChanges();

            // Assert

            var resposta = repositorio.SelecionarPorId(consultaExcluir.Id);

            Assert.IsNull(resposta);
        }
    }
}

            