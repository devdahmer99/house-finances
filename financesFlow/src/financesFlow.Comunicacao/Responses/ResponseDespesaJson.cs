using financesFlow.Comunicacao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace financesFlow.Comunicacao.Responses;
public class ResponseDespesaJson
{
    public string NomeDespesa { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public MetodoPagamento Pagamento { get; set; }

}
