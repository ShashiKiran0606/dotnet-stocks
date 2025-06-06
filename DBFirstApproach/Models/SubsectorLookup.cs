using System;
using System.Collections.Generic;

namespace dotnet_stocks_api.DBFirstApproach.Models;

/// <summary>
/// Table that contains Sub Sector Information
/// </summary>
public partial class SubsectorLookup
{
    public int SubsectorId { get; set; }

    public string? SubsectorName { get; set; }

    public int? SectorId { get; set; }

    public virtual SectorLookup? Sector { get; set; }
}
