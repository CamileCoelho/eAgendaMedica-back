using FluentValidation.Results;

namespace eAgendaMedica.Dominio.Compartilhado
{ 
    public interface IValidador<T> where T : EntidadeBase<T>
    {
        public ValidationResult Validate(T instance);
    }
}
