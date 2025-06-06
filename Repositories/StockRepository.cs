using dotnet_stocks_api.Interfaces;
using dotnet_stocks_api.Models;
using Npgsql;
using System.Data;
using Dapper;
using dotnet_stocks_api.Dtos;
using dotnet_stocks_api.DBFirstApproach.Models;

namespace dotnet_stocks_api.Repositories
{
    public class StockRepository: IStockRepository
    {
        StocksDbContext _stockscontext;
        public StockRepository(StocksDbContext stocksDbContext){
            _stockscontext=stocksDbContext;

        }

        

        public List<StockModel> GetAllStocks()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            List<StockModel> stocks = new List<StockModel>();
            var connectionString = "host=endeavourtech.ddns.net;Port=27443;User Id=evr_sql_app;Password=5LViU5pLkSjRHECec9NF4wRxxV;Database=StocksDB;";

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var command = new NpgsqlCommand("Select * from endeavour.stocks_lookup", connection);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);

            adapter.Fill(dataTable);

            dataSet.Tables.Add(dataTable);

            for (var i=0;  i < dataSet.Tables[0].Rows.Count; i++)
            {
                stocks.Add(new StockModel()
                {
                    TickerName = dataSet.Tables[0].Rows[i][0].ToString(),
                    TickerSymbol = dataSet.Tables[0].Rows[i][1].ToString()
                });
            }
            connection.Close();
            return stocks;

        }

        public List<StockModel> GetAllStocksUsingDapper()
        {
            List<StockModel> stocks = new List<StockModel>();
            var connectionString = "host=endeavourtech.ddns.net;Port=27443;User Id=evr_sql_app;Password=5LViU5pLkSjRHECec9NF4wRxxV;Database=StocksDB;";

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();



            stocks = connection.Query<StockModel>("Select ticker_symbol as TickerSymbol, ticker_name as TickerName from endeavour.stocks_lookup").ToList();

            

            connection.Close();
            return stocks;

        }
        public List<StockFund> GetAllSectors()
        {
            List<SectorLookupModel> sectors = new List<SectorLookupModel>();
            var connectionString = "host=endeavourtech.ddns.net;Port=27443;User Id=evr_sql_app;Password=5LViU5pLkSjRHECec9NF4wRxxV;Database=StocksDB;";

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            List<StockFundamentalsModel> stockFundamentalsModels = new List<StockFundamentalsModel>();

            List<SubsectorLookupModel>  subsectorLookupModels = new List<SubsectorLookupModel>();

            sectors = connection.Query<SectorLookupModel>("Select sector_id as SectorId, sector_name as SectorName from endeavour.sector_lookup").ToList();

            stockFundamentalsModels = connection.Query<StockFundamentalsModel>("Select sector_id as SectorId, ticker_symbol as TickerSymbol , market_cap as MarketCap , subsector_id as SubsectorId , current_ratio as CurrentRatio from endeavour.stock_fundamentals").ToList();

            subsectorLookupModels = connection.Query<SubsectorLookupModel>("Select sector_id as sector_id, subsector_id as subsector_id ,subsector_name as subsector_name from endeavour.subsector_lookup").ToList();

            Dictionary<int,string> d1 = new Dictionary<int, string>();
            
            Dictionary<int,string> d2 = new Dictionary<int, string>();

            for(int i=0; i < sectors.Count ;i ++)
            {
                d1[sectors[i].SectorId] = sectors[i].SectorName;
            }
            for(int i=0; i < subsectorLookupModels.Count ;i ++){
                d2[subsectorLookupModels[i].subsector_id] = subsectorLookupModels[i].subsector_name;
            }
            List<StockFund> stockFunds = new List<StockFund>();
            for(int i =0;i < stockFundamentalsModels.Count ; i++ ){
                StockFund s = new StockFund();
                s.SectorId = stockFundamentalsModels[i].SectorId;
                s.SubsectorId = stockFundamentalsModels[i].SubsectorId;
                s.SectorName = d1[stockFundamentalsModels[i].SectorId];
                s.subsector_name = d2[stockFundamentalsModels[i].SubsectorId];
                s.TickerSymbol = stockFundamentalsModels[i].TickerSymbol;
                s.MarketCap = stockFundamentalsModels[i].MarketCap;
                s.CurrentRatio = stockFundamentalsModels[i].CurrentRatio;
                stockFunds.Add(s);
            }
            connection.Close();
            return stockFunds;

        }

        public List<StockFundamental> GetFundamentalsByToken(String token, String sortOrder)
        {
            if(sortOrder=="ASC"){
                return  _stockscontext.StockFundamentals.Where(s=>s.TickerSymbol.Contains(token)).OrderBy(s=>s.TickerSymbol).ToList();
            }
            else{
                return  _stockscontext.StockFundamentals.Where(s=>s.TickerSymbol.Contains(token)).OrderByDescending(s=>s.TickerSymbol).ToList();
            }

        }

        public List<StockFundamental> GetFundamentalsByPagination(int pageNo, int pageCount)
        {
            
            return _stockscontext.StockFundamentals.ToList().GetRange((pageNo-1)*pageCount, pageCount);
            // for(int i=(pageNo-1)*pageCount;i<pageCount;i++)
            // {
            //     sfp[i]
            // }

        }

        public List<StocksPriceHistory> GetStocksPriceHistoriesByMonths(string ticker_symbol, DateOnly startdate, DateOnly enddate ){

            return _stockscontext.StocksPriceHistories.Where(s=>s.TickerSymbol==ticker_symbol && s.TradingDate >= startdate && s.TradingDate <= enddate).ToList();
        }
            


    }
}

