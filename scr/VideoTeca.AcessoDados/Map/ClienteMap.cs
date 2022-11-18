using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.AcessoDados.Map
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente").HasKey(t => new { t.Id });

            builder.Property(t => t.Id).HasColumnName("id").IsRequired(true);
            builder.Property(t => t.NomCliente).HasColumnName("nom_cliente").IsRequired(true);
            builder.Property(t => t.TipoPessoa).HasColumnName("cod_tipo_pessoa").IsRequired(true);
            builder.Property(t => t.NumDocumento).HasColumnName("num_documento").IsRequired(true);
            builder.Property(t => t.NumCep).HasColumnName("num_cep").IsRequired(true);
            builder.Property(t => t.DesMunicipio).HasColumnName("des_municipio").IsRequired(true);
            builder.Property(t => t.DesBairro).HasColumnName("des_bairro").IsRequired(true);
            builder.Property(t => t.DesLogradouro).HasColumnName("des_logradouro").IsRequired(true);
            builder.Property(t => t.NumTelefone).HasColumnName("num_telefone").IsRequired(true);
            builder.Property(t => t.NumEndereco).HasColumnName("num_endereco").IsRequired(true);
            builder.Property(t => t.DesComplemento).HasColumnName("des_complemento").IsRequired(false);
            builder.Property(t => t.DtcNascimento).HasColumnName("dtc_nascimento").IsRequired(false);
            builder.Property(t => t.DtcAtualizacao).HasColumnName("dtc_atualizacao").IsRequired(false);

            builder.Ignore(t => t.ClienteCatalogoTipoMidias);

            //builder.HasOne(c => c.FoneTipo)
            //       .WithMany(c => c.VideoTecas)
            //       .HasForeignKey(c => c.FoneTipoId);

            //builder.HasOne(c => c.Pessoas)
            //       .WithMany(c => c.VideoTecas)
            //       .HasForeignKey(c => c.PessoasId);

            // builder.HasData(new PersonPhone { BusinessEntityID = 1, PhoneNumber = "(19)99999-2883", PhoneNumberTypeID = 1 });
            // builder.HasData(new PersonPhone { BusinessEntityID = 1, PhoneNumber = "(19)99999-4021", PhoneNumberTypeID = 2 });
        }
    }
}
