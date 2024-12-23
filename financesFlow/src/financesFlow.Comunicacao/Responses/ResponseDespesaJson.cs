using financesFlow.Comunicacao.Enums;

namespace financesFlow.Comunicacao.Responses;
public class ResponseDespesaJson
{
    public string NomeDespesa { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public MetodoPagamento Pagamento { get; set; }

}
