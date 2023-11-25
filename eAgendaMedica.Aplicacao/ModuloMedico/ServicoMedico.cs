using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Aplicacao.ModuloMedico
{
    public class ServicoMedico : ServicoBase<Medico, ValidadorMedico>
    {
        private readonly IRepositorioMedico repositorioMedico;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoMedico(IRepositorioMedico repositorioMedico, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Medico>> InserirAsync(Medico medico)
        {
            var resultadoValidacao = Validar(medico);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioMedico.InserirAsync(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> EditarAsync(Medico medico)
        {
            var resultadoValidacao = Validar(medico);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioMedico.Editar(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> ExcluirAsync(Guid id)
        {
            var medicoResult = await SelecionarPorIdAsync(id);

            if (medicoResult == null)
                return Result.Fail("Médico não encontrado.");

            var medico = medicoResult.Value;

            var erros = new List<Error>();

            if (repositorioMedico.MedicoNaoPodeSerExcluido(medico))
            {
                Log.Logger.Warning($"O médico '{medico.Nome}' possuí ao menos uma atividade e não pode ser excluído.");

                erros.Add(new Error($"O médico '{medico.Nome}' possuí ao menos uma atividade e não pode ser excluído."));
            }

            if (erros.Any())
                return Result.Fail(erros);

            repositorioMedico.Excluir(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Medico>>> SelecionarTodosAsync()
        {
            var medicos = await repositorioMedico.SelecionarTodosAsync();

            return Result.Ok(medicos);
        }

        public async Task<Result<Medico>> SelecionarPorIdAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            if (medico == null)
                return Result.Fail("Médico não encontrado.");

            return Result.Ok(medico);
        }

        protected override Result Validar(Medico obj)
        {
            var validador = new ValidadorMedico();

            var resultadoValidacao = validador.Validate(obj);

            var erros = new List<Error>();

            if (CrmDuplicado(obj))
            {
                Log.Logger.Warning($"Este crm '{obj.Crm}' já está sendo utilizado");

                erros.Add(new Error($"Este crm '{obj.Crm}' já está sendo utilizado"));
            }

            foreach (var validationFailure in resultadoValidacao.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                erros.Add(new Error(validationFailure.ErrorMessage));
            }

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }

        private bool CrmDuplicado(Medico medico)
        {
            Medico medicoEncontrado = repositorioMedico.SelecionarPorCrm(medico.Crm);

            if (medicoEncontrado != null &&
               medicoEncontrado.Id != medico.Id &&
               medicoEncontrado.Crm == medico.Crm)
                return true;

            return false;
        }
    }
}