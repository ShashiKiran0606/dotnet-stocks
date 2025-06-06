using System;
using System.Collections.Generic;

namespace dotnet_stocks_api.DBFirstApproach.Models;

public partial class CompanyLocation
{
    public string TickerSymbol { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Zip { get; set; }

    public string? Country { get; set; }

    public virtual StocksLookup TickerSymbolNavigation { get; set; } = null!;
}
