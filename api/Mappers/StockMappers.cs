using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industy = stockModel.Industy,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto createStockRequestDto)
        {
            return new Stock
            {
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,
                Purchase = createStockRequestDto.Purchase,
                LastDiv = createStockRequestDto.LastDiv,
                Industy = createStockRequestDto.Industy,
                MarketCap = createStockRequestDto.MarketCap
            };
        }

         public static Stock ToStockFromUpdateDto(this UpdateStockRequestDto updateStockRequestDto)
        {
            return new Stock
            {
                Symbol = updateStockRequestDto.Symbol,
                CompanyName = updateStockRequestDto.CompanyName,
                Purchase = updateStockRequestDto.Purchase,
                LastDiv = updateStockRequestDto.LastDiv,
                Industy = updateStockRequestDto.Industy,
                MarketCap = updateStockRequestDto.MarketCap
            };
        }
       
    }
}