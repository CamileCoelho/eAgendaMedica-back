using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.TestProject.ModuloMedico
{
    [TestClass]
    public class MedicoTest
    {
        [TestMethod]
        public void Deve_inicializar_corretamente_as_propriedades_no_construtor()
        {
            // Arrange
            string nome = "Rech Açougeiro";
            string crm = "12345-SC";
            string especialidade = "Fazer Gambiarra";

            // Act

            var medico = new Medico(nome, crm, especialidade);

            // Assert

            Assert.AreEqual(nome, medico.Nome);
            Assert.AreEqual(crm, medico.Crm);
            Assert.AreEqual(especialidade, medico.Especialidade);
            Assert.IsNull(medico.Cirurgias);
        }

        [TestMethod]
        public void Dois_medicos_nao_podem_ter_o_mesmo_crm()
        {
            // Arrange

            var medico1 = new Medico("Rech Açougeiro", "12345-SC", "Fazer Gambiarra");
            var medico2 = new Medico("Tiago Açougeiro", "12345-SC", "Fazer Programa");

            // Assert

            Assert.AreNotEqual(medico1, medico2);
        }

        [TestMethod]
        public void Deve_atualizar_as_informacoes()
        {
            //arrange
            var medico = new Medico("Rech Açougeiro", "12345-SC", "Fazer Gambiarra");
            var medicoAtualizado = new Medico("Alexandre Rech", "12345-SC", "Fazer programa");

            //action
            medico.AtualizarInformacoes(medicoAtualizado);

            //assert
            Assert.AreEqual(medico.Nome, medicoAtualizado.Nome);
            Assert.AreEqual(medico.Crm, medicoAtualizado.Crm);
            Assert.AreEqual(medico.Especialidade, medicoAtualizado.Especialidade);
            Assert.AreEqual(medico.Cirurgias, medicoAtualizado.Cirurgias);
        }
    }
}
