namespace StockMarket.Model
{
    /// <summary>
    /// Represents a stock entity with properties for tracking stock details.
    /// </summary>
    public class Stock
    {
        public int Id { get; set; }
        public string TickerSymbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string BrokerId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
   
}
