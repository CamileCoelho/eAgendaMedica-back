using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.TestsUnitarios.TestProject.Dominio.ModuloAtividade
{
    [TestClass]
    public class CirurgiaTest
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void Deve_atualizar_as_informacoes_Cirurgia()
        {
            // Arrange
            var cirurgia = new Cirurgia("", DateTime.Today, DateTime.Today,
                TimeSpan.FromHours(2), TimeSpan.FromHours(3), new List<Medico>());
            var cirurgiaAtualizada = new Cirurgia("", DateTime.Today.AddDays(2), DateTime.Today.AddDays(2),
                TimeSpan.FromHours(6), TimeSpan.FromHours(8), new List<Medico>());

            // Act
            cirurgia.AtualizarInformacoes(cirurgiaAtualizada);

            // Assert
            Assert.AreEqual(cirurgiaAtualizada.DataInicio, cirurgia.DataInicio);
            Assert.AreEqual(cirurgiaAtualizada.HoraInicio, cirurgia.HoraInicio);
            Assert.AreEqual(cirurgiaAtualizada.HoraTermino, cirurgia.HoraTermino);
        }
    }
}
