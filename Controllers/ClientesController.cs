using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using entity_framework.Models;
using entity_framework.Servicos.Database;
using entity_framework.ModelViews;

namespace entity_framework.Controllers
{
    public class ClientesController : Controller
    {
        private readonly DbContexto _context;

        public ClientesController(DbContexto context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            //var listaClientes = await _context.Clientes.Contains(cliente).Where(c => c.Nome.ToLower().Contains("d")).ToListAsync();


            var dbContexto = _context.Clientes.Include(c => c.Endereco);
            return View(await dbContexto.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            var pedidosContext = _context.Clientes.Where(c => c.Id == cliente.Id);
            var pedidos = await pedidosContext.Join(
                _context.Pedidos,
                cli => cli.Id,
                ped => ped.ClienteId,

                (cli, ped) => new ClientePedido {
                    Cliente = cli.Nome,
                    ValorTotal = ped.ValorTotal,
                    PedidoId = ped.Id
                }
            ).Join(
                _context.PedidosProdutos,
                ped => ped.PedidoId,
                pp => pp.PedidoId,
                (ped,pp) => new ClientePedido {
                    Cliente = ped.Cliente,
                    ValorTotal = ped.ValorTotal,
                    PedidoId = ped.PedidoId,
                    Quantidade = pp.Quantidade,
                    Valor = pp.Valor,
                    ProdutoId = pp.ProdutoId,
                }
            ).Join(
                _context.Produtos,
                pCliente => pCliente.ProdutoId,
                produto => produto.Id,
                (pCliente,produto) => new ClientePedido {
                    Cliente = pCliente.Cliente,
                    ValorTotal = pCliente.ValorTotal,
                    PedidoId = pCliente.PedidoId,
                    Quantidade = pCliente.Quantidade,
                    Valor = pCliente.Valor,
                    Produto = produto.Nome,
                }
            ).ToListAsync(); // ToQueryString(); mostra o SQL GERADO

            /*
                COLOCAR QUERY SQL ENTITY APÓS FORMATAÇÃO


            */


             ViewBag.pedidos = pedidos;

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Bairro");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Observacao,EnderecoId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Bairro", cliente.EnderecoId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Bairro", cliente.EnderecoId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Observacao,EnderecoId")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Bairro", cliente.EnderecoId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'DbContexto.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
