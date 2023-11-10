using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosAPI.Dados;
using PedidosAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PedidosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensPedidoController : ControllerBase
    {
        private readonly dbPedidos _context;

        public ItensPedidoController(dbPedidos context)
        {
            _context = context;
        }

        // GET: api/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tbItensPedido>>> GetItensPedido()
        {
            if (_context.ItensPedido == null)
            {
                return NotFound();
            }

            return await _context.ItensPedido.ToListAsync();
        }

        // GET: api//5
        [HttpGet("{id}")]
        public async Task<ActionResult<tbItensPedido>> GetItensPedido(int id)
        {
            if (_context.ItensPedido == null)
            {
                return NotFound();
            }
            var ItensPedido = await _context.ItensPedido.FindAsync(id);

            if (ItensPedido == null)
            {
                return NotFound();
            }

            return ItensPedido;
        }

        // PUT: api//5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItensPedido(int id, tbItensPedido ItensPedido)
        {
            if (id != ItensPedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(ItensPedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItensPedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tbItensPedido>> PostItensPedido(tbItensPedido ItensPedido)
        {
            if (_context.ItensPedido == null)
            {
                return Problem("Entity set 'ItensPedidosAPIContext.ItensPedido'  is null.");
            }
            _context.ItensPedido.Add(ItensPedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItensPedido), new { id = ItensPedido.Id }, ItensPedido);
        }

        // DELETE: api//5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItensPedido(int id)
        {
            if (_context.ItensPedido == null)
            {
                return NotFound();
            }
            var ItensPedido = await _context.ItensPedido.FindAsync(id);
            if (ItensPedido == null)
            {
                return NotFound();
            }

            _context.ItensPedido.Remove(ItensPedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItensPedidoExists(int id)
        {
            return (_context.ItensPedido?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
