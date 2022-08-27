using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public class PedidoPagamentoConfiguration : IEntityTypeConfiguration<PedidoPagamento>
    {
        public void Configure(EntityTypeBuilder<PedidoPagamento> builder)
        {
            // Table
            builder.ToTable("TB_PEDIDO_PAGAMENTO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoPedidoPagamento });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoPedidoPagamento).HasColumnName("CD_PEDIDO_PAGAMENTO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.CodigoPedido).HasColumnName("CD_PEDIDO").IsRequired();
            builder.Property(e => e.IdSituacaoPagamento).HasColumnName("ID_SITUACAO_PAGAMENTO").IsRequired();
            builder.Property(e => e.DataPagamento).HasColumnName("DH_PAGAMENTO").IsRequired();
            builder.Property(e => e.DataAprovado).HasColumnName("DH_PAGAMENTO_APROVADO").IsRequired();
            builder.Property(e => e.DataAtualizacao).HasColumnName("DH_ATUALIZACAO").IsRequired();


            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);
        }
    }
}
