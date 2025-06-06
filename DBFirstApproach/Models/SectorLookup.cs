using System;
using System.Collections.Generic;

namespace dotnet_stocks_api.DBFirstApproach.Models;

/// <summary>
/// Table that contains the data for a Sector, i.e. SectorID and SectorName
/// </summary>
public partial class SectorLookup
{
    public int SectorId { get; set; }

    public string? SectorName { get; set; }

    public virtual ICollection<SubsectorLookup> SubsectorLookups { get; set; } = new List<SubsectorLookup>();
}
