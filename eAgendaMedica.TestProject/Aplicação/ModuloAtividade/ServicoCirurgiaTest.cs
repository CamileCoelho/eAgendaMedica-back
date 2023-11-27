using eAgendaMedica.Aplicacao.ModuloAtividade;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;
using Moq;

namespace eAgendaMedica.TestsUnitarios.TestProject.Aplicação.ModuloAtividade
{
    [TestClass]
    public class ServicoCirurgiaTest
    {
        private Mock<IRepositorioCirurgia> repositorioCirurgiaFake;
        private Mock<IContextoPersistencia> contextoPersistenciaFake;
        private ServicoCirurgia servicoCirurgia;

        [TestInitialize]
        public void Setup()
        {
            repositorioCirurgiaFake = new Mock<IRepositorioCirurgia>();
            contextoPersistenciaFake = new Mock<IContextoPersistencia>();
            servicoCirurgia = new ServicoCirurgia(repositorioCirurgiaFake.Object, contextoPersistenciaFake.Object);
        }

        #region Inserir
        [TestMethod]
        public void Deve_inserir_Cirurgia_Valida()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(40), medicos);
            
            // Act
            var resultado = servicoCirurgia.InserirAsync(cirurgia);

            // Assert
            resultado.Result.IsSuccess.Should().Be(true);
            resultado.Result.IsFailed.Should().Be(false);
        }

        [TestMethod]
        public void Nao_Deve_inserir_Cirurgia_Invalida()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(0), medicos);

            // Act
            var resultado = servicoCirurgia.InserirAsync(cirurgia);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_inserir_Cirurgia_Com_Data_Final_Anterior_A_Data_Inicial()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(40), TimeSpan.FromMinutes(0), medicos);

            // Act
            var resultado = servicoCirurgia.InserirAsync(cirurgia);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_inserir_Cirurgia_Com_Horario_Final_Conflitante_Com_Horario_inicial_De_Outra_Conulta()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgia2 = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(50), medicos);

            // Act
            var resultado = servicoCirurgia.InserirAsync(cirurgia2);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_inserir_Cirurgia_Caso_Exista_Outra_Colsulta_No_Mesmo_Horario()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgia2 = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(100), medicos);

            // Act
            var resultado = servicoCirurgia.InserirAsync(cirurgia2);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }


        [TestMethod]
        public void Nao_Deve_inserir_Cirurgia_Durante_O_Descanso_Automatico_Do_Medico()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(60), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgia2 = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(70), TimeSpan.FromMinutes(100), medicos);

            // Act
            var resultado = servicoCirurgia.InserirAsync(cirurgia2);

            // Assert
            resultado.Result.IsSuccess.Should().Be(false);
            resultado.Result.IsFailed.Should().Be(true);
        }
        #endregion

        #region Editar
        [TestMethod]
        public void Deve_Editar_Cirurgia_Valida()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(60), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgiaEdicao = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(60), medicos);
            cirurgiaEdicao.Id = cirurgia.Id;

            // Act
            var resultadoEditcao = servicoCirurgia.EditarAsync(cirurgiaEdicao);

            // Assert
            resultadoEditcao.Result.IsSuccess.Should().Be(true);
            resultadoEditcao.Result.IsFailed.Should().Be(false);
        }

        [TestMethod]
        public void Nao_Deve_Editar_Cirurgia_Invalida()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(60), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgiaEdicao = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(0), medicos);
            cirurgiaEdicao.Id = cirurgia.Id;

            // Act
            var resultadoEdicao = servicoCirurgia.EditarAsync(cirurgiaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_Editar_Cirurgia_Com_Data_Final_Anterior_A_Data_Inicial()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(60), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgiaEdicao = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(0), medicos);
            cirurgiaEdicao.Id = cirurgia.Id;

            // Act
            var resultadoEdicao = servicoCirurgia.EditarAsync(cirurgiaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_Editar_Cirurgia_Com_Horario_Final_Conflitante_Com_Horario_inicial_De_Outra_Conulta()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(80), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgia2 = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(100), TimeSpan.FromMinutes(120), medicos);
            var resultado = servicoCirurgia.InserirAsync(cirurgia2);

            var cirurgiaEdicao = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(59), medicos);
            cirurgiaEdicao.Id = cirurgia2.Id;

            // Act
            var resultadoEdicao = servicoCirurgia.EditarAsync(cirurgiaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }

        [TestMethod]
        public void Nao_Deve_Editar_Cirurgia_Caso_Exista_Outra_Colsulta_No_Mesmo_Horario()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(80), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgia2 = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(100), TimeSpan.FromMinutes(120), medicos);
            var resultado = servicoCirurgia.InserirAsync(cirurgia2);

            var cirurgiaEdicao = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(80), medicos);
            cirurgiaEdicao.Id = cirurgia2.Id;

            // Act
            var resultadoEdicao = servicoCirurgia.EditarAsync(cirurgiaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }


        [TestMethod]
        public void Nao_Deve_Editar_Cirurgia_Durante_O_Descanso_Automatico_Do_Medico()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(60), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            var cirurgia2 = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(100), TimeSpan.FromMinutes(120), medicos);
            var resultado = servicoCirurgia.InserirAsync(cirurgia2);

            var cirurgiaEdicao = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(70), TimeSpan.FromMinutes(100), medicos);
            cirurgiaEdicao.Id = cirurgia2.Id;

            // Act
            var resultadoEdicao = servicoCirurgia.EditarAsync(cirurgiaEdicao);

            // Assert
            resultadoEdicao.Result.IsSuccess.Should().Be(false);
            resultadoEdicao.Result.IsFailed.Should().Be(true);
        }
        #endregion

        #region Excluir
        [TestMethod]
        public void Deve_Excluir_Cirurgia_Com_Sucesso()
        {
            // Arrange
            var medicos = new List<Medico>();
            var medico = new Medico()
            {
                Nome = "Camile",
                Crm = "12345-SC",
                Especialidade = "Endocrinologista"
            };
            medicos.Add(medico);
            var cirurgia = new Cirurgia("Cirurgia de Teste", DateTime.Today.Date, DateTime.Today.Date, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(60), medicos);
            var resultado1 = servicoCirurgia.InserirAsync(cirurgia);

            repositorioCirurgiaFake.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(cirurgia);

            // Act
            var resultadoExcluir = servicoCirurgia.ExcluirAsync(cirurgia.Id);

            // Assert
            resultadoExcluir.Result.IsSuccess.Should().Be(true);
            resultadoExcluir.Result.IsFailed.Should().Be(false);
        }

        #endregion
    }
}
