using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.TestProject.ModuloMedico
{
    [TestClass]
    public class AtividadeTest
    {
        private Medico medico1;
        private Medico medico2;
        private Medico medico3;
        private Medico medico4;
        private Medico medico5;
        private List<Medico> medicos1;
        private List<Medico> medicos2;

        public AtividadeTest()
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
        public void Deve_atualizar_as_informacoes_Consulta()
        {
            //arrange
            var consulta = new Consulta(DateTime.Today, TimeSpan.FromHours(2), TimeSpan.FromHours(3), medico1);
            var consultaAtualizada = new Consulta(DateTime.Today, TimeSpan.FromHours(6), TimeSpan.FromHours(8), medico2);

            //action
            consulta.AtualizarInformacoes(consultaAtualizada);

            //assert
            Assert.AreNotEqual(consulta, consultaAtualizada);
        }

        [TestMethod]
        public void Deve_atualizar_as_informacoes_Cirurgia()
        {
            //arrange
            var cirurgia = new Cirurgia(DateTime.Today, 
                TimeSpan.FromHours(2), TimeSpan.FromHours(3), medicos1);
            var cirurgiaAtualizada = new Cirurgia(DateTime.Today.AddDays(2), 
                TimeSpan.FromHours(6), TimeSpan.FromHours(8), medicos2);

            //action
            cirurgia.AtualizarInformacoes(cirurgiaAtualizada);

            //assert
            Assert.AreNotEqual(cirurgia, cirurgiaAtualizada);
        }
    }
}
