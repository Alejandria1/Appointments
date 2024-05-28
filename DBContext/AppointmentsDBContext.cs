using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Appointments.Models
{
    public class AppointmentsDBContext : DbContext
    {

        public DbSet<Appointment> Appointments { get; set; }
        public AppointmentsDBContext(DbContextOptions<AppointmentsDBContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {

                    if (!databaseCreator.CanConnect())
                    {
                        throw new Exception("cannot connect to db");
                    }
                    if (!databaseCreator.HasTables())
                    {
                        throw new Exception("the db is empty");
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
