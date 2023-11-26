using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Integration.TestProject.Compartilhado
{
    public abstract class RepositorioBaseTest
    {
        protected readonly eAgendaMedicaDbContext dbContext;
        public RepositorioBaseTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();

            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSqlLocalDB;Initial Catalog=eAgendaMedicaForTesting;Integrated Security=True");

            dbContext = new eAgendaMedicaDbContext(optionsBuilder.Options);

            dbContext.Database.Migrate();
        }
    }
}
