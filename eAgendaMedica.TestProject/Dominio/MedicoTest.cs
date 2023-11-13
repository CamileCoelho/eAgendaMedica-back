using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.TestProject.ModuloMedico
{
    [TestClass]
    public class MedicoTest
    {
        [TestMethod]
        public void Deve_atualizar_as_informacoes()
        {
            //arrange
            var medico = new Medico("Rech Açougeiro", "12345-SC", "Fazer Gambiarra");
            var medicoAtualizado = new Medico("Alexandre Rech", "12345-SC", "Fazer programa");

            //action
            medico.AtualizarInformacoes(medicoAtualizado);

            //assert
            Assert.AreNotEqual(medico, medicoAtualizado);
        }
    }
}
