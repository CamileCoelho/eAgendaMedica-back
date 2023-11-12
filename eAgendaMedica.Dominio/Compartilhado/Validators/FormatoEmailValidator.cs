using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace eAgendaMedica.Dominio.Compartilhado.Validators
{
    public class FormatoEmailValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "FormatoEmailValidator";

        private string nomePropriedade;

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"'{nomePropriedade}' deve estar no formato 'xxxxxxxx@xxxxx.xxx' ";
        }

        public override bool IsValid(ValidationContext<T> contextoValidacao, string texto)
        {
            nomePropriedade = contextoValidacao.DisplayName;

            Regex rgx = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

            if (rgx.IsMatch(texto))
                return true;
            else
                return false;
        }
    }
}