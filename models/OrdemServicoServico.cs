public class OrdemServicoServico
{
    public int Id { get; set; }

    public int OrdemServicoId { get; set; }

    public int ServicoId { get; set; }

    public string ServicoNome { get; set; }

    public decimal Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    public decimal Subtotal { get; set; }
}