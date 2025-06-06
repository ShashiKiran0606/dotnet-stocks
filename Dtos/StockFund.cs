using System;

namespace dotnet_stocks_api.Dtos
{

    public class StockFund{

        public int SectorId { get; set; }

        public int SubsectorId { get; set; }

        public string TickerSymbol { get; set;}

        public long MarketCap {get;set;}

        public double CurrentRatio { get; set;}

        public string SectorName { get; set;}

        public string subsector_name { get; set;}
    }
    
}