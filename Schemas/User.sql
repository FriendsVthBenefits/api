CREATE TABLE User ( 
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Number INTEGER UNIQUE NOT NULL CHECK (Number BETWEEN 1000000000 AND 9999999999),
    Mail TEXT UNIQUE NOT NULL CHECK (Mail LIKE '%_@_%._%'),
    Password TEXT NOT NULL,
    Gender NUMERIC NOT NULL CHECK (Gender IN (0, 1)),
    DOB NUMERIC NOT NULL,
    Location TEXT NOT NULL,
    Pic BLOB NOT NULL,
    Bio TEXT,
    Interests TEXT,
    LastLogin NUMERIC DEFAULT (strftime('%s', 'now')),
    CreatedAt NUMERIC DEFAULT (strftime('%s', 'now')),
    UpdatedAt NUMERIC DEFAULT (strftime('%s', 'now')),
    IsActive NUMERIC CHECK (IsActive IN (0, 1)) DEFAULT 1,
    Role NUMERIC CHECK (Role IN (0, 1)) DEFAULT 1
);