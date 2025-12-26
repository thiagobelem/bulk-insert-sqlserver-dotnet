using System.ComponentModel;
using System.Data;
using BulkInsertServiceExample.Entity;
using BulkInsertServiceExample.Extensions;
using Microsoft.Data.SqlClient;

namespace BulkInsertServiceExample.Service;
public class BulkInsertService : IBulkInsertService
{
    public async Task Inserir<T>(List<T> itens, string tableName, string connectionString, int batchSize = 0) where T : BaseEntity
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                using (var copy = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
                {
                    copy.BulkCopyTimeout = 0;
                    copy.DestinationTableName = tableName;
                    copy.BatchSize = batchSize;

                    AdicionarMapeamentos<T>(copy);

                    DataTable dataTable = itens.ToDataTable<T>();
                    try
                    {
                        await copy.WriteToServerAsync(dataTable);
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
    }

    private void AdicionarMapeamentos<M>(SqlBulkCopy bcp)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(M));
        foreach (PropertyDescriptor prop in properties)
        {
            bcp.ColumnMappings.Add(prop.Name, prop.Name);
        }
    }
}
