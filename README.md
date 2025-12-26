# Sql Server Bulk Insert 

Este projeto é uma exemplo simples de inserção massiva de dados em bancos de dados SQL Server, utilizando a classe `SqlBulkCopy`.

## Tecnologias e Dependências

* **C# (.NET 10)**
* **SQL Server LocalDB**: Instância de banco de dados leve para desenvolvimento.
* **CsvHelper**: Biblioteca para leitura e escrita de arquivos CSV.
* **Microsoft.Data.SqlClient**: Provedor oficial para conexão com SQL Server.

---

## O que é o SqlBulkCopy?

A classe `SqlBulkCopy`  oferece uma maneira de alto desempenho para transferir grandes volumes de dados de uma fonte (como um DataTable) para uma tabela do SQL Server de forma eficiente. Em vez de enviar uma instrução INSERT para cada linha, o SqlBulkCopy abre uma conexão direta com o servidor e transmite os dados em lote. Essa abordagem reduz significativamente a sobrecarga associada a múltiplas requisições e é ideal para cenários em que milhares ou até milhões de linhas precisam ser inseridas rapidamente.


---

## Como Executar

### 1. Requisitos de Banco de Dados
Certifique-se de ter o LocalDB instalado e a tabela criada através do script abaixo:

```sql
CREATE DATABASE ExemploBulkDB;
GO
USE ExemploBulkDB;
GO
CREATE TABLE Produtos (
    Id INT PRIMARY KEY,
    Nome NVARCHAR(MAX),
    Preco DECIMAL(18, 2),
    DataCriacao DATETIME
);
```

### 2. Restauração de Dependências
```bash
dotnet restore
```

### 3. Execução do Projeto
```bash
dotnet run
```


---

## Funcionalidades
- **BatchSize (Lotes)**: O sistema divide a carga (configurado para lotes de 10.000). Isso evita que uma única transação gigante bloqueie o banco de dados, otimizando o consumo de memória.

- **KeepIdentity**: Configurado para respeitar os IDs gerados no código ou CSV, garantindo que o mapeamento de chaves primárias seja preservado.

- **Mapeamento Dinâmico**: Utiliza Reflection para garantir que as propriedades da classe C# sejam vinculadas corretamente às colunas da tabela SQL.


---

## Fontes

* **Classe SqlBulkCopy**: [Documentação técnica sobre operações de cópia em massa no provedor Microsoft.Data.SqlClient](https://learn.microsoft.com/pt-br/dotnet/api/microsoft.data.sqlclient.sqlbulkcopy)

* **Bulk Operations using ADO.NET Core SqlBulkCopy Class**: [Bulk Operations using ADO.NET Core SqlBulkCopy Class](https://dotnettutorials.net/lesson/bulk-operations-using-ado-net-core-sqlbulkcopy-class/)


