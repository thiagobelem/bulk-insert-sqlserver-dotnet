CREATE DATABASE ExemploBulkDB;
GO

USE ExemploBulkDB;
GO

CREATE TABLE Produtos (
    Id INT PRIMARY KEY,
    Nome NVARCHAR(100),
    Preco DECIMAL(18, 2),
    DataCriacao DATETIME
);
GO