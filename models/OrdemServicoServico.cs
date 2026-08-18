public class OrdemServicoServico
{
    public int Id { get; set; }

    public int OrdemServicoId { get; set; }

    public int ServicoId { get; set; }

    public string ServicoNome { get; set; }

    public int Quantidade { get; set; } // old: decimal

    public decimal PrecoUnitario { get; set; }

    public decimal Subtotal { get; set; }
}