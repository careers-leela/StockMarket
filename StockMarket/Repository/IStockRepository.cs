using StockMarket.Model;

namespace StockMarket.Repository
{
    /// <summary>
    /// Interface for stock repository operations.
    /// Provides methods to retrieve and add stock transactions.
    public interface IStockRepository
    {
        List<Stock> GetAllStocks();
        List<Stock> GetStocksByTicker(string ticker);
        List<Stock> GetStocksByTickers(List<string> tickers);
        void AddTransaction(Stock stockTransaction);
    }
}
