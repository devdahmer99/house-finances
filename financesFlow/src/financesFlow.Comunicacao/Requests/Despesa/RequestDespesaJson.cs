using financesFlow.Comunicacao.Enums;

namespace financesFlow.Comunicacao.Requests.Despesa;
public class RequestDespesaJson
{
    public string NomeDespesa { get; set; } = string.Empty;
    public string DescricaoDespesa { get; set; } = string.Empty;
    public DateTime DataDespesa { get; set; }
    public decimal ValorDespesa { get; set; }
    public MetodoPagamento FormaDePagamento { get; set; }
}
