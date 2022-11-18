using Microsoft.EntityFrameworkCore;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Map;
using System;

namespace VideoTeca.AcessoDados.Contexto
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<TipoMidia> TipoMidias { get; set; }
        public DbSet<CatalogoTipoMidia> CatalogoTipoMidias { get; set; }
        public DbSet<ClienteCatalogoTipoMidia> ClienteCatalogoTipoMidias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new ClienteMap());
                modelBuilder.ApplyConfiguration(new GeneroMap());
                modelBuilder.ApplyConfiguration(new TipoMidiaMap());
                modelBuilder.ApplyConfiguration(new CatalogoMap());
                modelBuilder.ApplyConfiguration(new CatalogoTipoMidiaMap());
                modelBuilder.ApplyConfiguration(new ClienteCatalogoTipoMidiaMap());
                modelBuilder.ApplyConfiguration(new UsuarioMap());
                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
 