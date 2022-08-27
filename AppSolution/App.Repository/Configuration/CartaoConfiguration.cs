using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repository.Configuration
{
    public class CartaoConfiguration : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            // Table
            builder.ToTable("TB_PESSOA_CARTAO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoCartao });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoCartao).HasColumnName("CD_CARTAO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.CodigoUsuario).HasColumnName("CD_USUARIO").IsRequired();
            builder.Property(e => e.NumeroCatao).HasColumnName("DC_NUMERO_CARTAO").IsRequired();
            builder.Property(e => e.HashCartao).HasColumnName("DC_HASH_CARTAO").IsRequired();
            builder.Property(e => e.DataVencimentoCartao).HasColumnName("DT_VENCIMENTO").IsRequired();
            builder.Property(e => e.SenhaCartao).HasColumnName("DC_SENHA");


            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);
        }
    }
}
