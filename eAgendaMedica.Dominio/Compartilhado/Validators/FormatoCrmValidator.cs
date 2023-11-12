using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace eAgendaMedica.Dominio.Compartilhado.Validators
{
    public class FormatoCrmValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "FormatoCrmValidator";

        private string nomePropriedade;

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"'{nomePropriedade}' deve estar no formato '00000-XX' ";
        }

        public override bool IsValid(ValidationContext<T> contextoValidacao, string crm)
        {
            nomePropriedade = contextoValidacao.DisplayName;

            Regex rgx = new Regex(@"^\d{5}-[A-Za-z]{2}$");

            if (rgx.IsMatch(crm))
                return true;
            else
                return false;
        }
    }
}