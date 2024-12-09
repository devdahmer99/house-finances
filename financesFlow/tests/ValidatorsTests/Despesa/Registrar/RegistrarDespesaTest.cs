using CommonTestsUtilitis.Requests;
using financesFlow.Aplicacao.useCase.Despesa.Registrar;
using financesFlow.Comunicacao.Enums;
using financesFlow.Exception;
using FluentAssertions;

namespace ValidatorsTests.Despesa.Registrar;
public class RegistrarDespesaTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegistrarValidacaoDespesa();

        var request = RequestDespesaJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Nome_Vazio()
    {
        var validator = new RegistrarValidacaoDespesa();
        var request = RequestDespesaJsonBuilder.Build();
        request.NomeDespesa = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.NOME_VAZIO));
    }

    [Fact]
    public void Data_Incorreta()
    {
        var validator = new RegistrarValidacaoDespesa();
        var request = RequestDespesaJsonBuilder.Build();
        request.DataDespesa = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.DATA_INCORRETA_OU_MAIOR));
    }

    [Fact]
    public void Metodo_Pagamento_Invalido()
    {
        var validator = new RegistrarValidacaoDespesa();
        var request = RequestDespesaJsonBuilder.Build();
        request.FormaDePagamento = (MetodoPagamento)700;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.METODO_PAGAMENTO_INEXISTENTE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Valor_Incorreto(decimal valorPagamento)
    {
        var validator = new RegistrarValidacaoDespesa();
        var request = RequestDespesaJsonBuilder.Build();
        request.ValorDespesa = valorPagamento;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.VALOR_DESPESA));
    }
}
