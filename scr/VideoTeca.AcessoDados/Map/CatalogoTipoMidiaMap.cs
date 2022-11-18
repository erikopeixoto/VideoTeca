using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.AcessoDados.Map
{
    public class CatalogoTipoMidiaMap : IEntityTypeConfiguration<CatalogoTipoMidia>
    {
        public void Configure(EntityTypeBuilder<CatalogoTipoMidia> builder)
        {
            builder.ToTable("catalogo_tipo_midia").HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("id").IsRequired(true);
            builder.Property(t => t.IdCatalogo).HasColumnName("id_catalogo").IsRequired(true);
            builder.Property(t => t.IdTipoMidia).HasColumnName("id_tipo_midia").IsRequired(true);
            builder.Property(t => t.QtdTitulo).HasColumnName("qtd_titulo").IsRequired(true);
            builder.Property(t => t.QtdDisponivel).HasColumnName("qtd_disponivel").IsRequired(true);
            builder.Property(t => t.DtcAtualizacao).HasColumnName("dtc_atualizacao").IsRequired(true);

            builder.HasOne(c => c.Catalogo)
                   .WithMany(c => c.CatalogoTipoMidias)
                   .HasForeignKey(c => c.IdCatalogo);

            builder.HasOne(c => c.TipoMidia)
                   .WithMany(c => c.CatalogoTipoMidias)
                   .HasForeignKey(c => c.IdTipoMidia);
        }
    }
}
