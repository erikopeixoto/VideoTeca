using AutoMapper;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Servicos.AutoMapperConfigs
{
    public class TipoMidiaProfile : Profile
    {
        public TipoMidiaProfile()
        {
            var configuracao = new MapperConfiguration(cfg =>
           {
               CreateMap<TipoMidia, TipoMidiaDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
               .ForMember(dest => dest.Descricao, opt => opt.MapFrom(scr => scr.Descricao))
               .ForMember(dest => dest.DtcAtualizacao, opt => opt.MapFrom(scr => scr.DtcAtualizacao))
               .ReverseMap()
               .ForAllOtherMembers(dest => dest.Ignore());
           });
        }
    }
}
