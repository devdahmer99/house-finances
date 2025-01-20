using financesFlow.Comunicacao.Enums;

namespace financesFlow.Comunicacao.Responses.Despesa;
public class ResponseDespesaJson
{
    public long Id { get; set; }
    public string NomeDespesa { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataDespesa { get; set; }
    public decimal ValorDespesa { get; set; }
    public MetodoPagamento MetodoPagamento { get; set; }
    public IList<Tag> Tags { get; set; } = [];
}
