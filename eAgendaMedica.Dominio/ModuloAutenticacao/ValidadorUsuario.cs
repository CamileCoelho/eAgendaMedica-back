namespace eAgendaMedica.Aplicacao.ModuloAutenticacao
{
    public class ValidadorUsuario : AbstractValidator<Usuario>
    {
        public ValidadorUsuario()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo vome é obrigatório")
                .NotNull().WithMessage("O campo vome é obrigatório");
        }
    }
}
