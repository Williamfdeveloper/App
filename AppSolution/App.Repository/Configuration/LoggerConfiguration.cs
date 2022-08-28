using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    internal class LoggerConfiguration : IEntityTypeConfiguration<LogEvento>
    {
        public void Configure(EntityTypeBuilder<LogEvento> builder)
        {
            // Table
            builder.ToTable("TB_LOG_ERRO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.id });

            // Properties / Column Mapping
            builder.Property(e => e.id).HasColumnName("CD_LOG_ERRO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.codigoErro).HasColumnName("CD_ERRO").IsRequired();
            builder.Property(e => e.message).HasColumnName("DC_MESSAGE").IsRequired();
            builder.Property(e => e.exception).HasColumnName("DC_EXCEPTION").IsRequired();
            builder.Property(e => e.createdtime).HasColumnName("DH_LOG_ERRO").IsRequired();


            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);
        }
    }
}
