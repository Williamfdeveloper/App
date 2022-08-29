using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repository.Configuration
{
    public class CartaoConfiguration : IEntityTypeConfiguration<DadosCartao>
    {
        public void Configure(EntityTypeBuilder<DadosCartao> builder)
        {
            // Table
            builder.ToTable("TB_PESSOA_CARTAO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoDadosCartao });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoDadosCartao).HasColumnName("CD_CARTAO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.CodigoUsuario).HasColumnName("CD_USUARIO").IsRequired();
            builder.Property(e => e.NumeroCartao).HasColumnName("DC_NUMERO_CARTAO").IsRequired();
            builder.Property(e => e.NomeCartao).HasColumnName("DC_NOME_CARTAO").IsRequired();
            builder.Property(e => e.HashCartao).HasColumnName("DC_HASH_CARTAO").IsRequired();
            builder.Property(e => e.DataVencimentoCartao).HasColumnName("DT_VENCIMENTO").IsRequired();


            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);
        }
    }
}
