using financesFlow.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace financesFlow.Infra.DataAccess;
public class financesFlowDbContext : DbContext
{
    public financesFlowDbContext(DbContextOptions<financesFlowDbContext>options) : base (options){}

    public DbSet<Despesa> Despesas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Despesa>()
            .HasOne(des => des.Usuario)
            .WithMany(usu => usu.Despesas)
            .HasForeignKey(des => des.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
