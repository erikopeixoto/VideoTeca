using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.AcessoDados.Map
{
    public class GeneroMap : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Genero").HasKey(t => new { t.Id });

            builder.Property(t => t.Id).HasColumnName("id").IsRequired(true);
            builder.Property(t => t.Descricao).HasColumnName("des_genero").IsRequired(true);
            builder.Property(t => t.DtcAtualizacao).HasColumnName("dtc_atualizacao").IsRequired(false);

            builder.Ignore(t => t.Catalogos);
        }
    }
}
