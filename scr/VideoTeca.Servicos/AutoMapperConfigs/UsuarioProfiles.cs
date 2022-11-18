using AutoMapper;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Servicos.AutoMapperConfigs
{
    public class UsuarioProfiles : Profile
    {
        public UsuarioProfiles()
        {
            //CreateMap<Usuario, UserDto>().ReverseMap();
            CreateMap<Usuario, UserLoginDto>()
               .ForMember(dest => dest.login, opt => opt.MapFrom(scr => scr.Login))
               .ForMember(dest => dest.email, opt => opt.MapFrom(scr => scr.Email))
               .ForMember(dest => dest.nomeCompleto, opt => opt.MapFrom(scr => scr.Nome))
               .ForMember(dest => dest.senha, opt => opt.MapFrom(scr => scr.Senha))
               .ReverseMap()
               .ForAllOtherMembers(dest => dest.Ignore());
;
        }
    }
}