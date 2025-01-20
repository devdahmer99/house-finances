using AutoMapper;
using financesFlow.Aplicacao.AutoMapper;

namespace CommonTestsUtilitis.Mapper;
public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });

        return mapper.CreateMapper();
    }
}
