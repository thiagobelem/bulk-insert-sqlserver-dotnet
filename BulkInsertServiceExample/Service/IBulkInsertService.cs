using BulkInsertServiceExample.Entity;

namespace BulkInsertServiceExample.Service;
public interface IBulkInsertService
{
    Task Inserir<T>(List<T> itens, string tableName, string connectionString, int batchSize = 0) where T : BaseEntity;
}