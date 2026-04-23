using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required ]
        [MinLength(10, ErrorMessage = "Symbol must be at least 10 characters long.")]
        public string Symbol { get; set; } = string.Empty;
        [Required ]
        [MinLength(10, ErrorMessage = "Company Name must be at least 10 characters long.")]
        public string CompanyName { get; set; } = string.Empty;
        [Required ]
        [Range(1, 10000000, ErrorMessage = "Purchase price must be between 1 and 1,000,000.")]
        public decimal Purchase { get; set; }
        [Required ]
        [Range(0.001, 100, ErrorMessage = "Last dividend must be between 0.001 and 100.")]
        public decimal LastDiv { get; set; }
        [Required ]
        [MaxLength(10, ErrorMessage = "Industry must be at least 10 characters long.")]
        public string Industy { get; set; }  = string.Empty;
        [Range(1, 500000000 , ErrorMessage = "Purchase price must be between 1 and 500,000,000")]
        public long MarketCap { get; set; }

    }
}