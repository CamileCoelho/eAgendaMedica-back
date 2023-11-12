using eAgendaMedica.Dominio.Compartilhado.Validators;

namespace eAgendaMedica.Dominio.Compartilhado
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> SemCaracteresEspeciaisValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new SemCaracteresEspeciaisValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> PodeApenasLetras<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new PodeApenasLetrasValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoEmailValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoCpfValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoCrm<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoCrmValidator<T>());
        }

        public static IRuleBuilderOptions<T, string> VerificaFormatoTelefone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FormatoTelefoneValidator<T>());
        }
    }
}
