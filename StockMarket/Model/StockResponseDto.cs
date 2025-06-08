namespace StockMarket.Model
{
    /// <summary>
    /// Data Transfer Object for returning stock information in responses.
    /// </summary>
    public class StockResponseDto
    {
        public required string TickerSymbol { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public required string BrokerId { get; set; }
    }
}
