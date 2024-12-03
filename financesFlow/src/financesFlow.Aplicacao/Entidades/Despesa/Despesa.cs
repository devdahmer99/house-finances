using System.ComponentModel.DataAnnotations;
using financesFlow.Aplicacao.Enums.Despesa_Tipo_Pagamento;

namespace financesFlow.Aplicacao.Entidades.Despesa
{
    public class Despesa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeDespesa { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataCadastroDespesa { get; set; }

        [Required]
        public MetodosPagamentoEntidade TipoPagamento { get; set; }

        [Required]
        public decimal ValorDespesa { get; set; }
    }
}
