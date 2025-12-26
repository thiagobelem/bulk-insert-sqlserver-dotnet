namespace BulkInsertServiceExample.Entity;
public class Produto : BaseEntity
{
    public int Id { get; set; }
    public string? Nome{ get; set; }
    public decimal Preco { get; set; }
    public DateTime DataCriacao { get; set; }
}