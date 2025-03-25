-------------------------------------------------------------

CREATE TABLE Customer (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(150) NOT NULL,
    Age INT NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);


-------------------------------------------------------------

CREATE TABLE ContactHistory (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    Type INT NOT NULL,
    ContactDate DATETIME DEFAULT GETDATE(),
    Notes TEXT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
);

-- Type: 
-- 1: Pré-Venda Realizada
-- 2: Interesse Futuro
-- 3: Sem Interesse
-- 4: Não Atendeu
-- 5: Telefone Errado

-------------------------------------------------------------

CREATE TABLE Product (
    Id INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
);

INSERT INTO Product (Id, Name, Price) VALUES (1, 'Plano Essencial', 19.90);
INSERT INTO Product (Id, Name, Price) VALUES (2, 'Plano Profissional', 49.90);
INSERT INTO Product (Id, Name, Price) VALUES (3, 'Plano Elite', 69.90);

-------------------------------------------------------------

CREATE TABLE Sale (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    TotalPrice DECIMAL(18, 2) NOT NULL,
    SaleDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customer(Id),
    FOREIGN KEY (ProductId) REFERENCES Product(Id)
);

-------------------------------------------------------------

