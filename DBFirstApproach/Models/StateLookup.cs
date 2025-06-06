using System;
using System.Collections.Generic;

namespace dotnet_stocks_api.DBFirstApproach.Models;

public partial class StateLookup
{
    public string StateSymbol { get; set; } = null!;

    public string? StateName { get; set; }
}
