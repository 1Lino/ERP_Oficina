public class OrdemServicoMaterial
{
    public int Id { get; set; }

    public int OrdemServicoId { get; set; }

    public int ProdutoId { get; set; }

    public string ProdutoNome { get; set; }

    public decimal Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    public decimal Subtotal { get; set; }
}