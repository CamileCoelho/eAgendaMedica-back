using FluentValidation;
using FluentValidation.Validators;

namespace eAgendaMedica.Dominio.Compartilhado.Validators
{
    public class SemCaracteresEspeciaisValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "SemCaracteresEspeciaisValidator";

        private string nomePropriedade;

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"'{nomePropriedade}' deve ser composto por apenas letras e/ou números.";
        }

        public override bool IsValid(ValidationContext<T> contextoValidacao, string texto)
        {
            nomePropriedade = contextoValidacao.DisplayName;

            if (string.IsNullOrEmpty(texto))
                return false;

            bool estaValido = true;

            foreach (char letra in texto)
            {
                if (letra == ' ')
                    continue;

                if (char.IsLetterOrDigit(letra) == false)
                {
                    estaValido = false;
                    break;
                }
            }

            return estaValido;
        }
    }
}
