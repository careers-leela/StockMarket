using Npgsql;
using StockMarket.Model;

namespace StockMarket.Repository
{
    // Repository class for handling stock-related database operations
    public class StockRepository(IConfiguration configuration) : IStockRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("PostgresConnection");

        public List<Stock> GetAllStocks()
        {
            const string query = "SELECT * FROM stock_exchange.stocks";
            return ExecuteStockQuery(query);
        }

        public List<Stock> GetStocksByTicker(string ticker)
        {
            const string query = "SELECT * FROM stock_exchange.stocks WHERE ticker = @ticker";
            return ExecuteStockQuery(query, ("ticker", ticker));
        }

        public List<Stock> GetStocksByTickers(List<string> tickers)
        {
            if (tickers == null || tickers.Count == 0)
                return new List<Stock>();

            var paramNames = tickers.Select((_, i) => $"@ticker{i}").ToList();
            var inClause = string.Join(", ", paramNames);
            var query = $"SELECT * FROM stock_exchange.stocks WHERE ticker IN ({inClause})";

            var parameters = tickers.Select((val, i) => ($"ticker{i}", (object)val)).ToArray();
            return ExecuteStockQuery(query, parameters);
        }

        // Shared query executor
        private List<Stock> ExecuteStockQuery(string query, params (string name, object value)[] parameters)
        {
            var trades = new List<Stock>();
            using var conn = CreateConnection();
            using var cmd = new NpgsqlCommand(query, conn);

            foreach (var param in parameters)
                cmd.Parameters.AddWithValue(param.name, param.value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                trades.Add(MapReaderToTrade(reader));

            return trades;
        }

        public void AddTransaction(Stock stockTransaction)
        {
            const string query = @"
        INSERT INTO stock_exchange.stock_transactions 
        (ticker, broker_id, quantity, price)
        VALUES 
        (@ticker, @broker_id, @quantity, @price)";

            using var conn = CreateConnection();
            using var transaction = conn.BeginTransaction(); // Begin a database transaction
            try
            {
                using var cmd = new NpgsqlCommand(query, conn, transaction);

                cmd.Parameters.AddWithValue("ticker", stockTransaction.TickerSymbol);
                cmd.Parameters.AddWithValue("broker_id", stockTransaction.BrokerId);
                cmd.Parameters.AddWithValue("quantity", stockTransaction.Quantity);
                cmd.Parameters.AddWithValue("price", stockTransaction.Price);

                cmd.ExecuteNonQuery();

                transaction.Commit(); // Commit the transaction if successful
            }
            catch
            {
                transaction.Rollback(); // Rollback the transaction on error
                throw;
            }
        }



        // Shared connection creator
        private NpgsqlConnection CreateConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            return conn;
        }


        //Reader mapper
        private Stock MapReaderToTrade(NpgsqlDataReader reader) => new()
        {
            TickerSymbol = reader.GetString(0),
            Price = reader.GetDecimal(1),
        };
    }
}
