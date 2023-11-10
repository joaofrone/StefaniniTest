using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosAPI.Dados;
using PedidosAPI.Models;

namespace PedidosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly dbPedidos _context;

        public PedidosController(dbPedidos context)
        {
            _context = context;
        }

        // GET: api/
        [HttpGet]
        public async Task<ActionResult<List<objPedido>>> GetPedido()
        {
            if (_context.Pedido == null)
            {
                return NotFound();
            }

            List<tbPedido> lstPedidos = await _context.Pedido.ToListAsync();

            if (lstPedidos == null)
            {
                return NotFound();
            }

            var lstRetorno = new List<objPedido>();

            var lstItensPedido = new List<tbItensPedido>();

            tbProduto oProduto;

            objPedido oPedido;

            objItensPedido oItensPedido;

            decimal total;

            foreach (var iPedido in lstPedidos)
            {
                //Pedido
                oPedido = new objPedido();
                oPedido.Id = iPedido.Id;
                oPedido.nomeCliente = iPedido.NomeCliente;
                oPedido.emailCliente = iPedido.EmailCliente;
                oPedido.pago = iPedido.Pago;

                //Busca Itens Pedido
                lstItensPedido = await _context.ItensPedido.Where(i => i.IdPedido.Equals(iPedido.Id)).ToListAsync();

                oPedido.itensPedido = new List<objItensPedido>();

                total = 0;

                foreach (var iItensPedido in lstItensPedido)
                {
                    //Busca info produto
                    oProduto = await _context.Produto.Where(p => p.Id.Equals(iItensPedido.IdProduto)).FirstAsync();

                    oItensPedido = new objItensPedido();
                    oItensPedido.Id = iItensPedido.Id;
                    oItensPedido.idProduto = iItensPedido.IdProduto;
                    oItensPedido.nomeProduto = oProduto.NomeProduto;
                    oItensPedido.valorUnitario = oProduto.Valor;
                    oItensPedido.quantidade = iItensPedido.Quantidade;

                    oPedido.itensPedido.Add(oItensPedido);

                    total += (iItensPedido.Quantidade * oProduto.Valor);
                }

                oPedido.valorTotal = total;

                lstRetorno.Add(oPedido);
            }

            return lstRetorno;
        }

        // GET: api//5
        [HttpGet("{id}")]
        public async Task<ActionResult<objPedido>> GetPedido(int id)
        {
            if (_context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            var lstItensPedido = new List<tbItensPedido>();

            tbProduto oProduto;

            var oPedido = new objPedido();

            if (oPedido == null)
            {
                return NotFound();
            }

            objItensPedido oItensPedido;

            decimal total;

            //Pedido
            oPedido = new objPedido();
            oPedido.Id = pedido.Id;
            oPedido.nomeCliente = pedido.NomeCliente;
            oPedido.emailCliente = pedido.EmailCliente;
            oPedido.pago = pedido.Pago;

            //Busca Itens Pedido
            lstItensPedido = await _context.ItensPedido.Where(i => i.IdPedido.Equals(pedido.Id)).ToListAsync();

            oPedido.itensPedido = new List<objItensPedido>();

            total = 0;

            foreach (var iItensPedido in lstItensPedido)
            {
                //Busca info produto
                oProduto = await _context.Produto.Where(p => p.Id.Equals(iItensPedido.IdProduto)).FirstAsync();

                oItensPedido = new objItensPedido();
                oItensPedido.Id = iItensPedido.Id;
                oItensPedido.idProduto = iItensPedido.IdProduto;
                oItensPedido.nomeProduto = oProduto.NomeProduto;
                oItensPedido.valorUnitario = oProduto.Valor;
                oItensPedido.quantidade = iItensPedido.Quantidade;

                oPedido.itensPedido.Add(oItensPedido);

                total += (iItensPedido.Quantidade * oProduto.Valor);
            }

            oPedido.valorTotal = total;

            return oPedido;
        }

        // PUT: api//5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, tbPedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tbPedido>> PostPedido(tbPedido pedido)
        {
            if (_context.Pedido == null)
            {
                return Problem("Entity set 'PedidosAPIContext.Pedido'  is null.");
            }
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

        // DELETE: api//5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (_context.Pedido == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            var lstItensPedido = await _context.ItensPedido.Where(i => i.IdPedido.Equals(id)).ToListAsync();

            foreach(tbItensPedido oitemPedido in lstItensPedido)
            {
                _context.ItensPedido.Remove(oitemPedido);
            }

            _context.Pedido.Remove(pedido);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return (_context.Pedido?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
