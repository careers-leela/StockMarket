namespace StockMarket.Model
{
    /// <summary>
    /// Data Transfer Object for creating or updating a stock.
    /// </summary>
    public class StockRequestDto
    {
        public required string TickerSymbol { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public required string BrokerId { get; set; }
    }
}
