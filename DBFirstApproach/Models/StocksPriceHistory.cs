using System;
using System.Collections.Generic;

namespace dotnet_stocks_api.DBFirstApproach.Models;

/// <summary>
/// Table that contains the Stock Price History information for each stock for every trading day
/// </summary>
public partial class StocksPriceHistory
{
    public string TickerSymbol { get; set; } = null!;

    public DateOnly TradingDate { get; set; }

    public double? OpenPrice { get; set; }

    public double? HighPrice { get; set; }

    public double? LowPrice { get; set; }

    public long? Volume { get; set; }

    public long? Dividends { get; set; }

    public double? ClosePrice { get; set; }

    public virtual StocksLookup TickerSymbolNavigation { get; set; } = null!;
}
