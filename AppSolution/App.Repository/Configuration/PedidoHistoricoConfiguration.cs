﻿using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public class PedidoHistoricoConfiguration : IEntityTypeConfiguration<PedidoHistorico>
    {
        public void Configure(EntityTypeBuilder<PedidoHistorico> builder)
        {
            // Table
            builder.ToTable("TB_PEDIDO_HISTORICO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoPedido });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoPedido).HasColumnName("CD_PEDIDO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.IdSituacaoPedido).HasColumnName("ID_SITUACAO").IsRequired();
            builder.Property(e => e.DataSituacao).HasColumnName("DH_SITUACAO").IsRequired();
            builder.Property(e => e.DataAtualizacaoInicio).HasColumnName("DH_SITUACAO_INICIO").IsRequired();
            builder.Property(e => e.DataAtualizacaoFim).HasColumnName("DH_SITUACAO_FIM").IsRequired();

            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);
        }
    }
}