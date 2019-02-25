/* Create sample data */

Use [Assignment2C2P]
GO

/* Customer table */
SET IDENTITY_INSERT [Customer] ON

INSERT INTO Customer(CustomerID, [Name], Email, MobileNo) VALUES (123456, 'Alan Smith', 'alan@mail.com','0812345678')
GO

INSERT INTO Customer(CustomerID, [Name], Email, MobileNo) VALUES (123457, 'John Wick', 'john@mail.com','0891112222')
GO

INSERT INTO Customer(CustomerID, [Name], Email, MobileNo) VALUES (123458, 'William  Mayer', 'william@mail.com','0899874321')
GO

SET IDENTITY_INSERT [Customer] OFF

/* Transaction table */
INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-01-31 18:55:32', 1234.56, 'THB', 'Success', 123457)
GO

INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-02-15 09:30:41', 100.12, 'THB', 'Success', 123458)
GO

INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-02-20 14:12:34', 0.47, 'USD', 'Failed', 123458)
GO
