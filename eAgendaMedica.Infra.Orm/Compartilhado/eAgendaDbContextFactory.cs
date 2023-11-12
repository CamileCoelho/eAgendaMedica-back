﻿namespace eAgendaMedica.Infra.Orm
{
    public class eAgendaMedicaDbContextFactory : IDesignTimeDbContextFactory<eAgendaMedicaDbContext>
    {
        public eAgendaMedicaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();

            builder.UseSqlServer(@"Data Source=(LOCALDB)\\MSSQLLOCALDB;Initial Catalog=eAgendaMedica;Integrated Security=True");

            return new eAgendaMedicaDbContext(builder.Options);
        }
    }
}