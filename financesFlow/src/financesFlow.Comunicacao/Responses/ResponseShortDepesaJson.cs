using financesFlow.Comunicacao.Enums;

namespace financesFlow.Comunicacao.Responses;
public class ResponseShortDepesaJson
{
    public long Id { get; set; }
    public string NomeDespesa { get; set; } = string.Empty;
    public decimal ValorDespesa { get; set; }
    public DateTime DataDespesa { get; set; }
    public MetodoPagamento MetodoPagamento { get; set; }

}
