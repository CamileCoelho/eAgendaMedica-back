using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using FizzWare.NBuilder;
using FluentAssertions;

namespace eAgendaMedica.Integration.TestProject.ModuloMedico
{
    [TestClass]
    public class RepositorioMedicoOrmTest : RepositorioBaseTest
    {
        private Guid _usuarioId;

        private RepositorioMedicoOrm _repositorioMedico;

        [TestInitialize]
        public void Setup()
        {
            ApagarDados();

            _usuarioId = RegistrarUsuario();

            _repositorioMedico = new RepositorioMedicoOrm(dbContext);

            BuilderSetup.SetCreatePersistenceMethod<Medico>(_repositorioMedico.InserirTest);
        }

        [TestMethod]
        public async Task Deve_inserir_Medico()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().With(x => x.UsuarioId = _usuarioId).Persist();
                    
            // Act
            await dbContext.SaveChangesAsync();

            // Assert
            var resposta = _repositorioMedico.SelecionarPorId(medico.Id);

            medico.Should().Be(medico);
        }

        [TestMethod]
        public async Task Deve_editar_medico()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().With(x => x.UsuarioId = _usuarioId).Persist();
            await dbContext.SaveChangesAsync();

            var medico2 = _repositorioMedico.SelecionarPorId(medico.Id);
            medico2.Nome = "Camile";
            medico2.Crm = "12589-SP";

            // Act
            _repositorioMedico.Editar(medico2);
            await dbContext.SaveChangesAsync();

            // Assert
            medico2.Nome.Should().Be("Camile");
            medico2.Crm.Should().Be("12589-SP");
        }

        [TestMethod]
        public async Task Deve_excluir_medico()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().With(x => x.UsuarioId = _usuarioId).Persist();
            await dbContext.SaveChangesAsync();

            // Act
            _repositorioMedico.Excluir(medico);
            await dbContext.SaveChangesAsync();

            // Assert
            var medico2 = _repositorioMedico.SelecionarPorId(medico.Id);
            medico2.Should().Be(null);
        }
    }
}
