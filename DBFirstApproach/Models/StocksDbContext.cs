using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnet_stocks_api.DBFirstApproach.Models;

public partial class StocksDbContext : DbContext
{
    public StocksDbContext()
    {
    }

    public StocksDbContext(DbContextOptions<StocksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CompanyLocation> CompanyLocations { get; set; }

    public virtual DbSet<CompanyLocationsStaging> CompanyLocationsStagings { get; set; }

    public virtual DbSet<SectorLookup> SectorLookups { get; set; }

    public virtual DbSet<StateLookup> StateLookups { get; set; }

    public virtual DbSet<StockFundamental> StockFundamentals { get; set; }

    public virtual DbSet<StockFundamentalsStaging> StockFundamentalsStagings { get; set; }

    public virtual DbSet<StocksLookup> StocksLookups { get; set; }

    public virtual DbSet<StocksPriceHistory> StocksPriceHistories { get; set; }

    public virtual DbSet<StocksPriceHistoryStaging> StocksPriceHistoryStagings { get; set; }

    public virtual DbSet<SubsectorLookup> SubsectorLookups { get; set; }

    public virtual DbSet<TotalMarketStock> TotalMarketStocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=endeavourtech.ddns.net;Port=27443;User Id=evr_sql_app;Password=5LViU5pLkSjRHECec9NF4wRxxV;Database=StocksDB;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("company_locations", "endeavour");

            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .HasColumnName("ticker_symbol");
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .HasColumnName("zip");

            entity.HasOne(d => d.TickerSymbolNavigation).WithMany()
                .HasForeignKey(d => d.TickerSymbol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("company_locations_stocks_lookup_fk");
        });

