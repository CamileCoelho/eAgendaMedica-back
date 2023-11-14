using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.TestProject.ModuloMedico
{
    [TestClass]
    public class MedicoTest
    {
        private Medico medico1;
        private Medico medico2;
        private Medico medico3;
        private Medico medico4;
        private Medico medico5;
        private List<Medico> medicos1;
        private List<Medico> medicos2;

        public MedicoTest()
        {
            medico1 = new Medico("Rech Açougeiro", "12345-SC", "Fazer Gambiarra");
            medico2 = new Medico("Alexandre Rech", "23456-SC", "Fazer programa");
            medico3 = new Medico("Tales", "34576-SC", "Fazer programa");
            medico4 = new Medico("Tiago", "78945-SC", "Fazer programa");
            medico5 = new Medico("Camile", "65489-SC", "Dev");

            medicos1 = new List<Medico> { medico1, medico2, medico3 };

            medicos2 = new List<Medico> { medico3, medico4, medico5 };

        }

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
        public void Dois_medicos_com_mesmas_informacoes_devem_ser_considerados_iguais()
        {
            // Arrange
            var medico1 = new Medico("Rech Açougeiro", "12345-SC", "Fazer Gambiarra");
            var medico2 = new Medico("Rech Açougeiro", "12345-SC", "Fazer Gambiarra");

            // Assert
            Assert.AreEqual(medico1, medico2);
        }

        [TestMethod]
        public void Adicionar_cirurgia_deve_aumentar_o_numero_de_cirurgias()
        {
            // Arrange
            var medico = new Medico("Rech Açougueiro", "12345-SC", "Fazer Gambiarra");
            
            var cirurgia = new Cirurgia(DateTime.Today,
                TimeSpan.FromHours(2), TimeSpan.FromHours(3), medicos1);

            // Act
            medico.Cirurgias.Add(cirurgia);

            // Assert
            Assert.AreEqual(1, medico.Cirurgias.Count);
            Assert.AreEqual(cirurgia, medico.Cirurgias[0]);
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
            Assert.AreEqual(medico, medicoAtualizado);
        }
    }
}
