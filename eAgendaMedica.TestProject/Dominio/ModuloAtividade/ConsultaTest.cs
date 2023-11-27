using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.TestProject.ModuloMedico
{
    [TestClass]
    public class ConsultaTest
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void Deve_atualizar_as_informacoes_Consulta()
        {
            // Arrange
            var consulta = new Consulta("", DateTime.Today, DateTime.Today, TimeSpan.FromHours(2), TimeSpan.FromHours(3), new Medico());
            var consultaAtualizada = new Consulta("", DateTime.Today, DateTime.Today, TimeSpan.FromHours(6), TimeSpan.FromHours(8), new Medico());

            // Act
            consulta.AtualizarInformacoes(consultaAtualizada);

            // Assert
            Assert.AreEqual(consultaAtualizada.DataInicio, consulta.DataInicio);
            Assert.AreEqual(consultaAtualizada.HoraInicio, consulta.HoraInicio);
            Assert.AreEqual(consultaAtualizada.HoraTermino, consulta.HoraTermino);
            Assert.AreEqual(consultaAtualizada.Medico, consulta.Medico);
        }
    }
}
