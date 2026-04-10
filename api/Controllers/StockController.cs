using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
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
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var stocks = await _context.Stocks.ToListAsync();

            //Using Repository
            var stocks = await _stockRepository.GetAllAsync();

            var stockDtos = stocks.Select(s => s.ToStockDto());

            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            //var stock = await _context.Stocks.FindAsync(id);

            var stock = await _stockRepository.GetByIdAsync(id);

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

            await _stockRepository.CreateAsync(createstock);

            return CreatedAtAction(nameof(GetById), new { id = createstock.Id }, createstock.ToStockDto()); 
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updatestockDto)
        {
            var stockModel = await _stockRepository.UpdateAsync(id, updatestockDto);

            if (stockModel == null)
            {
                return NotFound();
            }           

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]


        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _stockRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
} 