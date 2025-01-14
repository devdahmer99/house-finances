using financesFlow.Dominio.Enums;

namespace financesFlow.Dominio.Entidades;
public class Despesa
{
    public long Id { get; set; }
    public string NomeDespesa { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataDespesa { get; set; }
    public decimal ValorDespesa { get; set; }
    public MetodoPagamento MetodoPagamento { get; set; }
    public long UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = default!;
}
