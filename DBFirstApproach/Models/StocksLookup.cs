using System;
using System.Collections.Generic;

namespace dotnet_stocks_api.DBFirstApproach.Models;

/// <summary>
/// Table that contains the Ticker Symbol and the name of Stock 
/// </summary>
public partial class StocksLookup
{
    public string TickerSymbol { get; set; } = null!;

    public string? TickerName { get; set; }

    public virtual ICollection<StocksPriceHistory> StocksPriceHistories { get; set; } = new List<StocksPriceHistory>();
}
