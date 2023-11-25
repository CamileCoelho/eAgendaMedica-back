using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Aplicacao
{
    public abstract class ServicoBase<TDominio, TValidador> where TValidador : AbstractValidator<TDominio>, new()
    {
        protected virtual Result Validar(TDominio obj)
        {
            var validador = new TValidador();

            var resultadoValidacao = validador.Validate(obj);

            var erros = new List<Error>();

            foreach (var validationFailure in resultadoValidacao.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                erros.Add(new Error(validationFailure.ErrorMessage));
            }

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }

        public bool VerificarConflitoHorario(Medico medico, DateTime novoInicio, DateTime novoTermino, Guid id)
        {
            foreach (var consulta in medico.Consultas)
            {
                DateTime termino = consulta.DataTermino.AddMinutes(20);

                if (consulta.Id != id && consulta.Medico == medico &&
                   ((novoInicio >= consulta.DataInicio && novoInicio <= termino) ||
                   (novoTermino >= consulta.DataInicio && novoTermino <= termino)))
                {
                    return true;
                }
            }

            foreach (var cirurgia in medico.Cirurgias)
            {
                DateTime termino = cirurgia.DataTermino.AddHours(4);

                if (cirurgia.Id != id && cirurgia.Medicos.Contains(medico) &&
                   ((novoInicio >= cirurgia.DataInicio && novoInicio <= termino) ||
                   (novoTermino >= cirurgia.DataInicio && novoTermino <= termino)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
