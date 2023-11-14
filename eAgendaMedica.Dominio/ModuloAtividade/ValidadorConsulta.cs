namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorConsulta : AbstractValidator<Consulta>
    {
        public ValidadorConsulta()
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
                .GreaterThan((x) => x.HoraInicio)
                .WithMessage("Horário de término deve ser posterior ao horário de início."); 

            RuleFor(x => x.PeriodoRecuperacao)
                .Equal(TimeSpan.FromMinutes(20));

            RuleFor(x => x.Medico)
                .NotNull()
                .NotEmpty();
        }
    }
}
