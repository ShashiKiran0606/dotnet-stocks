using System;
using System.Collections.Generic;

namespace dotnet_stocks_api.DBFirstApproach.Models;

public partial class TotalMarketStock
{
    public string TickerSymbol { get; set; } = null!;

    public string? TickerName { get; set; }
}
