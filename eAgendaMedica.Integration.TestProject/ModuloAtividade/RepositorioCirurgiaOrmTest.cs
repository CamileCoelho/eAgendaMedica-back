using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.ModuloAtividade;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using FizzWare.NBuilder;
using FluentAssertions;

namespace eAgendaMedica.Integration.TestProject.ModuloAtividade
{
    [TestClass]
    public class RepositorioCirurgiaOrmTest : RepositorioBaseTest
    {
        private Guid _usuarioId;

        private RepositorioCirurgiaOrm _repositorioCirurgia;
        private RepositorioMedicoOrm _repositorioMedico;

        private Medico _medico;
        private Medico _medico2;

        [TestInitialize]
        public void Setup()
        {
            ApagarDados();

            _usuarioId = RegistrarUsuario();

            _repositorioCirurgia = new RepositorioCirurgiaOrm(dbContext);
            _repositorioMedico = new RepositorioMedicoOrm(dbContext);

            BuilderSetup.SetCreatePersistenceMethod<Medico>(_repositorioMedico.InserirTest);
            BuilderSetup.SetCreatePersistenceMethod<Cirurgia>(_repositorioCirurgia.InserirTest);

            var medico = Builder<Medico>.CreateNew().With(x => x.UsuarioId = _usuarioId).Persist();
            dbContext.SaveChanges();

            _medico = _repositorioMedico.SelecionarPorId(medico.Id);
            _medico2 = _repositorioMedico.SelecionarPorId(medico.Id);

            _medico.Nome = "Camile";
            _medico.Crm = "12589-SP";
            _medico.Especialidade = "Esteticista";
            _medico2.Nome = "Tales";
            _medico2.Crm = "12546-SP";
            _medico2.Especialidade = "Endorinologista";

            _repositorioMedico.Editar(_medico);
            _repositorioMedico.Editar(_medico2);

            dbContext.SaveChanges();
        }

        [TestMethod]
        public async Task Deve_inserir_Cirurgia()
        {
            // Arrange
            var medicos = new List<Medico>();
            medicos.Add(_medico);
            medicos.Add(_medico2);

            var cirurgia = Builder<Cirurgia>.CreateNew().With(x => x.Medicos = medicos).With(x => x.UsuarioId = _usuarioId).Persist();

            // Act
            await dbContext.SaveChangesAsync();

            // Assert
            var resposta = _repositorioCirurgia.SelecionarPorId(cirurgia.Id);

            cirurgia.Should().Be(cirurgia);
        }

        [TestMethod]
        public async Task Deve_editar_cirurgia()
        {
            // Arrange
            var medicos = new List<Medico>();
            medicos.Add(_medico);
            medicos.Add(_medico2);

            var cirurgia = Builder<Cirurgia>.CreateNew().With(x => x.Medicos = medicos).With(x => x.UsuarioId = _usuarioId).Persist();
            await dbContext.SaveChangesAsync();

            var cirurgia2 = _repositorioCirurgia.SelecionarPorId(cirurgia.Id);
            DateTime data = DateTime.Now;
            cirurgia2.DataInicio = data;
            cirurgia2.HoraInicio = TimeSpan.FromMinutes(20);
            cirurgia2.DataTermino = data;
            cirurgia2.HoraTermino = TimeSpan.FromMinutes(40);

            // Act
            _repositorioCirurgia.Editar(cirurgia2);
            await dbContext.SaveChangesAsync();

            // Assert
            cirurgia2.DataInicio.Should().Be(data);
            cirurgia2.HoraInicio.Should().Be(TimeSpan.FromMinutes(20));
            cirurgia2.DataTermino.Should().Be(data);
            cirurgia2.HoraTermino.Should().Be(TimeSpan.FromMinutes(40));
        }

        [TestMethod]
        public async Task Deve_excluir_cirurgia()
        {
            // Arrange
            var medicos = new List<Medico>();
            medicos.Add(_medico);
            medicos.Add(_medico2);

            var cirurgia = Builder<Cirurgia>.CreateNew().With(x => x.Medicos = medicos).With(x => x.UsuarioId = _usuarioId).Persist();
            await dbContext.SaveChangesAsync();

            // Act
            _repositorioCirurgia.Excluir(cirurgia);
            await dbContext.SaveChangesAsync();

            // Assert
            var cirurgia2 = _repositorioCirurgia.SelecionarPorId(cirurgia.Id);
            cirurgia2.Should().Be(null);
        }
    }
}
