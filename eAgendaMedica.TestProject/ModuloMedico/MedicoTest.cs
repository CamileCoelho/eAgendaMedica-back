using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgendaMedica.Dominio.ModuloMedico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eAgendaMedica.Dominio.TestProject.ModuloMedico
{
    [TestClass]
    public class MedicoTest
    {
        [TestMethod]
        public void TestMethod1()
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
