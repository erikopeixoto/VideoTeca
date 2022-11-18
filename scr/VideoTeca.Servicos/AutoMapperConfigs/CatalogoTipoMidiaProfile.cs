using AutoMapper;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Servicos.AutoMapperConfigs
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
               .ForMember(dest => dest.Descricao, opt => opt.MapFrom(scr => scr.TipoMidia.Descricao))
               .ForMember(dest => dest.QtdTitulo, opt => opt.MapFrom(scr => scr.QtdTitulo))
               .ForAllOtherMembers(dest => dest.Ignore());
           });
        }
    }
}
