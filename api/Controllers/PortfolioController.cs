using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Portfolios")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfoliosAsync(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPortfolio(string stockSymbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(stockSymbol);
            if (stock == null)
            {
                return BadRequest("Stock not found");
            }

            var userPortfolio = await _portfolioRepository.GetUserPortfoliosAsync(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == stockSymbol.ToLower()))
            {
                return BadRequest("Stock already in portfolio");
            }


            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };


            await _portfolioRepository.CreateAsync(portfolioModel);
            
            if(portfolioModel == null)
            {
                return StatusCode(500, "Could not create portfolio entry");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string stockSymbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepository.GetUserPortfoliosAsync(appUser);

            var filteredPortfolio = userPortfolio.Where(e => e.Symbol.ToLower() == stockSymbol.ToLower()).ToList();

            if(filteredPortfolio.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolio(appUser, stockSymbol);
                
            }
            else
            {
                
                return BadRequest("Stock not in portfolio");
            }
                     
            return Ok("Stock removed from portfolio");
        }
    }
}