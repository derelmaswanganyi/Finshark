using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio; 
        }

        public async Task<Portfolio> DeletePortfolio(AppUser user, string symbol)
        {
            var portfolioModel = await _context.Portfolios
                .FirstOrDefaultAsync(p => p.AppUserId == user.Id && p.Stock.Symbol.ToLower() == symbol.ToLower());
            if (portfolioModel == null)
            {
               return null; 
            }

            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfoliosAsync(AppUser user)
        {
           return await _context.Portfolios
            .Where(s => s.AppUserId == user.Id)
            .Select(s => new Stock
            {
                Id = s.StockId,
                Symbol = s.Stock.Symbol,
                CompanyName = s.Stock.CompanyName,
                Comments = s.Stock.Comments,
                Purchase = s.Stock.Purchase,
                LastDiv = s.Stock.LastDiv,
                Industy = s.Stock.Industy,
                MarketCap = s.Stock.MarketCap
            })
            .ToListAsync();
        }

    }
}