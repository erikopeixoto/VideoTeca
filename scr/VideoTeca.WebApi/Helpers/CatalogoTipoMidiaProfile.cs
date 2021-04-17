using VideoTeca.Modelos.Dtos;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Util;
using AutoMapper;

namespace VideoTeca.WebApi.Helpers
{
    public class CatalogoTipoMidiaProfile : Profile
    {
        public CatalogoTipoMidiaProfile()
        {
            var configuracao = new MapperConfiguration(cfg =>

           {
               CreateMap<CatalogoTipoMidia, CatalogoTipoMidiaDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
               .ForMember(dest => dest.IdCatalogo, opt => opt.MapFrom(scr => scr.IdCatalogo))
               .ForMember(dest => dest.IdTipoMidia, opt => opt.MapFrom(scr => scr.IdTipoMidia))
               .ForMember(dest => dest.QtdTitulo, opt => opt.MapFrom(scr => scr.QtdTitulo))
               .ForAllOtherMembers(dest => dest.Ignore());
            });
       }
    }
}
 