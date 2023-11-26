namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorCirurgia : AbstractValidator<Cirurgia>
    {
        public ValidadorCirurgia()
        {
            RuleFor(x => x.DataInicio)
               .NotNull()
               .NotEmpty();

            RuleFor(x => x.DataTermino)
               .NotNull()
               .NotEmpty();

            RuleFor(x => x.DataInicio)
                .NotNull()
                .NotEmpty()
                .LessThanOrEqualTo((x) => x.DataTermino)
                .WithMessage("Data de término deve ser igual ou posterior a data de início.");

            When(x => x.DataInicio.Date == x.DataTermino.Date, () =>
            {
                RuleFor(x => x.HoraTermino)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(x => x.HoraInicio)
                    .WithMessage("Horário de término deve ser posterior ao horário de início.");
            });

            RuleFor(x => x.PeriodoRecuperacao)
                .Equal(TimeSpan.FromMinutes(240));

            RuleFor(x => x.Medicos)
                .NotNull()
                .NotEmpty();
        }
    }
}
