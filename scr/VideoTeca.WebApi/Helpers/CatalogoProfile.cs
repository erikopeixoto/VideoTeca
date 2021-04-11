using VideoTeca.Modelos.Dtos;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Util;
using AutoMapper;

namespace VideoTeca.WebApi.Helpers
{
    public class CatalogoProfile : Profile
    {
        public CatalogoProfile()
        {
            var configuracao = new MapperConfiguration(cfg =>
           {
               CreateMap<Catalogo, CatalogoDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
               .ForMember(dest => dest.IdGenero, opt => opt.MapFrom(scr => scr.IdGenero))
               .ForMember(dest => dest.NomAutor, opt => opt.MapFrom(scr => scr.NomAutor))
               .ForMember(dest => dest.DesTitulo, opt => opt.MapFrom(scr => scr.DesTitulo))
               .ForMember(dest => dest.Codigo, opt => opt.MapFrom(scr => scr.Codigo))
               .ForMember(dest => dest.AnoLancamento, opt => opt.MapFrom(scr => scr.AnoLancamento))
               .ForAllOtherMembers(dest => dest.Ignore());
           });
       }
    }
}
 