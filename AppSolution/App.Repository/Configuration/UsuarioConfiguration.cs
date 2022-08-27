using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Table
            builder.ToTable("TB_USUARIO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.Codigo });

            // Properties / Column Mapping
            builder.Property(e => e.Codigo).HasColumnName("CD_DADO_USUARIO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.Nome).HasColumnName("DC_NOME").IsRequired();
            builder.Property(e => e.CPF).HasColumnName("DC_CPF").IsRequired();
            builder.Property(e => e.DataNascimento).HasColumnName("DT_NASCIMENTO").IsRequired();
            builder.Property(e => e.Sexo).HasColumnName("ID_SEXO").IsRequired();


            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);
        }
    }
}
