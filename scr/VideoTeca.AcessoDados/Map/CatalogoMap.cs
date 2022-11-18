using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.AcessoDados.Map
{
    public class CatalogoMap : IEntityTypeConfiguration<Catalogo>
    {
        public void Configure(EntityTypeBuilder<Catalogo> builder)
        {
            builder.ToTable("catalogo").HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("id").IsRequired(true);
            builder.Property(t => t.Codigo).HasColumnName("cod_catalogo").IsRequired(true);
            builder.Property(t => t.IdGenero).HasColumnName("id_genero").IsRequired(true);
            builder.Property(t => t.DesTitulo).HasColumnName("des_titulo").IsRequired(true);
            builder.Property(t => t.NomAutor).HasColumnName("nom_autor").IsRequired(true);
            builder.Property(t => t.AnoLancamento).HasColumnName("ano_lancamento").IsRequired(true);
            builder.Property(t => t.DtcAtualizacao).HasColumnName("dtc_atualizacao").IsRequired(true);

            // builder.Ignore(c => c.CatalogoTipoMidias);
            // builder.HasData(new PhoneNumberType { PhoneNumberTypeID = 1, Name = "Local phone" });
            // builder.HasData(new PhoneNumberType { PhoneNumberTypeID = 2, Name = "Cellphone" });

        }
    }
}
