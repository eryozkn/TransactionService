USE TransactionStore
GO
       
CREATE OR ALTER VIEW UserBalances
(
	UserId,
	Balance,
	Currency
)
WITH SCHEMABINDING
AS
SELECT 
    UserId,
    SUM(Amount) AS Balance,
    Currency
FROM 
    Transactions
GROUP BY 
    UserId, Currency

GO

CREATE UNIQUE CLUSTERED INDEX
	IX_UserId
ON UserBalances(UserId)