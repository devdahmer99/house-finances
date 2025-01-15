using System.Net;
using FluentAssertions;
namespace WebApi.Test.Usuarios.Delete
{
    public class DeletaUsuarioTest : financesFlowClassFixture
    {
        private const string METHOD = "api/User";

        private readonly string _token;

        public DeletaUsuarioTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.User_Team_Member.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoDelete(METHOD, _token);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
