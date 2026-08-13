public class MovimentacaoEstoque
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public int UsuarioId { get; set; }
    public int? OrdemServicoId { get; set; }
    public string TipoMovimento { get; set; }
    public decimal Quantidade { get; set; }
    public DateTime DataMovimento { get; set; }
    public string Observacao { get; set; }
}