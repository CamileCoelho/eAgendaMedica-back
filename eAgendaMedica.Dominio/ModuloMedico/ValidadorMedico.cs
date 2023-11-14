namespace eAgendaMedica.Dominio.ModuloMedico
{
    public class ValidadorMedico : AbstractValidator<Medico>
    {
        public ValidadorMedico()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.Crm)
                .NotNull()
                .NotEmpty()
                .VerificaFormatoCrm();

            RuleFor(x => x.Especialidade)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3); 
        }
    }
}
