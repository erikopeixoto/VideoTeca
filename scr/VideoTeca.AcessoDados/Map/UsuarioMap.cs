using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.AcessoDados.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario").HasKey(t => new { t.Id });

            builder.Property(t => t.Id).HasColumnName("id").IsRequired(true).ValueGeneratedOnAdd();
            builder.Property(t => t.Nome).HasColumnName("nom_usuario").IsRequired(true);
            builder.Property(t => t.Email).HasColumnName("email").IsRequired(false);
            builder.Property(t => t.DtcAtualizacao).HasColumnName("dtc_atualizacao").HasDefaultValue(DateTime.Now).IsRequired(true);
        }
    }
}
