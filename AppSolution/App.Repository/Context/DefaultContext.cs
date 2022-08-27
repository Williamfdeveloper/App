

using App.Domain.Entities;
using App.Repository.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Context
{
    public class DefaultContext : IdentityDbContext<Usuario, IdentityRole, string>
    {
        public DefaultContext()
        {
        }

        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {


        }
        
        public virtual DbSet<Cartao> Cartao { get; set; }
        public virtual DbSet<EmailSistema> EmailSistema { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<FormaPagamento> FormaPagamento { get; set; }
        public virtual DbSet<ParametrosSistema> ParametrosSistema { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoHistorico> PedidoHistorico { get; set; }
        public virtual DbSet<PedidoItem> PedidoItem { get; set; }
        public virtual DbSet<PedidoPagamento> PedidoPagamento { get; set; }
        public virtual DbSet<PedidoPagamentoHistorico> PedidoPagamentoHistorico { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("DataSource=app.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new CartaoConfiguration());
            modelBuilder.ApplyConfiguration(new EmailSistemaConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration(new FormaPagamentoConfiguration());
            modelBuilder.ApplyConfiguration(new ParametrosSistemaConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoHistoricoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoPagamentoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoPagamentoHistoricoConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
