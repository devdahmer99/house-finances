using Bogus;
using financesFlow.Comunicacao.Enums;
using financesFlow.Comunicacao.Requests;

namespace CommonTestsUtilitis.Requests;
public class RequestDespesaJsonBuilder
{
    public static RequestDespesaJson Build()
    {
       return new Faker<RequestDespesaJson>()
            .RuleFor(request => request.NomeDespesa, faker => faker.Commerce.ProductName())
            .RuleFor(request => request.DescricaoDespesa, faker => faker.Lorem.Paragraph())
            .RuleFor(request => request.DataDespesa, faker => faker.Date.Past())
            .RuleFor(request => request.FormaDePagamento, faker => faker.PickRandom<MetodoPagamento>())
            .RuleFor(request => request.ValorDespesa, faker => faker.Random.Decimal());
    }
}
