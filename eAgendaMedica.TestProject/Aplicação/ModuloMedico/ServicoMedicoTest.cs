using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;
using Moq;

namespace eAgendaMedica.TestsUnitarios.TestProject.Aplicação.ModuloMedico
{
    [TestClass]
    public class ServicoMedicoTest
    {
        private Mock<IRepositorioMedico> repositorioMedicoFake;
        private Mock<IContextoPersistencia> contextoPersistenciaFake;
        private ServicoMedico servicoMedico;

        [TestInitialize]
        public void Setup()
        {
            repositorioMedicoFake = new Mock<IRepositorioMedico>();
            contextoPersistenciaFake = new Mock<IContextoPersistencia>();
            servicoMedico = new ServicoMedico(repositorioMedicoFake.Object, contextoPersistenciaFake.Object);
        }

        [TestMethod]
        public void Deve_inserir_Medico_Valido()
        {
            // Arrange
            var medico = new Medico("Camile", "12345-SC", "Endocrinologista");

            // Act
            var resultado = servicoMedico.InserirAsync(medico);

            // Assert
            resultado.Result.IsSuccess.Should().Be(true);
            resultado.Result.IsFailed.Should().Be(false);

        }

        [TestMethod]
        public void Nao_Deve_inserir_Medico_Invalido()
        {
            // Arrange
            var medico = new Medico("Camile", "1235-SC", "Endocrinologista");

            // Act
            var resultado = servicoMedico.InserirAsync(medico);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);

        }
    }
}
