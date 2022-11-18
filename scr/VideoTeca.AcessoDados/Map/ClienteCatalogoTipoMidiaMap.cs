using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.AcessoDados.Map
{
    public class ClienteCatalogoTipoMidiaMap : IEntityTypeConfiguration<ClienteCatalogoTipoMidia>
    {
        public void Configure(EntityTypeBuilder<ClienteCatalogoTipoMidia> builder)
        {
            builder.ToTable("cliente_catalogo_tipo_midia").HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("id").IsRequired(true).ValueGeneratedOnAdd();
            builder.Property(t => t.IdCliente).HasColumnName("id_cliente").IsRequired(true);
            builder.Property(t => t.IdCatalogoTipoMidia).HasColumnName("id_catalogo_tipo_midia").IsRequired(true);
            builder.Property(t => t.QtdTitulo).HasColumnName("qtd_titulo").IsRequired(true);
            builder.Property(t => t.DtcLocacao).HasColumnName("dtc_locacao").IsRequired(true);
            builder.Property(t => t.DtcEntrega).HasColumnName("dtc_entrega").IsRequired(true);
            builder.Property(t => t.DtcDevolucao).HasColumnName("dtc_devolucao").IsRequired(false);
            builder.Property(t => t.DtcAtualizacao).HasColumnName("dtc_atualizacao").IsRequired(true);

            builder.HasOne(c => c.Cliente)
                   .WithMany(c => c.ClienteCatalogoTipoMidias)
                   .HasForeignKey(c => c.IdCliente);

            builder.HasOne(c => c.CatalogoTipoMidia)
                   .WithMany(c => c.ClienteCatalogoTipoMidias)
                   .HasForeignKey(c => c.IdCatalogoTipoMidia);
        }
    }
}
