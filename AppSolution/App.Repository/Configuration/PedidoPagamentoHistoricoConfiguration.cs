using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public class PedidoPagamentoHistoricoConfiguration : IEntityTypeConfiguration<PedidoPagamentoHistorico>
    {
        public void Configure(EntityTypeBuilder<PedidoPagamentoHistorico> builder)
        {
            // Table
            builder.ToTable("TB_PEDIDO_PAGAMENTO_HISTORICO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoPedidoPagamentoHistorico, m.CodigoPedidoPagamento });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoPedidoPagamentoHistorico).HasColumnName("CD_PEDIDO_PAGAMENTO_HISTORICO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.CodigoPedidoPagamento).HasColumnName("CD_PEDIDO_PAGAMENTO").IsRequired();
            builder.Property(e => e.IdSituacaoPedidoPagamento).HasColumnName("ID_SITUACAO_PAGAMENTO").IsRequired();
            builder.Property(e => e.DataAtualizacao).HasColumnName("DH_ATUALIZACAO").IsRequired();


        }
    }
}
