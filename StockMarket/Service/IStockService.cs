using StockMarket.Model;

namespace StockMarket.Service
{
    /// <summary>
    /// Service interface for managing stock transactions and queries.
    /// </summary>
    public interface IStockService
    {
        IEnumerable<StockResponseDto> GetAllStocks();
        IEnumerable<StockResponseDto> GetStocksByTicker(string ticker);
        IEnumerable<StockResponseDto> GetStocksByTickers(List<string> tickers);
        void AddTransaction(StockRequestDto transactionDto);
    }

}
