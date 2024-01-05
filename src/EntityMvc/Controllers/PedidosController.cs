using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entity.Pedidos.Data.Contexto;
using Entity.Pedidos.Domain.Entidades;
using Entity.Pedidos.Domain.Repositories;
using Entity.Shared.Mediator;
using Entity.Shared.IntegracaoEventos;

namespace entity_framework.Controllers
{
    public class PedidosController : Controller
    {
         private readonly IPedidosRepository _pedidosRepository;
         private readonly IMediatorHandler _mediator;

        public PedidosController(IPedidosRepository pedidosRepository, IMediatorHandler mediator)
        {
            _pedidosRepository = pedidosRepository;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pedidosRepository.BuscarTodosComEndereco());
        }

        public async Task<IActionResult> Details(int id)
        {

            var pedido = await _pedidosRepository.Buscar(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["EnderecoId"] = new SelectList(await _pedidosRepository.BuscarEnderecos(), "Id", "Bairro");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,EnderecoId,ValorTotal,Data")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _pedidosRepository.Adicionar(pedido);
                await _pedidosRepository.UnitOfWork.Commit();
                await _mediator.PublicarEvento(new PedidoFinalizadoEvento(pedido.Id, pedido.Codigo, pedido.ClienteId,
                pedido.Data, pedido.Desconto, pedido.ValorTotal));
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["EnderecoId"] = new SelectList(await _pedidosRepository.BuscarEnderecos(), "Id", "Bairro", pedido.EnderecoId);
            return View(pedido);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pedido = await _pedidosRepository.Buscar(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(await _pedidosRepository.BuscarEnderecos(), "Id", "Bairro", pedido.EnderecoId);
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,EnderecoId,ValorTotal,Data")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _pedidosRepository.Atualizar(pedido);
                    await _pedidosRepository.UnitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(await _pedidosRepository.BuscarEnderecos(), "Id", "Bairro", pedido.EnderecoId);
            return View(pedido);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var pedido = await _pedidosRepository.BuscarComEndereco(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _pedidosRepository.Buscar(id);
            _pedidosRepository.Deletar(pedido);
            await _pedidosRepository.UnitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PedidoExists(int id) => await _pedidosRepository.PedidoExiste(id);
    }
}
