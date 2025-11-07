Stock inverstment training for .net core with react


tables 
CREATE TABLE StudentsAll(
	[Id] [INTEGER] NOT NULL PRIMARY KEY AUTOINCREMENT,
	[Username] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL
)


CREATE TABLE [Student](
	[id] [INTEGER] NOT NULL  PRIMARY KEY AUTOINCREMENT,
	[name] [varchar](100) NULL,
	[age] [INTEGER] NULL 
)
GO


-- CREATE TABLE [dbo].[StudentsAll](
	-- [Id] [int] IDENTITY(1,1) NOT NULL,
	-- [Username] [nvarchar](100) NOT NULL,
	-- [PasswordHash] [nvarchar](255) NOT NULL,
-- PRIMARY KEY CLUSTERED 
-- (
	-- [Id] ASC
-- )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
-- ) ON [PRIMARY]
-- GO






CREATE TABLE Users (
    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT UNIQUE NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    PasswordHash TEXT NOT NULL,
    Role TEXT DEFAULT 'User',
    CreatedAt DATETIME DEFAULT (DATETIME('now'))
);

-- Users table (already provided earlier)
CREATE TABLE Users (
    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT UNIQUE NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    PasswordHash TEXT NOT NULL,
    Role TEXT DEFAULT 'User',
    CreatedAt DATETIME DEFAULT (DATETIME('now'))
);

-- UserTokens table
CREATE TABLE UserTokens (
    TokenId INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    Token TEXT,
    Expiry DATETIME,
    IsRevoked INTEGER DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Stocks table
CREATE TABLE Stocks (
    StockId INTEGER PRIMARY KEY AUTOINCREMENT,
    TickerSymbol TEXT UNIQUE NOT NULL,
    CompanyName TEXT,
    Sector TEXT,
    Exchange TEXT
);

-- UserHoldings table
CREATE TABLE UserHoldings (
    HoldingId INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    StockId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    PurchasePrice REAL NOT NULL,
    PurchaseDate DATE NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (StockId) REFERENCES Stocks(StockId)
);

-- Watchlist table
CREATE TABLE Watchlist (
    WatchlistId INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    StockId INTEGER NOT NULL,
    AddedDate DATETIME DEFAULT (DATETIME('now')),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (StockId) REFERENCES Stocks(StockId)
);

-- StockPrices table
CREATE TABLE StockPrices (
    PriceId INTEGER PRIMARY KEY AUTOINCREMENT,
    StockId INTEGER NOT NULL,
    Price REAL,
    PriceDate DATETIME,
    FOREIGN KEY (StockId) REFERENCES Stocks(StockId)
);


--
truncate table stockprices
truncate table UserHoldings
truncate table UserTokens
truncate table Watchlist
truncate table Users
truncate table Stocks 


-- Insert Users
INSERT INTO Users (Username, Email, PasswordHash, Role)
VALUES 
('john_doe', 'john@example.com', 'john123', 'Investor'),
('jane_smith', 'jane@example.com', 'jane123', 'Investor'),
('admin_user', 'admin@example.com', 'admin123', 'Admin');

-- Insert Stocks
INSERT INTO Stocks (TickerSymbol, CompanyName, Sector, Exchange)
VALUES
('AAPL', 'Apple Inc.', 'Technology', 'NASDAQ'),
('GOOGL', 'Alphabet Inc.', 'Technology', 'NASDAQ'),
('TSLA', 'Tesla Inc.', 'Automotive', 'NASDAQ'),
('MSFT', 'Microsoft Corporation', 'Technology', 'NASDAQ'),
('AMZN', 'Amazon.com, Inc.', 'Consumer Services', 'NASDAQ');

-- Insert StockPrices
INSERT INTO StockPrices (StockId, Price, PriceDate)
VALUES
(1, 190.25, DATETIME('now')),
(2, 2750.50, DATETIME('now')),
(3, 710.00, DATETIME('now')),
(4, 340.75, DATETIME('now')),
(5, 135.40, DATETIME('now'));

-- Insert UserHoldings
INSERT INTO UserHoldings (UserId, StockId, Quantity, PurchasePrice, PurchaseDate)
VALUES
(1, 1, 10, 180.00, '2025-10-01'),
(1, 3, 5, 650.00, '2025-09-15'),
(2, 2, 8, 2500.00, '2025-09-20'),
(2, 5, 4, 120.00, '2025-10-05');

-- Insert Watchlist
INSERT INTO Watchlist (UserId, StockId, AddedDate)
VALUES
(1, 4, DATETIME('now')),
(1, 5, DATETIME('now')),
(2, 1, DATETIME('now')),
(2, 3, DATETIME('now'));

-- Insert UserTokens
INSERT INTO UserTokens (UserId, Token, Expiry, IsRevoked)
VALUES
(1, 'token_john_123', DATETIME('now', '+7 days'), 0),
(2, 'token_jane_456', DATETIME('now', '+7 days'), 0),
(3, 'token_admin_789', DATETIME('now', '+30 days'), 0);

