namespace eAgendaMedica.Aplicacao.ModuloAutenticacao
{
    public class ValidadorUsuario : AbstractValidator<Usuario>
    {
        public ValidadorUsuario()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .NotNull();
        }
    }
}
