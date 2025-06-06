using dotnet_stocks_api.DBFirstApproach.Models;
using dotnet_stocks_api.Dtos;
using dotnet_stocks_api.Interfaces;
using dotnet_stocks_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dotnet_stocks_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        IStockService _stockService;
        ILogger<StockController> logger;

        public StockController(IStockService stockService, ILogger<StockController> logger)
        {
            _stockService = stockService;
            this.logger = logger;
        }

        [HttpGet()]
        public List<StockFund> GetAllSectors()
        {
            logger.LogInformation("GET /Stock - Fetching all sectors.");
            try
            {
                var sectors = _stockService.GetAllSectors();
                logger.LogInformation("Successfully retrieved all sectors.");
                return sectors;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while fetching all sectors.");
                throw; // Let middleware handle the exception
            }
            //return _stockService.GetAllSectors();
        }

        // [HttpGet("{tickerSymbol}")]
        // public StockFund GetAllSectorsByTickerSymbol(string tickerSymbol)
        // {
            
        //     return _stockService.GetAllSectorsByTickerSymbol(tickerSymbol);
        // }

        // [HttpGet("{token}/{sortOrder}")]
        // public List<StockFundamental> GetFundamentalsByToken(String token, String sortOrder)
        // {
        //     return _stockService.GetFundamentalsByToken(token,sortOrder);
        // }
        [HttpGet("{pageNo}/{pageCount}")]
        public List<StockFundamental> GetFundamentalsByPagination(int pageNo, int pageCount)
        {
            return _stockService.GetFundamentalsByPagination(pageNo,pageCount);
        }

        [HttpGet("{tickersymbol}/{startdate}/{enddate}")]
        public List<StocksPriceHistory> GetStocksPriceHistoriesByMonths(string tickersymbol, DateOnly startdate, DateOnly enddate){
            return _stockService.GetStocksPriceHistoriesByMonths( tickersymbol, startdate, enddate);
        }
       
        // [HttpGet()]
        // public List<StockModel> GetAllStocks()
        // {
            
        //     return _stockService.GetAllStocks();
        // }

        // [HttpGet("{tickerSymbol}")]
        // public StockModel GetByTickerSymbol([FromRoute] string tickerSymbol)
        // {

        //     return _stockService.GetStockByTickerSymbol(tickerSymbol);
        // }

        // [HttpGet("get")]
        // public StockModel GetBySymbol([FromQuery] string tickerSymbol)
        // {

        //     return _stockService.GetStockByTickerSymbol(tickerSymbol);
        // }

        // [HttpPost]
        // public List<StockModel> Post([FromBody] StockModel stock)
        // {
        //    var stockList =  _stockService.InsertStocks(stock);

        //     return stockList;
           
        // }

        // [HttpPut]
        // public IActionResult Put()
        // { 
        //     return Ok();
        // }

        // [HttpDelete]
        // public IActionResult Delete()
        // {
        //     return Ok();
        // }
    }
}
