using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public PedidoRepository(ApplicationContext context,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this._contextAccessor = httpContextAccessor;
        }

        public void AddItem(string codigo)
        {
            var produto = _context.Set<Produto>()
                .Where(p => p.Codigo == codigo)
                .SingleOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var pedido = GetPedido();

            var itemPedido = _context.Set<ItemPedido>()
                .Where(i => i.Produto.Codigo == codigo
                && i.Pedido.Id == pedido.Id)
                .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                _context.Set<ItemPedido>()
                    .Add(itemPedido);
                _context.SaveChanges();
            }
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = _dbSet
                .Where(p => p.Id == pedidoId)
                .SingleOrDefault();

            if (pedido == null)
            {
                pedido = new Pedido();
                _dbSet.Add(pedido);
                _context.SaveChanges();

                //Salvando o pedido na sessão
                SetPedidoId(pedido.Id);
            }

            return pedido;
        }

        public Pedido GetPedido(int p)
        {
            return _dbSet.Find(p);
        }

        private int? GetPedidoId()
        {
           return  _contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        private void SetPedidoId(int pedidoId)
        {
            _contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }
    }
}
