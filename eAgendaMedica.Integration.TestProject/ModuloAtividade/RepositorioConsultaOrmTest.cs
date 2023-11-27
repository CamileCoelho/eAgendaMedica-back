using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.ModuloAtividade;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using FizzWare.NBuilder;
using FluentAssertions;

namespace eAgendaMedica.Integration.TestProject.ModuloAtividade
{
    [TestClass]
    public class RepositorioConsultaOrmTest : RepositorioBaseTest
    {
        private Guid _usuarioId;

        private RepositorioConsultaOrm _repositorioConsulta;
        private RepositorioMedicoOrm _repositorioMedico;

        private Medico _medico;

        [TestInitialize]
        public void Setup()
        {
            ApagarDados();

            _usuarioId = RegistrarUsuario();

            _repositorioConsulta = new RepositorioConsultaOrm(dbContext);
            _repositorioMedico = new RepositorioMedicoOrm(dbContext);

            BuilderSetup.SetCreatePersistenceMethod<Medico>(_repositorioMedico.InserirTest);
            BuilderSetup.SetCreatePersistenceMethod<Consulta>(_repositorioConsulta.InserirTest);

            var medico = Builder<Medico>.CreateNew().With(x => x.UsuarioId = _usuarioId).Persist();
            dbContext.SaveChanges();

            _medico = _repositorioMedico.SelecionarPorId(medico.Id);
            _medico.Nome = "Camile";
            _medico.Crm = "12589-SP";
            _medico.Especialidade = "Esteticista";

            _repositorioMedico.Editar(_medico);

            dbContext.SaveChanges();
        }

        [TestMethod]
        public async Task Deve_inserir_Consulta()
        {
            // Arrange
            var consulta = Builder<Consulta>.CreateNew().With(x => x.Medico = _medico).With(x => x.UsuarioId = _usuarioId).Persist();

            // Act
            await dbContext.SaveChangesAsync();

            // Assert
            var resposta = _repositorioConsulta.SelecionarPorId(consulta.Id);

            consulta.Should().Be(consulta);
        }

        [TestMethod]
        public async Task Deve_editar_consulta()
        {
            // Arrange
            var consulta = Builder<Consulta>.CreateNew().With(x => x.Medico = _medico).With(x => x.UsuarioId = _usuarioId).Persist();
            await dbContext.SaveChangesAsync();

            var consulta2 = _repositorioConsulta.SelecionarPorId(consulta.Id);
            DateTime data = DateTime.Now;
            consulta2.DataInicio = data;
            consulta2.HoraInicio = TimeSpan.FromMinutes(20);
            consulta2.DataTermino = data;
            consulta2.HoraTermino = TimeSpan.FromMinutes(40);

            // Act
            _repositorioConsulta.Editar(consulta2);
            await dbContext.SaveChangesAsync();

            // Assert
            consulta2.DataInicio.Should().Be(data);
            consulta2.HoraInicio.Should().Be(TimeSpan.FromMinutes(20));
            consulta2.DataTermino.Should().Be(data);
            consulta2.HoraTermino.Should().Be(TimeSpan.FromMinutes(40));
        }

        [TestMethod]
        public async Task Deve_excluir_consulta()
        {
            // Arrange
            var consulta = Builder<Consulta>.CreateNew().With(x => x.Medico = _medico).With(x => x.UsuarioId = _usuarioId).Persist();
            await dbContext.SaveChangesAsync();

            // Act
            _repositorioConsulta.Excluir(consulta);
            await dbContext.SaveChangesAsync();

            // Assert
            var consulta2 = _repositorioConsulta.SelecionarPorId(consulta.Id);
            consulta2.Should().Be(null);
        }
    }
}

            