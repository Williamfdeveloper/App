using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            // Table
            builder.ToTable("TB_PEDIDO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoPedido });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoPedido).HasColumnName("CD_PEDIDO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.CodigoUsuario).HasColumnName("CD_USUARIO").IsRequired();
            builder.Property(e => e.ValorTotal).HasColumnName("MO_VALOR_TOTAL").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(e => e.ValorTotalComDesconto).HasColumnName("MO_VALOR_TOTAL_COM_DESCONTO").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(e => e.QuatidadeItensVenda).HasColumnName("QT_ITENS_VENDA").IsRequired();
            builder.Property(e => e.DataPedido).HasColumnName("DH_PEDIDO").IsRequired();
            builder.Property(e => e.DataAtualizacaoPedido).HasColumnName("DH_ATUALIZACAO_PEDIDO").IsRequired();
            builder.Property(e => e.DataCaptacaoPedido).HasColumnName("DH_CAPTACAO_PEDIDO").IsRequired();
            builder.Property(e => e.DataAprovacaoPedido).HasColumnName("DH_APROVACAO_PEDIDO").IsRequired();
            builder.Property(e => e.SituacaoPedido).HasColumnName("ID_SITUACAO_PEDIDO").IsRequired();
            builder.Property(e => e.CupomDesconto).HasColumnName("DC_CUPOM").IsRequired();
            builder.Property(e => e.CodigoFormaPagamento).HasColumnName("CD_FORMA_PAGAMENTO").IsRequired();


            builder.HasMany(x => x.PedidoHistorico).WithOne(x => x.Pedido).HasForeignKey(x => x.CodigoPedido);
            builder.HasMany(x => x.PedidoItem).WithOne(x => x.Pedido).HasForeignKey(x => x.CodigoPedido);
            builder.HasMany(x => x.PedidoPagamento).WithOne(x => x.Pedido).HasForeignKey(x => x.CodigoPedido);
            
        }
    }
}
