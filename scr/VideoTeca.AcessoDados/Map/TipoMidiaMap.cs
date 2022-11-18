using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.AcessoDados.Map
{
    public class TipoMidiaMap : IEntityTypeConfiguration<TipoMidia>
    {
        public void Configure(EntityTypeBuilder<TipoMidia> builder)
        {
            builder.ToTable("tipo_midia").HasKey(t => new { t.Id });

            builder.Property(t => t.Id).HasColumnName("id").IsRequired(true);
            builder.Property(t => t.Descricao).HasColumnName("des_tipo_midia").IsRequired(true);
            builder.Property(t => t.DtcAtualizacao).HasColumnName("dtc_atualizacao").IsRequired(false);

            builder.Ignore(t => t.Catalogos);
            builder.Ignore(t => t.CatalogoTipoMidias);
        }
    }
}
