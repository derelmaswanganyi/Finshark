using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/Stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();

            var stockDtos = stocks.Select(s => s.ToStockDto());

            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var createstock = stockDto.ToStockFromCreateDto();

            await _context.Stocks.AddAsync(createstock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = createstock.Id }, createstock.ToStockDto()); 
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updatestockDto)
        {
            var stockModel = await _context.Stocks.FindAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = updatestockDto.Symbol;
            stockModel.CompanyName = updatestockDto.CompanyName;
            stockModel.Purchase = updatestockDto.Purchase;
            stockModel.LastDiv = updatestockDto.LastDiv;
            stockModel.Industy = updatestockDto.Industy;
            stockModel.MarketCap = updatestockDto.MarketCap;

            await _context.SaveChangesAsync();
            

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            //return Ok(stockModel.ToStockDto()); 
            return NoContent();
        }
    }
} 