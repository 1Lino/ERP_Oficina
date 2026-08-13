public class Produto
{
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public string CategoriaNome { get; set; }
    public string Nome { get; set; }
    public string SKU { get; set; }
    public decimal EstoqueAtual { get; set; }
    public decimal Preco { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
}