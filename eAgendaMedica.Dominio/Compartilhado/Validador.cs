using System.Text.RegularExpressions;

namespace eAgendaMedica.Dominio.Compartilhado
{
    public class Validador
    {
        public Validador()
        {
            
        }

        public bool ValidaString(string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return true;
            else
                return false;
        }

        public bool ValidaEnum(Enum e)
        {
            if (string.IsNullOrEmpty(e.ToString()) && string.IsNullOrWhiteSpace(e.ToString()))
                return true;
            else
                return false;
        }

        public bool ValidaTelefone(string telefone)
        {
            // formato (XX)9XXXX-XXXX
            Regex rgx = new Regex(@"^\(\d{2}\)\s9\d{4}-\d{4}$");

            if (rgx.IsMatch(telefone))
                return false;
            else
                return true;
        }

        public bool ValidaFormatoEmail(string email)
        {
            // formato permitido: qualquer coisa antes do "@" seguido por pelo menos uma letra depois
            Regex rgx = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

            if (rgx.IsMatch(email))
                return false;
            else
                return true;
        }

        public bool ValidaEmail(string email)
        { 
            // formato permitido: qualquer coisa antes do "@" seguido por pelo menos um caractere depois
            Regex rgx = new(@"^[^\s@]+@[^\s@]+$");

            if (rgx.IsMatch(email))
                return false;
            else
                return true;
        }

        public bool ValidaDateTime(DateTime date)
        {
            if (date == DateTime.MinValue)
                return true;
            if (string.IsNullOrEmpty(date.ToString()) || string.IsNullOrWhiteSpace(date.ToString()))
                return true;
            else
                return false;
        }

        public bool ValidaDateTimeComTimeOnly(DateTime date)
        {
            if (date == DateTime.MinValue)
                return true;
            if (TimeOnly.FromDateTime(date) == TimeOnly.MinValue)
                return true;
            if (string.IsNullOrEmpty(date.ToString()) || string.IsNullOrWhiteSpace(date.ToString()))
                return true;
            else
                return false;
        }

        public bool ValidaDateOnly(DateOnly data)
        {
            if (string.IsNullOrEmpty(data.ToString()) || string.IsNullOrWhiteSpace(data.ToString()))
                return true;
            else
                return false;
        }

        public bool ValidaTimeOnly(TimeOnly hora)
        {
            if (string.IsNullOrEmpty(hora.ToString()) || string.IsNullOrWhiteSpace(hora.ToString()))
                return true;
            if (hora == TimeOnly.MinValue)
                return true;
            else
                return false;
        }
    }
}
