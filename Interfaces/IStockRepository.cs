using dotnet_stocks_api.DBFirstApproach.Models;
using dotnet_stocks_api.Dtos;
using dotnet_stocks_api.Models;
using static dotnet_stocks_api.Repositories.StockRepository;

namespace dotnet_stocks_api.Interfaces
{
    public interface IStockRepository
    {
        public List<StockModel> GetAllStocks();

        public List<StockModel> GetAllStocksUsingDapper();

        public List<StockFund> GetAllSectors();
        public List<StockFundamental> GetFundamentalsByToken(String token, string sortOrder);
         public List<StockFundamental> GetFundamentalsByPagination(int pageNo, int pageCount);
         public List<StocksPriceHistory> GetStocksPriceHistoriesByMonths(string ticker_symbol, DateOnly startdate, DateOnly enddate);
    }
}
