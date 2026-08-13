public class HistoricoOrdemServico
{
    public int Id { get; set; }

    public int OrdemServicoId { get; set; }

    public int UsuarioId { get; set; }

    public string UsuarioNome { get; set; }

    public string StatusAnterior { get; set; }

    public string StatusNovo { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string Observacao { get; set; }
}