using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VideoTeca.AcessoDados.Contexto;

namespace VideoTeca.AcessoDados.ConfigMigracao
{
    public class FabricaBanco : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionBuider = new DbContextOptionsBuilder<DataContext>();
            optionBuider.UseSqlServer("Data Source=TD-622\\SQLEXPRESS;Integrated Security=false;Initial Catalog=VideoTeca;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;User ID=sa;Password=123456");
            return new DataContext(optionBuider.Options);

            // Add-Migration Base-Inicial
        }
    }
}
