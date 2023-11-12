using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace eAgendaMedica.Dominio.Compartilhado.Validators
{
    public class FormatoTelefoneValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "FormatoTelefoneValidator";

        private string nomePropriedade;

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"'{nomePropriedade}' deve estar no formato '(XX) 9XXXX-XXXX' ";
        }

        public override bool IsValid(ValidationContext<T> contextoValidacao, string telefone)
        {
            nomePropriedade = contextoValidacao.DisplayName;

            Regex rgx = new Regex(@"^\(\d{2}\)\s9\d{4}-\d{4}$");

            if (rgx.IsMatch(telefone))
                return true;
            else
                return false;
        }
    }
}