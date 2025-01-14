using Bogus;
using financesFlow.Comunicacao.Enums;
using financesFlow.Comunicacao.Requests.Despesa;

namespace CommonTestsUtilitis.Requests;
public class RequestDespesaJsonBuilder
{
    public static RequestDespesaJson Build()
    {
       return new Faker<RequestDespesaJson>()
            .RuleFor(request => request.NomeDespesa, faker => faker.Commerce.ProductName())
            .RuleFor(request => request.Descricao, faker => faker.Lorem.Paragraph())
            .RuleFor(request => request.DataDespesa, faker => faker.Date.Past())
            .RuleFor(request => request.MetodoPagamento, faker => faker.PickRandom<MetodoPagamento>())
            .RuleFor(request => request.ValorDespesa, faker => faker.Random.Decimal());
    }
}
