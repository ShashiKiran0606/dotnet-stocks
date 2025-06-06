namespace dotnet_stocks_api.Models
{
    public class StockFundamentalsModel
    {
        public int SectorId { get; set; }

        public int SubsectorId { get; set; }

        public string TickerSymbol { get; set;}

        public long MarketCap {get;set;}

        public double CurrentRatio { get; set;}
        
    }
}
