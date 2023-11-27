using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;

namespace eAgendaMedica.TestsUnitarios.TestProject.Dominio.ModuloAtividade
{
    [TestClass]
    public class ValidadorCirurgiaTest
    {
        List<Medico> medicos = new();

        [TestInitialize]
        public void Setup()
        {
            Medico medico = new();
            medico.Nome = "aaaa";
            medico.Crm = "12654-SC";
            medico.Especialidade = "aaaa";

            Medico medico2 = new();
            medico2.Nome = "bbbb";
            medico2.Crm = "62589-SC";
            medico2.Especialidade = "bbbb";

            medicos.Add(medico);
            medicos.Add(medico2);
        }

        [TestMethod]
        public void DataInicio_deve_ser_obrigatoria()
        {
            // Arrange
            var cirurgia = new Cirurgia();
            cirurgia.Detalhes = null;
            cirurgia.HoraInicio = TimeSpan.FromMinutes(20);
            cirurgia.HoraTermino = TimeSpan.FromMinutes(40);
            cirurgia.DataInicio = DateTime.MinValue;
            cirurgia.DataTermino = DateTime.Today.Date + cirurgia.HoraTermino;
            cirurgia.Medicos = medicos;

            var validador = new ValidadorCirurgia();

            // Act
            var resultado = validador.Validate(cirurgia);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("A data de inicio deve ser informada.");
        }

        [TestMethod]
        public void DataTermino_deve_ser_obrigatoria()
        {
            // Arrange
            var cirurgia = new Cirurgia();
            cirurgia.Detalhes = null;
            cirurgia.HoraInicio = TimeSpan.FromMinutes(20);
            cirurgia.HoraTermino = TimeSpan.FromMinutes(40);
            cirurgia.DataInicio = DateTime.Today.Date + cirurgia.HoraInicio;
            cirurgia.DataTermino = DateTime.MinValue;
            cirurgia.Medicos = medicos;

            var validador = new ValidadorCirurgia();

            // Act
            var resultado = validador.Validate(cirurgia);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("A data de término deve ser informada.");
        }

        [TestMethod]
        public void DataTermino_deve_ser_posterior_ou_igual_a_DataInicio()
        {
            // Arrange
            var cirurgia = new Cirurgia();
            cirurgia.Detalhes = null;
            cirurgia.HoraInicio = TimeSpan.FromMinutes(20);
            cirurgia.HoraTermino = TimeSpan.FromMinutes(40);
            cirurgia.DataInicio = DateTime.Today.Date + cirurgia.HoraInicio;
            cirurgia.DataTermino = cirurgia.DataInicio.Subtract(TimeSpan.FromDays(1));
            cirurgia.Medicos = medicos;

            var validador = new ValidadorCirurgia();

            // Act
            var resultado = validador.Validate(cirurgia);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("Data de término deve ser igual ou posterior a data de início.");
        }


        [TestMethod]
        public void HoraTermino_deve_ser_posterior_ou_igual_a_HoraInicio_se_as_datas_forem_iguais()
        {
            // Arrange
            var cirurgia = new Cirurgia();
            cirurgia.Detalhes = null;
            cirurgia.HoraInicio = TimeSpan.FromMinutes(20);
            cirurgia.HoraTermino = TimeSpan.FromMinutes(10);
            cirurgia.DataInicio = DateTime.Today.Date + cirurgia.HoraInicio;
            cirurgia.DataTermino = DateTime.Today.Date + cirurgia.HoraInicio;
            cirurgia.Medicos = medicos;

            var validador = new ValidadorCirurgia();

            // Act
            var resultado = validador.Validate(cirurgia);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("Horário de término deve ser posterior ao horário de início.");
        }



        [TestMethod]
        public void Periodo_de_recuperacao_deve_ser_de_4_horas_na_cirurgia()
        {
            // Arrange
            var cirurgia = new Cirurgia();
            cirurgia.Detalhes = null;
            cirurgia.HoraInicio = TimeSpan.FromMinutes(20);
            cirurgia.HoraTermino = TimeSpan.FromMinutes(30);
            cirurgia.DataInicio = DateTime.Today.Date + cirurgia.HoraInicio;
            cirurgia.DataTermino = DateTime.Today.Date + cirurgia.HoraTermino;
            cirurgia.Medicos = medicos;
            cirurgia.PeriodoRecuperacao = TimeSpan.FromMinutes(30);

            var validador = new ValidadorCirurgia();

            // Act
            var resultado = validador.Validate(cirurgia);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("O periodo de recuperação deve ser de 4 horas.");
        }
    }
}
