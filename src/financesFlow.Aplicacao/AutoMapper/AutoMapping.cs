﻿using AutoMapper;
using financesFlow.Comunicacao.Requests.Despesa;
using financesFlow.Comunicacao.Requests.Usuario;
using financesFlow.Comunicacao.Responses.Despesa;
using financesFlow.Comunicacao.Responses.Usuario;
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
        CreateMap<RequestCriaUsuarioJson, Usuario>()
            .ForMember(dest => dest.Senha, conf => conf.Ignore());
        CreateMap<Comunicacao.Enums.Tag, Tag>();
    }

    public void RetornaEntidade()
    {
        CreateMap<Despesa, ResponseDespesaJson>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(tag => tag.Value).ToList()));
        CreateMap<Despesa, ResponseShortDepesaJson>();
        CreateMap<Usuario, ResponseCriaUsuarioJson>();
        CreateMap<Usuario, RequestUserProfileJson>();
    }
}
