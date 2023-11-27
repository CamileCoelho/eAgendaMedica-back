using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Integration.TestProject.Compartilhado
{
    public abstract class RepositorioBaseTest
    {
        protected string connectionString = "Data Source=(LocalDb)\\MSSqlLocalDB;Initial Catalog=eAgendaMedicaForTesting;Integrated Security=True";

        protected readonly eAgendaMedicaDbContext dbContext;

        public RepositorioBaseTest()
        {
            Guid usuarioId = Guid.Parse("A8BC593B-5945-417B-3C2A-08DBE8775234");

            var optionsBuilder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();

            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSqlLocalDB;Initial Catalog=eAgendaMedicaForTesting;Integrated Security=True");

            dbContext = new eAgendaMedicaDbContext(optionsBuilder.Options, new TestTenantProvider(usuarioId));

            dbContext.Database.Migrate();
        }

        public static Guid RegistrarUsuario()
        {
            Guid usuarioId = Guid.Parse("A8BC593B-5945-417B-3C2A-08DBE8775234");

            var sql =
                        @$"INSERT INTO ASPNETUSERS
                (
                    ID,
                    NOME,
                    USERNAME,
                    NORMALIZEDUSERNAME,
                    EMAIL,
                    NORMALIZEDEMAIL,
                    EMAILCONFIRMED,
                    PASSWORDHASH,
                    SECURITYSTAMP,
                    CONCURRENCYSTAMP,
                    PHONENUMBER,
                    PHONENUMBERCONFIRMED,
                    TWOFACTORENABLED,
                    LOCKOUTEND,
                    LOCKOUTENABLED,
                    ACCESSFAILEDCOUNT
                )
                VALUES
                (
                    '{usuarioId}',
                    'TESTE TESTE',
                    'TESTE@GMAIL.COM',
                    'TESTE@GMAIL.COM',
                    'TESTE@GMAIL.COM',
                    'TESTE@GMAIL.COM',
                    1,
                    'AQAAAAEAACCQAAAAEL/NE00SPXPMU7SRDGSENWX7TKLQNMKI9AEYIDFGYKLGT1V6YFH+QEGZJMF5HVBN8G==',
                    'FSNVOM5DIYV67KMJWQBDDIE3OSR57XTN',
                    'CAB37435-2315-4C44-AB99-12EF2C7D91A4',
                    NULL,
                    0,
                    0,
                    NULL,
                    1,
                    0
                )";

            SqlConnection conexao = new SqlConnection("Data Source=(LocalDb)\\MSSqlLocalDB;Initial Catalog=eAgendaMedicaForTesting;Integrated Security=True");
           
            conexao.Open();
            
            SqlCommand comando = new SqlCommand(sql, conexao);
            
            comando.ExecuteNonQuery();
            conexao.Close();
            
            return usuarioId;
        }

        public static void ApagarDados()
        {
            var sql = @$"DELETE FROM TBCirurgia_TBMedico;";

            sql += @$"DELETE FROM TBCirurgia;";

            sql += @$"DELETE FROM TBConsulta;";

            sql += @$"DELETE FROM TBMedico;";

            sql += @$"DELETE FROM ASPNETUSERS";

            SqlConnection conexao = new SqlConnection(@"Data Source=(LocalDb)\MSSqlLocalDb;Initial Catalog=eAgendaMedicaForTesting;Integrated Security=True");

            conexao.Open();

            SqlCommand comando = new SqlCommand(sql, conexao);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
