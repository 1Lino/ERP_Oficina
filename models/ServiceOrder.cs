public class OrdemServico
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; }
    public string EquipamentoNome { get; set; }
    public string ResponsavelNome { get; set; }
    public int EquipamentoId { get; set; }
    public string Status { get; set; }
    public int ResponsavelId { get; set; }
    public DateTime DataAbertura { get; set; }
    public string Observacoes { get; set; }
    public decimal ValorMateriais { get; internal set; }
    public decimal ValorServicos { get; internal set; }
    public decimal ValorTotal { get; internal set; } // valor dos materiais + valor dos serviços
    public DateTime? DataFechamento { get; internal set; }
}