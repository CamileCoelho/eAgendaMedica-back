using eAgendaMedica.Aplicacao.ModuloAtividade;
using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;
using Moq;

namespace eAgendaMedica.TestsUnitarios.TestProject.Aplicação.ModuloAtividade
{
    [TestClass]
    public class ServicoConsultaTest
    {
        private Mock<IRepositorioConsulta> repositorioConsultaFake;
        private Mock<IContextoPersistencia> contextoPersistenciaFake;
        private ServicoConsulta servicoConsulta;

        [TestInitialize]
        public void Setup()
        {
            repositorioConsultaFake = new Mock<IRepositorioConsulta>();
            contextoPersistenciaFake = new Mock<IContextoPersistencia>();
            servicoConsulta = new ServicoConsulta(repositorioConsultaFake.Object, contextoPersistenciaFake.Object);
        }

        #region Inserir
        [TestMethod]
        public void Deve_inserir_Consulta_Valida()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(40), medico);
            consulta.MedicoId = medico.Id;

            // Act
            var resultado = servicoConsulta.InserirAsync(consulta);

            // Assert
            resultado.Result.IsSuccess.Should().Be(true);
            resultado.Result.IsFailed.Should().Be(false);
        }

        [TestMethod]
        public void Nao_Deve_inserir_Consulta_Invalida()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(0), medico);
            consulta.MedicoId = medico.Id;

            // Act
            var resultado = servicoConsulta.InserirAsync(consulta);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_inserir_Consulta_Com_Data_Final_Anterior_A_Data_Inicial()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(0), medico);
            consulta.MedicoId = medico.Id;

            // Act
            var resultado = servicoConsulta.InserirAsync(consulta);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_inserir_Consulta_Com_Horario_Final_Conflitante_Com_Horario_inicial_De_Outra_Conulta()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medico);
            consulta.MedicoId = medico.Id;
            var resultado1 = servicoConsulta.InserirAsync(consulta);

            var consulta2 = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(59), medico);
            consulta.MedicoId = medico.Id;

            // Act
            var resultado = servicoConsulta.InserirAsync(consulta2);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_inserir_Consulta_Caso_Exista_Outra_Colsulta_No_Mesmo_Horario()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medico);
            consulta.MedicoId = medico.Id;
            var resultado1 = servicoConsulta.InserirAsync(consulta);

            var consulta2 = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medico);
            consulta.MedicoId = medico.Id;

            // Act
            var resultado = servicoConsulta.InserirAsync(consulta2);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }


        [TestMethod]
        public void Nao_Deve_inserir_Consulta_Durante_O_Descanso_Automatico_Do_Medico()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(50), medico);
            consulta.MedicoId = medico.Id;
            var resultado1 = servicoConsulta.InserirAsync(consulta);

            var consulta2 = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medico);
            consulta.MedicoId = medico.Id;

            // Act
            var resultado = servicoConsulta.InserirAsync(consulta2);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }
        #endregion

        #region Editar
        [TestMethod]
        public void Deve_Editar_Consulta_Valida()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(40), medico);
            consulta.MedicoId = medico.Id;
            var resultado = servicoConsulta.InserirAsync(consulta);

            var consultaEdicao = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(60), medico);
            consultaEdicao.Id = consulta.Id;
            consultaEdicao.MedicoId = medico.Id;

            // Act
            var resultadoEditcao = servicoConsulta.EditarAsync(consultaEdicao);

            // Assert
            resultadoEditcao.Result.IsSuccess.Should().Be(true);
            resultadoEditcao.Result.IsFailed.Should().Be(false);
        }

        [TestMethod]
        public void Nao_Deve_Editar_Consulta_Invalida()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(40), medico);
            consulta.MedicoId = medico.Id;
            var resultado = servicoConsulta.InserirAsync(consulta);

            var consultaEdicao = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(0), medico);
            consultaEdicao.Id = consulta.Id;
            consultaEdicao.MedicoId = medico.Id;

            // Act
            var resultadoEdicao = servicoConsulta.EditarAsync(consultaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_Editar_Consulta_Com_Data_Final_Anterior_A_Data_Inicial()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(40), medico);
            consulta.MedicoId = medico.Id;
            var resultado = servicoConsulta.InserirAsync(consulta);

            var consultaEdicao = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(0), medico);
            consultaEdicao.Id = consulta.Id;
            consultaEdicao.MedicoId = medico.Id;

            // Act
            var resultadoEdicao = servicoConsulta.EditarAsync(consultaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_Editar_Consulta_Com_Horario_Final_Conflitante_Com_Horario_inicial_De_Outra_Conulta()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medico);
            consulta.MedicoId = medico.Id;
            var resultado1 = servicoConsulta.InserirAsync(consulta);

            var consulta2 = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(30), medico);
            consulta.MedicoId = medico.Id;
            var resultado = servicoConsulta.InserirAsync(consulta2);

            var consultaEdicao = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(59), medico);
            consultaEdicao.Id = consulta2.Id;
            consultaEdicao.MedicoId = medico.Id;

            // Act
            var resultadoEdicao = servicoConsulta.EditarAsync(consultaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_Editar_Consulta_Caso_Exista_Outra_Colsulta_No_Mesmo_Horario()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medico);
            consulta.MedicoId = medico.Id;
            var resultado1 = servicoConsulta.InserirAsync(consulta);

            var consulta2 = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(30), medico);
            consulta.MedicoId = medico.Id;
            var resultado = servicoConsulta.InserirAsync(consulta2);

            var consultaEdicao = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medico);
            consultaEdicao.Id = consulta2.Id;
            consultaEdicao.MedicoId = medico.Id;

            // Act
            var resultadoEdicao = servicoConsulta.EditarAsync(consultaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }


        [TestMethod]
        public void Nao_Deve_Editar_Consulta_Durante_O_Descanso_Automatico_Do_Medico()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(50), medico);
            consulta.MedicoId = medico.Id;
            var resultado1 = servicoConsulta.InserirAsync(consulta);

            var consulta2 = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(100), TimeSpan.FromMinutes(120), medico);
            consulta.MedicoId = medico.Id;

            var consultaEdicao = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medico);
            consultaEdicao.Id = consulta2.Id;
            consultaEdicao.MedicoId = medico.Id;

            // Act
            var resultadoEdicao = servicoConsulta.EditarAsync(consultaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }
        #endregion

        #region Excluir
        [TestMethod]
        public void Deve_Excluir_Consulta_Com_Sucesso()
        {
            // Arrange
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            var consulta = new Consulta("Consulta de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(40), medico);
            consulta.MedicoId = medico.Id;
            var resultado = servicoConsulta.InserirAsync(consulta);

            repositorioConsultaFake.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(consulta);

            // Act
            var resultadoExcluir = servicoConsulta.ExcluirAsync(consulta.Id);

            // Assert
            resultadoExcluir.Result.IsSuccess.Should().Be(true);
            resultadoExcluir.Result.IsFailed.Should().Be(false);
        }

        #endregion
    }
}
