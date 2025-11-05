using System;
using System.Collections.Generic;

namespace StockInvestmentNew.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserHolding> UserHoldings { get; set; } = new List<UserHolding>();

    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
