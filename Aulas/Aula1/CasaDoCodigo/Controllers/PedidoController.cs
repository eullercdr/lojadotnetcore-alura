using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository, 
            IPedidoRepository pedidoRepository)
        {
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
        }

        public IActionResult Carrossel()
        {
            return View(_produtoRepository.GetProdutos());
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                _pedidoRepository.AddItem(codigo);
            }

            //Pedido pedido = _pedidoRepository.GetPedido();
            Pedido pedido = _pedidoRepository.GetPedido(4);
            return View(pedido.Itens);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Resumo()
        {
            return View();
        }

    }
}