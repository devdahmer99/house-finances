using financesFlow.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.DataAccess;
public class financesFlowDbContext : DbContext
{
    public financesFlowDbContext(DbContextOptions<financesFlowDbContext>options) : base (options){}

    public DbSet<Despesa> Despesas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

}
