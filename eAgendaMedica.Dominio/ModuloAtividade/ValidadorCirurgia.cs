namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorCirurgia : AbstractValidator<Cirurgia>
    {
        public ValidadorCirurgia()
        {
            RuleFor(x => x.Data)
               .NotNull()
               .NotEmpty();

            RuleFor(x => x.HoraInicio)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.HoraTermino)
                .NotNull()
                .NotEmpty()
                .GreaterThan((x) => x.HoraInicio);

            RuleFor(x => x.PeriodoRecuperacao)
                .Equal(TimeSpan.FromMinutes(240));

            RuleFor(x => x.Medicos)
                .NotNull()
                .NotEmpty();
        }
    }
}
