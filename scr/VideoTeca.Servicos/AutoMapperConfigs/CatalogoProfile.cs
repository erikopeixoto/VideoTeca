using AutoMapper;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Modelos.Modelos;
using System.Linq;

namespace VideoTeca.Servicos.AutoMapperConfigs
{
    public class CatalogoProfile : Profile
    {
        public CatalogoProfile()
        {
            var configuracao = new MapperConfiguration(cfg =>
           {

               cfg.Advanced.AllowAdditiveTypeMapCreation = true; 
               CreateMap<Catalogo, CatalogoDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
               .ForMember(dest => dest.IdGenero, opt => opt.MapFrom(scr => scr.IdGenero))
               .ForMember(dest => dest.NomAutor, opt => opt.MapFrom(scr => scr.NomAutor))
               .ForMember(dest => dest.DesTitulo, opt => opt.MapFrom(scr => scr.DesTitulo))
               .ForMember(dest => dest.Codigo, opt => opt.MapFrom(scr => scr.Codigo))
               .ForMember(dest => dest.AnoLancamento, opt => opt.MapFrom(scr => scr.AnoLancamento))
               .ForMember(dest => dest.DtcAtualizacao, opt => opt.MapFrom(scr => scr.DtcAtualizacao))
               .ForMember(dest => dest.CatalogoTipoMidiasDto, opt => opt.MapFrom(scr => scr.CatalogoTipoMidias))
               .ForAllOtherMembers(dest => dest.Ignore());

               CreateMap<CatalogoTipoMidiaDto, CatalogoTipoMidia>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.IdTipoMidia, opt => opt.MapFrom(scr => scr.IdTipoMidia))
                .ForMember(dest => dest.IdCatalogo, opt => opt.MapFrom(scr => scr.IdCatalogo))
                .ForMember(dest => dest.QtdTitulo, opt => opt.MapFrom(scr => scr.QtdTitulo))
                .ForMember(dest => dest.QtdDisponivel, opt => opt.MapFrom(scr => scr.QtdDisponivel))
                .ForAllOtherMembers(dest => dest.Ignore());

               CreateMap<CatalogoDto, Catalogo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.IdGenero, opt => opt.MapFrom(scr => scr.IdGenero))
                .ForMember(dest => dest.NomAutor, opt => opt.MapFrom(scr => scr.NomAutor))
                .ForMember(dest => dest.DesTitulo, opt => opt.MapFrom(scr => scr.DesTitulo))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(scr => scr.Codigo))
                .ForMember(dest => dest.AnoLancamento, opt => opt.MapFrom(scr => scr.AnoLancamento))
                .ForMember(dest => dest.DtcAtualizacao, opt => opt.MapFrom(scr => scr.DtcAtualizacao))
                .ForAllOtherMembers(dest => dest.Ignore());

           });
        }
    }
}
