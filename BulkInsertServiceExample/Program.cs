using System.Globalization;
using CsvHelper;
using BulkInsertServiceExample.Service;
using BulkInsertServiceExample.Entity;
using BulkInsertServiceExample.Extensions;
using System.Diagnostics;

namespace BulkInsertServiceExample.Program;
public class Program
{
    public static async Task Main(string[] args)
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ExemploBulkDB;Trusted_Connection=True;TrustServerCertificate=True;";
        const string pathCsv = "dados.csv";
        const int TOTAL_REGISTROS = 100_000;
        
        CreateCsv.Generate(pathCsv, TOTAL_REGISTROS);

        Console.WriteLine("Lendo CSV e preparando Bulk Insert...");

        List<Produto> produtos;
        using (var reader = new StreamReader(pathCsv))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            produtos = csv.GetRecords<Produto>().ToList();
        }

        IBulkInsertService bulkService = new BulkInsertService();

        try 
        {
            Console.WriteLine($"Iniciando inserção de {produtos.Count} registros no banco de dados...");

            Stopwatch sw = Stopwatch.StartNew();
            await bulkService.Inserir(produtos, "Produtos", connectionString, batchSize: 10000);
            sw.Stop();

            Console.WriteLine("Bulk Insert finalizado com sucesso!");
            Console.WriteLine($"Tempo total: {sw.Elapsed.TotalSeconds:F2} segundos");
            Console.WriteLine($"Velocidade média: {(TOTAL_REGISTROS / sw.Elapsed.TotalSeconds):F0} registros por segundo");
            Console.WriteLine("-------------------------------");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao inserir: {ex.Message}");
        }
    }
}
