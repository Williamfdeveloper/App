using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public  class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            // Table
            builder.ToTable("TB_PESSOA_ENDERECO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoEndereco });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoEndereco).HasColumnName("ID_ENDERECO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.Usuario).HasColumnName("CD_USUARIO").IsRequired();
            builder.Property(e => e.Rua).HasColumnName("DC_RUA").IsRequired();
            builder.Property(e => e.Numero).HasColumnName("DC_NUMERO").IsRequired();
            builder.Property(e => e.Bairro).HasColumnName("DC_BAIRRO").IsRequired();
            builder.Property(e => e.Cidade).HasColumnName("DC_CIDADE").IsRequired();
            builder.Property(e => e.Estado).HasColumnName("DC_ESTADO").IsRequired();
            builder.Property(e => e.CEP).HasColumnName("DC_CEP").IsRequired();


            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);
        }
    }
}
