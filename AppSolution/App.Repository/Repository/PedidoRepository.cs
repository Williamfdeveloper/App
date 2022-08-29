using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        DefaultContext _context;
        public PedidoRepository(DefaultContext context)
        {
            _context = context;
        }


        public bool Atualizar(IList<PedidoItem> PedidoItens, ref Pedido Pedido)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //if (DescontoUtilizado != null)
                    //    _context.DescontoUtilizado.Add(DescontoUtilizado);

                    _context.Entry(Pedido).State = EntityState.Modified;
                    //_context.Entry(Pedido).Reference(x => x.Usuario).IsModified = false;
                    _context.Entry(Pedido).Reference(x => x.FormaPagamento).IsModified = false;
                    _context.Entry(Pedido).Collection(x => x.PedidoItem).IsModified = false;

                    foreach (var PedidoItem in PedidoItens)
                    {
                        _context.PedidoItem.Update(PedidoItem).Property(c => c.CodigoPedidoItem).IsModified = false;
                    }

                    if (_context.SaveChanges() > 0)
                    {
                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
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
                    // TODO: Handle failure
                }
            }
        }

        public Pedido Buscar(int PedidoID)
        {
            return _context.Pedido.Where(c => c.CodigoPedido.Equals(PedidoID))
                .Include(c => c.FormaPagamento)
                //.Include(c => c.Usuario)
                .Include(c => c.PedidoItem).FirstOrDefault();
        }

        //public Pedido Buscar(int CodigoPedido)
        //{
        //    return _context.Pedido.Where(c => c.CodigoPedido.Equals(CodigoPedido))
        //        .Include(c => c.FormaPagamento)
        //        .Include(c => c.Usuario)
        //        .Include(c => c.PedidoItem).FirstOrDefault();
        //}

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

        public bool Salvar(IList<PedidoItem> PedidoItens, ref Pedido Pedido)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Pedido.Add(Pedido);
                    //if (DescontoUtilizado != null)
                    //    _context.DescontoUtilizado.Add(DescontoUtilizado);
                    //_context.Entry(Pedido).State = EntityState.Added;
                    //_context.Entry(Pedido).Reference(x => x.Usuario).IsModified = false;
                    _context.Entry(Pedido).Reference(x => x.FormaPagamento).IsModified = false;

                    foreach (var pedidoItem in PedidoItens)
                    {
                        _context.PedidoItem.Update(pedidoItem).Property(x => x.CodigoPedido).IsModified = false;
                    }

                    if (_context.SaveChanges() > 0)
                    {
                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
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
                    // TODO: Handle failure
                }
            }
        }
    }
}
