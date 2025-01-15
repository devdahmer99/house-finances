using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DocumentFormat.OpenXml.Spreadsheet;
using financesFlow.Comunicacao.Requests.Usuario;

namespace CommonTestsUtilitis.Requests
{
    public class RequestAtualizaUsuarioBuilder
    {
        public static RequestAtualizaUsuarioJson Build()
        {
            return new Faker<RequestAtualizaUsuarioJson>()
                .RuleFor(user => user.Nome, faker => faker.Person.FirstName)
                .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Nome));
        }

    }
}
