using System;
using System.Collections.Generic;

namespace StockInvestmentNew.Models;

public partial class StudentsAll
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
