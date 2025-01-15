using Bogus;
using financesFlow.Comunicacao.Enums;
using financesFlow.Dominio.Entidades;
using financesFlow.Dominio.Enums;

namespace CommonTestUtilities.Entities;
public class DespesaBuilder
{
    public static List<Despesa> Collection(Usuario user, uint count = 2)
    {
        var list = new List<Despesa>();

        if (count == 0)
            count = 1;

        var expenseId = 1;

        for (int i = 0; i < count; i++)
        {
            var expense = Build(user);
            expense.Id = expenseId++;
            list.Add(expense);
        }
        return list;
    }

    public static Despesa Build(Usuario user)
    {
        return new Faker<Despesa>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.NomeDespesa, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Descricao, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.DataDespesa, faker => faker.Date.Past())
            .RuleFor(r => r.ValorDespesa, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.MetodoPagamento, faker => faker.PickRandom<<MetodoPagamento>>())
            .RuleFor(r => r.UserId, _ => user.Id)
            .RuleFor(r => r.Tags, faker => faker.Make(1, () => new CashFlow.Domain.Entities.Tag
            {
                Id = 1,
                Value = faker.PickRandom<financesFlow.Domain.Enums.Tag>(),
                ExpenseId = 1
            }));
    }
}