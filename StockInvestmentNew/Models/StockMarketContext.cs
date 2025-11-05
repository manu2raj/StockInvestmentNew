using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StockInvestmentNew.Models;

public partial class StockMarketContext : DbContext
{
    public StockMarketContext()
    {
    }

    public StockMarketContext(DbContextOptions<StockMarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockPrice> StockPrices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserHolding> UserHoldings { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    public virtual DbSet<Watchlist> Watchlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TRNG6076;Initial Catalog=StockMarket;Persist Security Info=True;User ID=sa;Password=cybage@123456;Encrypt=False;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stocks__2C83A9C2DF0D2132");

            entity.HasIndex(e => e.TickerSymbol, "UQ__Stocks__F144591BA0FBC11E").IsUnique();

            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Exchange)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sector)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StockPrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__StockPri__49575BAFED84D9BA");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriceDate).HasColumnType("datetime");

            entity.HasOne(d => d.Stock).WithMany(p => p.StockPrices)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__StockPric__Stock__4CA06362");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C89C1C635");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4243D6E7B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105347948679E").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("User");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserHolding>(entity =>
        {
            entity.HasKey(e => e.HoldingId).HasName("PK__UserHold__E524B56D2E996061");

            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Stock).WithMany(p => p.UserHoldings)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__UserHoldi__Stock__44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.UserHoldings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserHoldi__UserI__440B1D61");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__UserToke__658FEEEA98189A5D");

            entity.Property(e => e.Expiry).HasColumnType("datetime");
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);
            entity.Property(e => e.Token).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserToken__UserI__3D5E1FD2");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasKey(e => e.WatchlistId).HasName("PK__Watchlis__48DE55CB6A5E917B");

            entity.ToTable("Watchlist");

            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Stock).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__Watchlist__Stock__48CFD27E");

            entity.HasOne(d => d.User).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Watchlist__UserI__47DBAE45");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
