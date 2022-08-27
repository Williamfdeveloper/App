using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Table
            builder.ToTable("TB_PRODUTO", "dbo");

            // Primary Key
            builder.HasKey(m => new { m.CodigoProduto });

            // Properties / Column Mapping
            builder.Property(e => e.CodigoProduto).HasColumnName("CD_PRODUTO").IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.Descricao).HasColumnName("DC_DESCRICAO").IsRequired();
            builder.Property(e => e.LinkDownload).HasColumnName("DC_LINK").IsRequired();
            builder.Property(e => e.Valor).HasColumnName("MO_VALOR").IsRequired().HasColumnType("decimal(18,2)");

            // relationsShips
            //this.HasRequired(m => m.Cidades);
            //this.HasMany(x => x.Cidades).WithRequired(x => x.Estado).HasForeignKey(x => x.Estado);



            builder.HasData(
                //new Produto
                //{
                //    CodigoProduto = "",
                //    Descricao = "",
                //    LinkDownload = "",
                //    Valor = 1
                //}

                new Produto
                {
                    CodigoProduto = 1,
                    Descricao = "Aplicativo Teste 001",
                    LinkDownload = "http://localhost",
                    Valor = 500
                },
                new Produto
                {
                    CodigoProduto = 2,
                    Descricao = "Aplicativo Teste 002",
                    LinkDownload = "http://localhost",
                    Valor = 100
                },
                new Produto
                {
                    CodigoProduto = 3,
                    Descricao = "Aplicativo Teste 003",
                    LinkDownload = "http://localhost",
                    Valor = 300
                },
                new Produto
                {
                    CodigoProduto = 4,
                    Descricao = "Aplicativo Teste 004",
                    LinkDownload = "http://localhost",
                    Valor = 800
                },
                new Produto
                {
                    CodigoProduto = 5,
                    Descricao = "Aplicativo Teste 005",
                    LinkDownload = "http://localhost",
                    Valor = 500
                },
                new Produto
                {
                    CodigoProduto = 6,
                    Descricao = "Aplicativo Teste 006",
                    LinkDownload = "http://localhost",
                    Valor = 500
                },
                new Produto
                {
                    CodigoProduto = 7,
                    Descricao = "Aplicativo Teste 007",
                    LinkDownload = "http://localhost",
                    Valor = 500
                },
                new Produto
                {
                    CodigoProduto = 8,
                    Descricao = "Aplicativo Teste 008",
                    LinkDownload = "http://localhost",
                    Valor = 500
                },
                new Produto
                {
                    CodigoProduto = 9,
                    Descricao = "Aplicativo Teste 009",
                    LinkDownload = "http://localhost",
                    Valor = 500
                },
                new Produto
                {
                    CodigoProduto = 10,
                    Descricao = "Aplicativo Teste 010",
                    LinkDownload = "http://localhost",
                    Valor = 500
                },
                new Produto
                {
                    CodigoProduto = 11,
                    Descricao = "Aplicativo Teste 011",
                    LinkDownload = "http://localhost",
                    Valor = 500
                }
            );
        }
    }
}
