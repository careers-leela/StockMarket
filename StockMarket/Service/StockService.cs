using AutoMapper;
using StockMarket.Model;
using StockMarket.Repository;
using StockMarket.Service;

namespace StockMarket.Service
{
    /// <summary>
    /// Service implementation for managing stock transactions and queries.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IStockRepository _repository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<StockResponseDto> GetAllStocks()
        {
            var stocks = _repository.GetAllStocks();
            return _mapper.Map<IEnumerable<StockResponseDto>>(stocks);
        }

        public IEnumerable<StockResponseDto> GetStocksByTicker(string ticker)
        {
            var stocks = _repository.GetStocksByTicker(ticker);
            return _mapper.Map<IEnumerable<StockResponseDto>>(stocks);
        }

        public IEnumerable<StockResponseDto> GetStocksByTickers(List<string> tickers)
        {
            var stocks = _repository.GetStocksByTickers(tickers);
            return _mapper.Map<IEnumerable<StockResponseDto>>(stocks);
        }

        public void AddTransaction(StockRequestDto stockReqDto)
        {
            Stock stock = _mapper.Map<Stock>(stockReqDto);
            _repository.AddTransaction(stock);
        }
    }
}
