/* Create sample data */

Use [Assignment2C2P]
GO

/* Customer table */
SET IDENTITY_INSERT [Customer] ON

INSERT INTO Customer(CustomerID, [Name], Email, MobileNo) VALUES (123456, 'User 123456', 'user123456@email.com','0812345678')
GO

INSERT INTO Customer(CustomerID, [Name], Email, MobileNo) VALUES (1234567, 'User 1234567', 'user1234567@email.com','0891112222')
GO

INSERT INTO Customer(CustomerID, [Name], Email, MobileNo) VALUES (12345678, 'User 12345678', 'user12345678@email.com','0899874321')
GO

SET IDENTITY_INSERT [Customer] OFF

/* Transaction table */
INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-01-31 18:55:32', 1234.56, 'THB', 'Success', 1234567)
GO

INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-02-15 09:30:41', 100.12, 'THB', 'Success', 12345678)
GO

INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-02-20 14:12:34', 0.47, 'USD', 'Failed', 12345678)
GO

INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-02-21 02:11:11', 222.22, 'THB', 'Success', 12345678)
GO

INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-02-22 17:44:56', 321.23, 'USD', 'Success', 12345678)
GO

INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-02-23 09:45:21', 5032.12, 'THB', 'Canceled', 12345678)
GO

INSERT INTO [Transaction] (TransactionDate, Amount, CurrencyCode, [Status], CustomerID) VALUES ('2018-02-24 22:34:52', 403.59, 'JPY', 'Canceled', 12345678)
GO

