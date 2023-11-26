namespace eAgendaMedica.Dominio.ModuloMedico
{
    public class ValidadorMedico : AbstractValidator<Medico>
    {
        public ValidadorMedico()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Um nome com ao menos 3 caracteres deve ser informado.");

            RuleFor(x => x.Crm)
                .NotNull()
                .NotEmpty()
                .VerificaFormatoCrm();

            RuleFor(x => x.Especialidade)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Uma especialidade com ao menos 3 caracteres deve ser informada.");
        }
    }
}
