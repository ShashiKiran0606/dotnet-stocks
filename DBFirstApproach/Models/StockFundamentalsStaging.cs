using System;
using System.Collections.Generic;

namespace dotnet_stocks_api.DBFirstApproach.Models;

public partial class StockFundamentalsStaging
{
    public string? TickerSymbol { get; set; }

    public int? SectorId { get; set; }

    public int? SubsectorId { get; set; }

    public long? MarketCap { get; set; }

    public double? CurrentRatio { get; set; }

    public double? PriceToBookRatio { get; set; }

    public double? Peg { get; set; }

    public double? Epsqq { get; set; }

    public double? EpsNxtyear { get; set; }

    public double? EpsTtm { get; set; }

    public double? Roe { get; set; }

    public double? InsiderOwnership { get; set; }

    public double? DebtEquityRatio { get; set; }

    public double? TrailingPe { get; set; }

    public double? ForwardPe { get; set; }
}
