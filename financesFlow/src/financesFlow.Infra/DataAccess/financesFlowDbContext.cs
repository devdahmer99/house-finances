using financesFlow.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.DataAccess;
public class financesFlowDbContext : DbContext
{
    public DbSet<Despesa> Despesas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=financeflow;Uid=root;Pwd=1099";
        var serverVersion = new MySqlServerVersion(new Version(9,1,0));
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}
