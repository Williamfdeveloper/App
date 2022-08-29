using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            // Table
            builder.ToTable("TB_PEDIDO_ITEM", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoPedidoItem });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoPedidoItem).HasColumnName("CD_PEDIDO_ITEM").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.CodigoPedido).HasColumnName("CD_PEDIDO").IsRequired();
            builder.Property(e => e.CodigoProduto).HasColumnName("CD_PRODUTOR").IsRequired();
            builder.Property(e => e.DescricaoProduto).HasColumnName("DC_PRODUTO").IsRequired();
            builder.Property(e => e.Quantidade).HasColumnName("QT_ITEM").IsRequired().HasMaxLength(2);
            builder.Property(e => e.ValorUnitario).HasColumnName("MO_VALOR_UNITARIO").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(e => e.ValorTotal).HasColumnName("MO_VALOR_TOTAL").IsRequired().HasColumnType("decimal(18,2)");

            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);
        }

    }
}
