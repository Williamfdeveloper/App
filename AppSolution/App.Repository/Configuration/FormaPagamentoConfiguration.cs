using App.Domain;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repository.Configuration
{
    public class FormaPagamentoConfiguration : IEntityTypeConfiguration<FormaPagamento>
    {
        public void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            // Table
            builder.ToTable("TB_FORMA_PAGAMENTO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoFormaPagamento });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoFormaPagamento).HasColumnName("CD_FORMA_PAGAMENTO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.DescricaoFormaPagamento).HasColumnName("DC_FORMA_PAGAMENTO").IsRequired();
            builder.Property(e => e.IdTipoFormaPagamento).HasColumnName("ID_TIPO_FORMA_PAGAMENTO").IsRequired();

            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);



            builder.HasData(
                new FormaPagamento
                {
                    CodigoFormaPagamento = 1,
                    DescricaoFormaPagamento = "Boleto",
                    IdTipoFormaPagamento = (int)Constant.FormaPagamento.Boleto
                },
                new FormaPagamento
                {
                    CodigoFormaPagamento = 2,
                    DescricaoFormaPagamento = "Credito",
                    IdTipoFormaPagamento = (int)Constant.FormaPagamento.Credito
                },
                new FormaPagamento
                {
                    CodigoFormaPagamento = 3,
                    DescricaoFormaPagamento = "Debito",
                    IdTipoFormaPagamento = (int)Constant.FormaPagamento.Debito
                }

            );

        }
    }
}
