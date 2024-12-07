using financesFlow.Aplicacao.useCase.Despesa.Registrar;
using financesFlow.Comunicacao.Requests;

namespace ValidatorsTests.Despesa.Registrar;
public class RegistrarDespesaTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegistrarValidacaoDespesa();
        var request = new RequestDespesaJson 
        {
            ValorDespesa = 100,
            NomeDespesa = "Teste",
            DescricaoDespesa = "despesa de teste",
            FormaDePagamento = financesFlow.Comunicacao.Enums.MetodoPagamento.Pix,
            DataDespesa = DateTime.Now.AddDays(-1)
        };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}
