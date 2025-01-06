using financesFlow.Dominio.Enums;
using financesFlow.Dominio.Reports;
namespace financesFlow.Dominio.Extensões;
public static class TipoMetodoPagamento
{
    public static string TipoMetodoPagamentoToString(this MetodoPagamento metodoPagamento)
    {
        return metodoPagamento switch
        {
            MetodoPagamento.Pix => ResourceReportGenerationMessages.PIX,
            MetodoPagamento.Cartão => ResourceReportGenerationMessages.CARTAO,
            MetodoPagamento.Boleto => ResourceReportGenerationMessages.BOLETO,
            _ => "Não identificado"
        };
    }
}
