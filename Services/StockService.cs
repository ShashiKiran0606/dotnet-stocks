using dotnet_stocks_api.Models;
using dotnet_stocks_api.Interfaces;
using dotnet_stocks_api.Dtos;
using dotnet_stocks_api.DBFirstApproach.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dotnet_stocks_api.Services
{
    public class StockService: IStockService
    {
        IStockRepository _stockRepository;
        List<StockModel> stocks = new List<StockModel>();
        public StockService(IStockRepository stockRepository) {
            _stockRepository = stockRepository;
        }

        public List<StockModel> GetAllStocks()
        {
            return _stockRepository.GetAllStocksUsingDapper();
        }

        public List<StockFund> GetAllSectors()
        {
            return _stockRepository.GetAllSectors();
        }

        public StockFund GetAllSectorsByTickerSymbol(string tickerSymbol)
        {
            return _stockRepository.GetAllSectors().Find(s=>s.TickerSymbol==tickerSymbol);
        }

        public List<StockModel> InsertStocks(StockModel stock)
        {
            stocks.Add(stock);

            return stocks;
        }

        public StockModel GetStockByTickerSymbol(string symbol)
        {
            return stocks.Find(s => s.TickerSymbol == symbol);
        }

        public List<StockFundamental> GetFundamentalsByToken(String token, String sortOrder)
        {
            return _stockRepository.GetFundamentalsByToken(token,sortOrder);
        }
        public List<StockFundamental> GetFundamentalsByPagination(int pageNo, int pageCount)
        {
            return _stockRepository.GetFundamentalsByPagination(pageNo,pageCount);
        }

        public List<StocksPriceHistory> GetStocksPriceHistoriesByMonths(string ticker_symbol, DateOnly startdate, DateOnly enddate){
            if (startdate.CompareTo (enddate)<0){
            return _stockRepository.GetStocksPriceHistoriesByMonths( ticker_symbol, startdate, enddate);
         }
        else {
                throw new ArgumentException("Start date should be earlier than end date.");
         }
        }
    }
}
