using System.Globalization;
using System.Text;
using BulkInsertServiceExample.Entity;
using CsvHelper;

namespace BulkInsertServiceExample.Extensions;
public class CreateCsv
{
    public static void Generate(string pathCsv,int quantity = 1000)
    {
        StringBuilder csvContent = new StringBuilder();

        csvContent.AppendLine("Id,Nome,Preco,DataCriacao");

        Console.WriteLine($"Criando {quantity} registros.");

        using var writer = new StreamWriter(pathCsv);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        var lista = new List<Produto>();
        for (int i = 1; i <= quantity; i++)
        {
            lista.Add(new Produto { Id = i, Nome = $"Produto {i}", Preco = i * 1.5m, DataCriacao = DateTime.Now });
        }
        csv.WriteRecords(lista);

        Console.WriteLine($"Arquivo {pathCsv} gerado com {quantity} registros.");
    }
}

