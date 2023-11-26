using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm
{
    public static class MigradorBancoDadoseAgendaMedica
    {
        public static bool AtualizarBancoDados(DbContext db)
        {
            var qtdMigracoesPendentes = db.Database.GetPendingMigrations().Count();

            if (qtdMigracoesPendentes == 0)
                return false;

            db.Database.Migrate();

            return true;
        }
    }
}
