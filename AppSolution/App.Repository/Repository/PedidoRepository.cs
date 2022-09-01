using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        public readonly IServiceScopeFactory _serviceScopeFactory;
        DefaultContext _context;
        public PedidoRepository(DefaultContext context, IServiceScopeFactory serviceScopeFactory)
        {
            //_context = context;
            _serviceScopeFactory = serviceScopeFactory;
            _context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DefaultContext>();
        }

        public bool Atualizar(Pedido Pedido)
        {
            //_context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DefaultContext>();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Entry(Pedido).State = EntityState.Modified;
                    _context.Entry(Pedido).Reference(x => x.FormaPagamento).IsModified = false;

                    if (_context.SaveChanges() > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Pedido Buscar(int PedidoID)
        {

            return _context.Pedido.Where(c => c.CodigoPedido.Equals(PedidoID))
                .Include(c => c.PedidoItem)
                .Include(c => c.PedidoHistorico)
                .Include(c => c.PedidoPagamento)
                .Include(c => c.FormaPagamento).FirstOrDefault();
        }

        public IList<Pedido> BuscarLista()
        {
            return _context.Pedido.ToList();
        }

        public IList<Pedido> BuscarListaPorUsuario(string UsuarioID)
        {
            return _context.Pedido.Where(c => c.CodigoUsuario.Equals(UsuarioID)).ToList();
        }

        public IList<Pedido> BuscarPedidosDoDia()
        {
            return _context.Pedido.Where(c => c.DataPedido.Date.Equals(DateTime.Now.Date)).ToList();
        }

        public IList<Pedido> BuscarPedidosPorTipo(int StatusPedido)
        {
            return _context.Pedido.Where(c => c.SituacaoPedido.Equals(StatusPedido)).ToList();
        }

        public bool Salvar(ref Pedido Pedido)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Pedido.Add(Pedido);
                    if (_context.SaveChanges() > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
    public interface IScoped { }

    public class Scoped : IScoped { }
}
