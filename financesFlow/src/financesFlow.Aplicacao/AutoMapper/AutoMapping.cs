using AutoMapper;
using financesFlow.Comunicacao.Requests;
using financesFlow.Comunicacao.Responses;
using financesFlow.Dominio.Entidades;

namespace financesFlow.Aplicacao.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        BuscaEntidade();
        RetornaEntidade();
    }


    public void BuscaEntidade()
    {
        CreateMap<RequestDespesaJson, Despesa>();
    }


    public void RetornaEntidade()
    {
        CreateMap<Despesa, ResponseDespesaJson>();
    }
}
