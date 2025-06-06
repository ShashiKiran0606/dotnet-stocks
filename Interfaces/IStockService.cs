using dotnet_stocks_api.DBFirstApproach.Models;
using dotnet_stocks_api.Dtos;
using dotnet_stocks_api.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dotnet_stocks_api.Interfaces
{
    public interface IStockService
    {
        public List<StockModel> GetAllStocks();

        public List<StockFund> GetAllSectors();

        public List<StockModel> InsertStocks(StockModel stock);

        public StockModel GetStockByTickerSymbol(string symbol);

        public StockFund GetAllSectorsByTickerSymbol(string tickerSymbol);
        public List<StockFundamental> GetFundamentalsByToken(String token, String sortOrder);
         public List<StockFundamental> GetFundamentalsByPagination(int pageNo, int pageCount);
        public List<StocksPriceHistory> GetStocksPriceHistoriesByMonths(string ticker_symbol, DateOnly startdate, DateOnly enddate);
    }
}
