using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace api.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS01;Database=FinShark;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            return new ApplicationDBContext(optionsBuilder.Options);
        }
    }
}