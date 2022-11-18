namespace VideoTeca.Servicos.AutoMapperConfigs
{
    public static class Configs
    {
        public static AutoMapper.MapperConfiguration RegisterMappings() =>
            new AutoMapper.MapperConfiguration(cfg =>
           {
               cfg.AllowNullCollections = true;
               cfg.AddProfile<UsuarioProfiles>();
               cfg.AddProfile<ClienteProfile>();
               cfg.AddProfile<CatalogoTipoMidiaProfile>();
               cfg.AddProfile<CatalogoProfile>();
           });
    }
}
