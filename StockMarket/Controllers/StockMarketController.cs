using Microsoft.AspNetCore.Mvc;
using StockMarket.Model;
using StockMarket.Service;

namespace StockMarket.Controllers
{
    /// <summary>
    /// API controller for stock exchange operations such as retrieving prices and posting transactions.
    /// </summary>
    [ApiController]
    [Route("api/stockexchange")]
    public class StockMarketController : ControllerBase
    {
        private readonly ILogger<StockMarketController> _logger;
        private readonly IStockService _stockService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockMarketController"/> class.
        /// </summary>
        /// <param name="logger">Logger for logging errors and information.</param>
        /// <param name="tradeService">Service for stock operations.</param>
        public StockMarketController(ILogger<StockMarketController> logger, IStockService tradeService)
        {
            _logger = logger;
            _stockService = tradeService;
        }

        /// <summary>
        /// Gets the average price for a specific ticker symbol.
        /// </summary>
        /// <param name="ticker">The ticker symbol to query.</param>
        /// <returns>Average price for the ticker, or 404 if not found.</returns>
        [HttpGet("{ticker}")]
        public IActionResult GetAveragePrice(string ticker)
        {
            var stocks = _stockService.GetStocksByTicker(ticker);
            if (stocks == null || !stocks.Any()) return NotFound();

            // Calculate average price for the ticker
            var avgPrice = stocks.Average(t => t.Price);
            return Ok(new { TickerSymbol = ticker.ToUpper(), AveragePrice = avgPrice });
        }

        /// <summary>
        /// Gets the average prices for all tickers.
        /// </summary>
        /// <returns>List of ticker symbols with their average prices.</returns>
        [HttpGet("all")]
        public IActionResult GetAllAveragePrices()
        {
            var allStocks = _stockService.GetAllStocks();
            var result = allStocks
                .GroupBy(t => t.TickerSymbol)
                .Select(g => new
                {
                    TickerSymbol = g.Key,
                    AveragePrice = g.Average(t => t.Price),
                });

            return Ok(result);
        }

        /// <summary>
        /// Gets the average prices for a batch of ticker symbols.
        /// </summary>
        /// <param name="tickers">List of ticker symbols to query.</param>
        /// <returns>List of ticker symbols with their average prices.</returns>
        [HttpPost("batch")]
        public IActionResult GetPricesForTickers([FromBody] List<string> tickers)
        {
            var stocks = _stockService.GetStocksByTickers(tickers);
            var result = stocks
                .GroupBy(t => t.TickerSymbol)
                .Select(g => new
                {
                    TickerSymbol = g.Key,
                    AveragePrice = g.Average(t => t.Price),
                });

            return Ok(result);
        }

        /// <summary>
        /// Records a new stock transaction.
        /// </summary>
        /// <param name="transactionDto">The transaction data transfer object.</param>
        /// <returns>Success message or error response.</returns>
        [HttpPost("transaction")]
        public IActionResult PostTransaction([FromBody] StockRequestDto transactionDto)
        {
            try
            {
                _stockService.AddTransaction(transactionDto);
                return Ok(new { message = "Transaction recorded successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add transaction.");
                return StatusCode(500, "An error occurred while recording the transaction.");
            }
        }
    }
}
