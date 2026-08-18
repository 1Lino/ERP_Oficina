public class OrdemServicoMaterial
{
    public int Id { get; set; }

    public int OrdemServicoId { get; set; }

    public int ProdutoId { get; set; }

    public string ProdutoNome { get; set; }

    public int Quantidade { get; set; } // old: decimal

    public decimal PrecoUnitario { get; set; }

    public decimal Subtotal { get; set; }
}