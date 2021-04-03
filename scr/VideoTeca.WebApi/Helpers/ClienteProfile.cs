using VideoTeca.Modelos.Dtos;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Util;
using AutoMapper;

namespace VideoTeca.WebApi.Helpers
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            var configuracao = new MapperConfiguration(cfg =>
           {
               CreateMap<Cliente, ClienteDto>()
               .ForMember(dest => dest.NomCliente, opt => opt.MapFrom(scr => scr.NomCliente))
               .ForMember(dest => dest.DesMunicipio, opt => opt.MapFrom(scr =>scr.DesMunicipio))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
               .ForMember(dest => dest.NumDocumentoFormatado, opt => opt.MapFrom(scr => Util.FormatarDocumento(scr.NumDocumento, scr.TipoPessoa)))
               .ForMember(dest => dest.FoneFormatado, opt => opt.MapFrom(scr => Util.FormatarFone(scr.NumTelefone)))
               //.ReverseMap()
               .ForAllOtherMembers(dest => dest.Ignore());
           });

            //CreateMap<Cliente, ClienteDto>()
            //    .ForAllOtherMembers(dest => dest.Ignore());

       }

    }
}
 