public class Equipamento
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    // public string ClienteNome { get; set; }
    public string Descricao { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string NumeroSerie { get; set; }
    public DateTime DataCadastro { get; set; }
}