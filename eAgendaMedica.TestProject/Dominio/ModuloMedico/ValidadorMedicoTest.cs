using eAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;

namespace eAgendaMedica.TestsUnitarios.TestProject.Dominio.ModuloMedico
{
    [TestClass]
    public class ValidadorMedicoTest
    {
        [TestMethod]
        public void Nome_do_medico_deve_ser_obrigatorio()
        {
            // Arrange
            var medico = new Medico();
            medico.Nome = "a";
            medico.Crm = "12654-SC";
            medico.Especialidade = "aaa";

            var validador = new ValidadorMedico();

            // Act
            var resultado = validador.Validate(medico);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("Um nome com ao menos 3 caracteres deve ser informado.");
        }

        [TestMethod]
        public void Especialidade_do_medico_deve_ser_obrigatoria()
        {
            // Arrange
            var medico = new Medico();
            medico.Nome = "aaa";
            medico.Crm = "12654-SC";
            medico.Especialidade = "a";

            var validador = new ValidadorMedico();

            // Act
            var resultado = validador.Validate(medico);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("Uma especialidade com ao menos 3 caracteres deve ser informada.");
        }

        [TestMethod]
        public void Crm_do_medico_deve_ser_obrigatorio()
        {
            // Arrange
            var medico = new Medico();
            medico.Nome = "aaa";
            medico.Crm = "12654";
            medico.Especialidade = "aaa";

            var validador = new ValidadorMedico();

            // Act
            var resultado = validador.Validate(medico);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("'Crm' deve estar no formato '00000-XX' ");
        }
    }
}
