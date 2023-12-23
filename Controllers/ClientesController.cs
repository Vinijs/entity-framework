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

            // var clientes = from c in _context.Clientes
            //     join e in _context.Enderecos on c.EnderecoId equals e.Id
            //     join p in _context.Pedidos on c.Id equals p.ClienteId
            //     join pp in _context.PedidosProdutos on p.Id equals pp.PedidoId
            //     join prod in _context.Produtos on pp.ProdutoId equals prod.Id
            //     where c.Nome == "Danilo" && c.Id == 3
            //     select new {
            //     Nome = c.Nome,
            //     Endereco = e.Logradouro,
            //     PedidoId = p.Id,
            //     Quantidade = pp.Quantidade,
            //     NomeProduto = prod.Nome
            //     };

            //     foreach (var cli in clientes)
            //     {
            //         Console.WriteLine(cli);
            //     }

            //  var clientes = from c in _context.Clientes
            //     join p in _context.Pedidos on c.Id equals p.ClienteId
            //     group p by c.Nome into grouping
            //     select new {
            //     Nome = grouping.Key,
            //     Total = grouping.Sum(g => g.ValorTotal)
            //     };

            //     foreach (var cli in clientes)
            //     {
            //         Console.WriteLine(cli);
            //     }

            // var clientes = from c in _context.Clientes
            //     join p in _context.Pedidos on c.Id equals p.ClienteId
            //     join pp in _context.PedidosProdutos on p.Id equals pp.PedidoId
            //     group p by new {c.Nome,pp.Quantidade} into grouping
            //     select new {
            //     Nome = grouping.Key,
            //     Quantidade = grouping.Key.Quantidade,
            //     Total = grouping.Sum(g => g.ValorTotal)
            //     };

            var clientes = from c in _context.Clientes
                           where (
                               from p in _context.Pedidos
                               where p.ClienteId == c.Id
                               select p

                           ).Count() >= 2
                           select c.Nome;

            foreach (var cli in clientes)
            {
                Console.WriteLine(cli);
            }

            var y = "";

            // var pedidosContext = _context.Clientes;
            // var pedidosSql =  pedidosContext.Join(
            //     _context.Pedidos,
            //     cli => cli.Id,
            //     ped => ped.ClienteId,

            //     (cli, ped) => new ClientePedido {
            //         Cliente = cli.Nome,
            //         ValorTotal = ped.ValorTotal
            //     }
            // ).GroupBy(p => p.Cliente).Select(c => new {
            //     Nome = c.Key,
            //     ValorTotal = c.Sum(cp => cp.ValorTotal)
            // }).ToQueryString();



            // var pedidosContexte = _context.Clientes;
            // var pedidos = await pedidosContext.Join(
            //     _context.Pedidos,
            //     cli => cli.Id,
            //     ped => ped.ClienteId,

            //     (cli, ped) => new ClientePedido {
            //         Cliente = cli.Nome,
            //         ValorTotal = ped.ValorTotal
            //     }
            // ).GroupBy(p => p.Cliente).Select(c => new {
            //     Nome = c.Key,
            //     ValorTotal = c.Sum(cp => cp.ValorTotal)
            // }).ToListAsync();



            string x = "";

            /*
            using(var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select clientes.nome as nome, sum(pedidos.valor_total) as ValorTotal from pedidos " +
                                        "inner join clientes on clientes.id = pedidos.cliente_id " +
                                        "group by clientes.id";

                _context.Database.OpenConnection();


                   using (var result = await command.ExecuteReaderAsync())
                   {
                        var pedidos_agrupados = new List<dynamic>();
                        while(result.Read())
                        {
                            pedidos_agrupados.Add(new{
                                Nome = result["nome"].ToString(),
                                ValorTotal = Convert.ToDouble(result["ValorTotal"]),
                            });
                        }
                        ViewBag.pedidos = pedidos_agrupados;
                   }

                   _context.Database.CloseConnection();
            }
            */

            /*
            using(var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT c.nome AS Cliente,p.valor_total AS ValorTotal, " +
                   "p.id AS PedidoId, p0.quantidade AS Quantidade,p0.valor AS Valor, p1.nome AS Produto " +
                   "FROM clientes AS c INNER JOIN pedidos AS p ON c.id = p.cliente_id " +
                   "INNER JOIN pedidos_produtos AS p0 ON p.id = p0.pedido_id " +
                   "INNER JOIN produtos AS p1 ON p0.produto_id = p1.id WHERE c.id = " + cliente.Id;

                _context.Database.OpenConnection();


                   using (var result = await command.ExecuteReaderAsync())
                   {
                        var pedidos = new List<ClientePedido>();
                        while(result.Read())
                        {
                            pedidos.Add(new ClientePedido{
                                Cliente = result["cliente"].ToString(),
                                ValorTotal = Convert.ToDouble(result["ValorTotal"]),
                                PedidoId = Convert.ToInt32(result["PedidoId"]),
                                Quantidade = Convert.ToInt32(result["quantidade"]),
                                Valor = Convert.ToDouble(result["valor"]),
                                Produto = result["produto"].ToString(),

                            });
                        }
                        ViewBag.pedidos = pedidos;
                   }

                   _context.Database.CloseConnection();
            }
            */

            /*
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
            */

            /*
                SELECT c.nome AS Cliente,
                p.valor_total AS ValorTotal,
                p.id AS PedidoId, p0.quantidade AS Quantidade,
                p0.valor AS Valor, p1.nome AS Produto
                FROM clientes AS c
                INNER JOIN pedidos AS p ON c.id = p.cliente_id
                INNER JOIN pedidos_produtos AS p0 ON p.id = p0.pedido_id
                INNER JOIN produtos AS p1 ON p0.produto_id = p1.id
                WHERE c.id = 3;
            */


            //  ViewBag.pedidos = pedidos;

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
