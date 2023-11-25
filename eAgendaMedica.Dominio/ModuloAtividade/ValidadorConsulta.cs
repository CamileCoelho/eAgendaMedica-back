namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorConsulta : AbstractValidator<Consulta>
    {
        public ValidadorConsulta()
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
                .Equal(TimeSpan.FromMinutes(20));

            RuleFor(x => x.MedicoId)
                .NotNull()
                .NotEmpty();
        }
    }
}
