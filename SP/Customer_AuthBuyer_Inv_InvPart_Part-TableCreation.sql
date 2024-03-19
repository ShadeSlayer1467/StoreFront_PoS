CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY,
    CustomerName NVARCHAR(255),
    SalesmanID INT,
    CustomerInfo NVARCHAR(MAX),
    PhoneNumber NVARCHAR(20),
    Fax NVARCHAR(20),
    -- AuthorizedBuyers will be stored in a separate table to normalize the database
);

CREATE TABLE AuthorizedBuyers (
    BuyerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    BuyerName NVARCHAR(255),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
GO

CREATE TABLE Part (
    PartNumber NVARCHAR(255) PRIMARY KEY,
    PartDescription NVARCHAR(MAX),
    Price DECIMAL(10, 2),
    Quantity INT,
    StockLevel INT,
    MinimumRequired INT,
    StockLocation NVARCHAR(255),
);
GO

CREATE TABLE Invoice (
    Id INT PRIMARY KEY,
    EmployeeId INT,
    CustomerId INT,
    SalesmanId INT,
    AuthorizedBuyer NVARCHAR(255),
    SubTotal DECIMAL(10, 2),
    Tax DECIMAL(10, 2),
    Total DECIMAL(10, 2),
    Date DATETIME,
    InvoiceNumber INT,
    PurchaseOrder NVARCHAR(255)
);

-- Link table for Invoice and Part (PartsList)
CREATE TABLE InvoiceParts (
    InvoiceId INT,
    PartNumber NVARCHAR(255),
    Quantity INT,
    FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id),
    FOREIGN KEY (PartNumber) REFERENCES Part(PartNumber)
);
GO