        modelBuilder.Entity<CompanyLocationsStaging>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("company_locations_staging", "endeavour");

            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .HasColumnName("ticker_symbol");
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .HasColumnName("zip");
        });

        modelBuilder.Entity<SectorLookup>(entity =>
        {
            entity.HasKey(e => e.SectorId).HasName("SECTOR_LOOKUP_pkey");

            entity.ToTable("sector_lookup", "endeavour", tb => tb.HasComment("Table that contains the data for a Sector, i.e. SectorID and SectorName"));

            entity.Property(e => e.SectorId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 10L, 50L, null, null)
                .HasColumnName("sector_id");
            entity.Property(e => e.SectorName)
                .HasMaxLength(25)
                .HasColumnName("sector_name");
        });

        modelBuilder.Entity<StateLookup>(entity =>
        {
            entity.HasKey(e => e.StateSymbol).HasName("state_lookup_pk");

            entity.ToTable("state_lookup", "endeavour");

            entity.Property(e => e.StateSymbol)
                .HasMaxLength(10)
                .HasColumnName("state_symbol");
            entity.Property(e => e.StateName)
                .HasMaxLength(25)
                .HasColumnName("state_name");
        });

        modelBuilder.Entity<StockFundamental>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("stock_fundamentals", "endeavour", tb => tb.HasComment("Table that conditions critical parameters to judge the health of a stock"));

            entity.Property(e => e.CurrentRatio).HasColumnName("current_ratio");
            entity.Property(e => e.DebtEquityRatio).HasColumnName("debt_equity_ratio");
            entity.Property(e => e.EpsNxtyear).HasColumnName("eps_nxtyear");
            entity.Property(e => e.EpsTtm).HasColumnName("eps_ttm");
            entity.Property(e => e.Epsqq).HasColumnName("epsqq");
            entity.Property(e => e.ForwardPe).HasColumnName("forward_pe");
            entity.Property(e => e.InsiderOwnership).HasColumnName("insider_ownership");
            entity.Property(e => e.MarketCap).HasColumnName("market_cap");
            entity.Property(e => e.Peg).HasColumnName("peg");
            entity.Property(e => e.PriceToBookRatio).HasColumnName("price_to_book_ratio");
            entity.Property(e => e.Roe).HasColumnName("roe");
            entity.Property(e => e.SectorId).HasColumnName("sector_id");
            entity.Property(e => e.SubsectorId).HasColumnName("subsector_id");
            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .HasColumnName("ticker_symbol");
            entity.Property(e => e.TrailingPe).HasColumnName("trailing_pe");

            entity.HasOne(d => d.Sector).WithMany()
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("stock_fundamentals_fk_1");

            entity.HasOne(d => d.Subsector).WithMany()
                .HasForeignKey(d => d.SubsectorId)
                .HasConstraintName("stock_fundamentals_fk_2");

            entity.HasOne(d => d.TickerSymbolNavigation).WithMany()
                .HasForeignKey(d => d.TickerSymbol)
                .HasConstraintName("stock_fundamentals_fk");
        });

        modelBuilder.Entity<StockFundamentalsStaging>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("stock_fundamentals_staging", "endeavour");

            entity.Property(e => e.CurrentRatio).HasColumnName("current_ratio");
            entity.Property(e => e.DebtEquityRatio).HasColumnName("debt_equity_ratio");
            entity.Property(e => e.EpsNxtyear).HasColumnName("eps_nxtyear");
            entity.Property(e => e.EpsTtm).HasColumnName("eps_ttm");
            entity.Property(e => e.Epsqq).HasColumnName("epsqq");
            entity.Property(e => e.ForwardPe).HasColumnName("forward_pe");
            entity.Property(e => e.InsiderOwnership).HasColumnName("insider_ownership");
            entity.Property(e => e.MarketCap).HasColumnName("market_cap");
            entity.Property(e => e.Peg).HasColumnName("peg");
            entity.Property(e => e.PriceToBookRatio).HasColumnName("price_to_book_ratio");
            entity.Property(e => e.Roe).HasColumnName("roe");
            entity.Property(e => e.SectorId).HasColumnName("sector_id");
            entity.Property(e => e.SubsectorId).HasColumnName("subsector_id");
            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .HasColumnName("ticker_symbol");
            entity.Property(e => e.TrailingPe).HasColumnName("trailing_pe");
        });

        modelBuilder.Entity<StocksLookup>(entity =>
        {
            entity.HasKey(e => e.TickerSymbol).HasName("STOCKS_LOOKUP_pkey");

            entity.ToTable("stocks_lookup", "endeavour", tb => tb.HasComment("Table that contains the Ticker Symbol and the name of Stock "));

            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .HasColumnName("ticker_symbol");
            entity.Property(e => e.TickerName)
                .HasMaxLength(75)
                .HasColumnName("ticker_name");
        });

        modelBuilder.Entity<StocksPriceHistory>(entity =>
        {
            entity.HasKey(e => new { e.TickerSymbol, e.TradingDate }).HasName("STOCKS_PRICE_HISTORY_pkey");

            entity.ToTable("stocks_price_history", "endeavour", tb => tb.HasComment("Table that contains the Stock Price History information for each stock for every trading day"));

            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .HasColumnName("ticker_symbol");
            entity.Property(e => e.TradingDate).HasColumnName("trading_date");
            entity.Property(e => e.ClosePrice).HasColumnName("close_price");
            entity.Property(e => e.Dividends).HasColumnName("dividends");
            entity.Property(e => e.HighPrice).HasColumnName("high_price");
            entity.Property(e => e.LowPrice).HasColumnName("low_price");
            entity.Property(e => e.OpenPrice).HasColumnName("open_price");
            entity.Property(e => e.Volume).HasColumnName("volume");

            entity.HasOne(d => d.TickerSymbolNavigation).WithMany(p => p.StocksPriceHistories)
                .HasForeignKey(d => d.TickerSymbol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stocks_price_history_fk");
        });

        modelBuilder.Entity<StocksPriceHistoryStaging>(entity =>
        {
            entity.HasKey(e => new { e.TickerSymbol, e.TradingDate }).HasName("STOCKS_PRICE_HISTORY_pkey_1");

            entity.ToTable("stocks_price_history_staging", "endeavour");

            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .HasColumnName("ticker_symbol");
            entity.Property(e => e.TradingDate).HasColumnName("trading_date");
            entity.Property(e => e.ClosePrice).HasColumnName("close_price");
            entity.Property(e => e.Dividends).HasColumnName("dividends");
            entity.Property(e => e.HighPrice).HasColumnName("high_price");
            entity.Property(e => e.LowPrice).HasColumnName("low_price");
            entity.Property(e => e.OpenPrice).HasColumnName("open_price");
            entity.Property(e => e.Volume).HasColumnName("volume");
        });

        modelBuilder.Entity<SubsectorLookup>(entity =>
        {
            entity.HasKey(e => e.SubsectorId).HasName("SUBSECTOR_LOOKUP_pkey");

            entity.ToTable("subsector_lookup", "endeavour", tb => tb.HasComment("Table that contains Sub Sector Information"));

            entity.Property(e => e.SubsectorId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 100L, 1000L, null, null)
                .HasColumnName("subsector_id");
            entity.Property(e => e.SectorId).HasColumnName("sector_id");
            entity.Property(e => e.SubsectorName)
                .HasMaxLength(75)
                .HasColumnName("subsector_name");

            entity.HasOne(d => d.Sector).WithMany(p => p.SubsectorLookups)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("subsector_lookup_fk");
        });

        modelBuilder.Entity<TotalMarketStock>(entity =>
        {
            entity.HasKey(e => e.TickerSymbol).HasName("STOCKS_LOOKUP_pkey_1");

            entity.ToTable("total_market_stocks", "endeavour");

            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .HasColumnName("ticker_symbol");
            entity.Property(e => e.TickerName)
                .HasMaxLength(75)
                .HasColumnName("ticker_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
