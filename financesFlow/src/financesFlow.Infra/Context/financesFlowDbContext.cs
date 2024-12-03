using financesFlow.Aplicacao.Entidades.Despesa;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.Context
{
    public class financesFlowDbContext : DbContext
    {
        public financesFlowDbContext(DbContextOptions<financesFlowDbContext> options) : base(options)
        { }
        
        public DbSet<Despesa> Despesas { get; set; }
    }
}
