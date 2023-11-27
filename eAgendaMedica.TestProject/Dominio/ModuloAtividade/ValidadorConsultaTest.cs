using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;

namespace eAgendaMedica.TestsUnitarios.TestProject.Dominio.ModuloAtividade
{
    [TestClass]
    public class ValidadorConsultaTest
    {
        Medico medico = new();

        [TestInitialize]
        public void Setup()
        {
            medico.Nome = "aaaa";
            medico.Crm = "12654-SC";
            medico.Especialidade = "aaaa";
        }

        [TestMethod]
        public void Detalhes_não_deve_ser_obrigatorio()
        {
            // Arrange
            var consulta = new Consulta();
            consulta.Detalhes = null;
            consulta.HoraInicio = TimeSpan.FromMinutes(20);
            consulta.HoraTermino = TimeSpan.FromMinutes(40);
            consulta.DataInicio = DateTime.Today.Date + consulta.HoraInicio;
            consulta.DataTermino = DateTime.Today.Date + consulta.HoraTermino;
            consulta.Medico = medico;

            var validador = new ValidadorConsulta();

            // Act
            var resultado = validador.Validate(consulta);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().NotContain("Erro");
        }

        [TestMethod]
        public void DataInicio_deve_ser_obrigatoria()
        {
            // Arrange
            var consulta = new Consulta();
            consulta.Detalhes = null;
            consulta.HoraInicio = TimeSpan.FromMinutes(20);
            consulta.HoraTermino = TimeSpan.FromMinutes(40);
            consulta.DataInicio = DateTime.MinValue;
            consulta.DataTermino = DateTime.Today.Date + consulta.HoraTermino;
            consulta.Medico = medico;

            var validador = new ValidadorConsulta();

            // Act
            var resultado = validador.Validate(consulta);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("A data de inicio deve ser informada.");
        }

        [TestMethod]
        public void DataTermino_deve_ser_obrigatoria()
        {
            // Arrange
            var consulta = new Consulta();
            consulta.Detalhes = null;
            consulta.HoraInicio = TimeSpan.FromMinutes(20);
            consulta.HoraTermino = TimeSpan.FromMinutes(40);
            consulta.DataInicio = DateTime.Today.Date + consulta.HoraInicio;
            consulta.DataTermino = DateTime.MinValue;
            consulta.Medico = medico;

            var validador = new ValidadorConsulta();

            // Act
            var resultado = validador.Validate(consulta);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("A data de término deve ser informada.");
        }

        [TestMethod]
        public void DataTermino_deve_ser_posterior_ou_igual_a_DataInicio()
        {
            // Arrange
            var consulta = new Consulta();
            consulta.Detalhes = null;
            consulta.HoraInicio = TimeSpan.FromMinutes(20);
            consulta.HoraTermino = TimeSpan.FromMinutes(40);
            consulta.DataInicio = DateTime.Today.Date + consulta.HoraInicio;
            consulta.DataTermino = consulta.DataInicio.Subtract(TimeSpan.FromDays(1));
            consulta.Medico = medico;

            var validador = new ValidadorConsulta();

            // Act
            var resultado = validador.Validate(consulta);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("Data de término deve ser igual ou posterior a data de início.");
        }


        [TestMethod]
        public void HoraTermino_deve_ser_posterior_ou_igual_a_HoraInicio_se_as_datas_forem_iguais()
        {
            // Arrange
            var consulta = new Consulta();
            consulta.Detalhes = null;
            consulta.HoraInicio = TimeSpan.FromMinutes(20);
            consulta.HoraTermino = TimeSpan.FromMinutes(10);
            consulta.DataInicio = DateTime.Today.Date + consulta.HoraInicio;
            consulta.DataTermino = DateTime.Today.Date + consulta.HoraInicio;
            consulta.Medico = medico;

            var validador = new ValidadorConsulta();

            // Act
            var resultado = validador.Validate(consulta);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("Horário de término deve ser posterior ao horário de início.");
        }



        [TestMethod]
        public void Periodo_de_recuperacao_deve_ser_de_20_minutos_na_consulta()
        {
            // Arrange
            var consulta = new Consulta();
            consulta.Detalhes = null;
            consulta.HoraInicio = TimeSpan.FromMinutes(20);
            consulta.HoraTermino = TimeSpan.FromMinutes(30);
            consulta.DataInicio = DateTime.Today.Date + consulta.HoraInicio;
            consulta.DataTermino = DateTime.Today.Date + consulta.HoraTermino;
            consulta.Medico = medico;
            consulta.PeriodoRecuperacao = TimeSpan.FromMinutes(30); 

            var validador = new ValidadorConsulta();

            // Act
            var resultado = validador.Validate(consulta);

            // Assert
            resultado.Errors[0].ErrorMessage.Should().Be("O periodo de recuperação deve ser de 20 minutos.");
        }
    }
}
