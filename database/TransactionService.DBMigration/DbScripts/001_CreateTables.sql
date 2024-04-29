IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'TransactionStore')
BEGIN
    CREATE DATABASE TransactionStore
END

USE TransactionStore
GO

IF NOT EXISTS (SELECT 1 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Transactions')
BEGIN
    CREATE TABLE dbo.Transactions (
            [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
            [Reference] UNIQUEIDENTIFIER NOT NULL,
            [UserId] BIGINT NOT NULL,
            [Amount] DECIMAL(18,2) NOT NULL,
            [Currency] NVARCHAR(5) NOT NULL,
            [Status] SMALLINT NOT NULL,
            [CreatedAt] DATETIMEOFFSET NULL,
            [UpdatedAt] DATETIMEOFFSET NULL
        );

        -- Seed data 

        INSERT INTO dbo.Transactions (Reference,UserId, Amount, Currency, Status, CreatedAt, UpdatedAt) 
        VALUES (NEWID(), 1, 1000, 'AED', 1, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());
    
        INSERT INTO dbo.Transactions (Reference,UserId, Amount, Currency, Status, CreatedAt, UpdatedAt) 
        VALUES (NEWID(), 1, 500, 'AED', 1, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());
    
        INSERT INTO dbo.Transactions (Reference,UserId, Amount, Currency, Status, CreatedAt, UpdatedAt) 
        VALUES (NEWID(), 1, -200, 'AED', 1, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());
    
        INSERT INTO dbo.Transactions (Reference,UserId, Amount, Currency, Status, CreatedAt, UpdatedAt) 
        VALUES (NEWID(), 1, -100, 'AED', 1, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());
    
        INSERT INTO dbo.Transactions (Reference,UserId, Amount, Currency, Status, CreatedAt, UpdatedAt) 
        VALUES (NEWID(), 2, 2400, 'AED', 1, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());
    
        INSERT INTO dbo.Transactions (Reference,UserId, Amount, Currency, Status, CreatedAt, UpdatedAt) 
        VALUES (NEWID(), 2, -500, 'AED', 1, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());
    
        INSERT INTO dbo.Transactions (Reference,UserId, Amount, Currency, Status, CreatedAt, UpdatedAt) 
        VALUES (NEWID(), 2, -300, 'AED', 1, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET());
END       

    