using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockInvestmentNew.Models;

namespace StockInvestmentNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockPricesController : ControllerBase
    {
        private readonly StockInvestmentContext _context;

        public StockPricesController(StockInvestmentContext context)
        {
            _context = context;
        }

        // GET: api/StockPrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockPrice>>> GetStockPrices()
        {
            return await _context.StockPrices.ToListAsync();
        }

        // GET: api/StockPrices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockPrice>> GetStockPrice(int id)
        {
            var stockPrice = await _context.StockPrices.FindAsync(id);

            if (stockPrice == null)
            {
                return NotFound();
            }

            return stockPrice;
        }

        // PUT: api/StockPrices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockPrice(int id, StockPrice stockPrice)
        {
            if (id != stockPrice.PriceId)
            {
                return BadRequest();
            }

            _context.Entry(stockPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockPriceExists(id))
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

        // POST: api/StockPrices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockPrice>> PostStockPrice(StockPrice stockPrice)
        {
            _context.StockPrices.Add(stockPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockPrice", new { id = stockPrice.PriceId }, stockPrice);
        }

        // DELETE: api/StockPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockPrice(int id)
        {
            var stockPrice = await _context.StockPrices.FindAsync(id);
            if (stockPrice == null)
            {
                return NotFound();
            }

            _context.StockPrices.Remove(stockPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockPriceExists(int id)
        {
            return _context.StockPrices.Any(e => e.PriceId == id);
        }
    }
}
