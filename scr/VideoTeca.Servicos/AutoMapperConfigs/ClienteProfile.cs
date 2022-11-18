using AutoMapper;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Servicos.AutoMapperConfigs
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            var configuracao = new MapperConfiguration(cfg =>
           {
               CreateMap<Cliente, ClienteDto>()
               .ForMember(dest => dest.NomCliente, opt => opt.MapFrom(scr => scr.NomCliente))
               .ForMember(dest => dest.DesMunicipio, opt => opt.MapFrom(scr => scr.DesMunicipio))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
               .ForMember(dest => dest.NumDocumentoFormatado, opt => opt.MapFrom(scr => Util.Util.FormatarDocumento(scr.NumDocumento, scr.TipoPessoa)))
               .ForMember(dest => dest.FoneFormatado, opt => opt.MapFrom(scr => Util.Util.FormatarFone(scr.NumTelefone)))
               .ForAllOtherMembers(dest => dest.Ignore());
           });
        }
    }
}
