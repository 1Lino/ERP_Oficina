using System.Text.Json;
public static class DadosMock
{
    // NOTE: todas estas listas de dados de testes, quando preenchidas, se tornam objetos que ficam na memória temporária.
    // Como se trata de testes, não há problemas. Mas caso fosse uma base de dados real com milhares ou milhões de entradas, seria
    // inviável deixar tudo isso pre-carregado na memória. É por isso que existem recursos como paginação, onde só se carrega quantidade limitada de dados por vez, de acordo com algoritmos de busca e de sort.
    public static List<Cliente> Clientes { get; } = new();
    public static List<Equipamento> Equipamentos { get; } = new();
    public static List<Produto> Produtos { get; } = new();
    public static List<Categoria> Categorias { get; } = new();
    public static List<Servico> Servicos { get; } = new();
    public static List<OrdemServico> OrdensServico { get; } = new();
    public static List<Usuario> Usuarios { get; } = new();
    public static List<MovimentacaoEstoque> MovimentacoesEstoque { get; } = new();
    public static List<OrdemServicoServico> OrdensServicoServicos { get; } = new();
    public static List<OrdemServicoMaterial> OrdensServicoMateriais { get; } = new();
    public static List<HistoricoOrdemServico> HistoricoOrdensServico { get; } = new();

    public static void CarregarDadosTestes()
    {
        CriarDadosTeste<Usuario>(Usuarios, "usuarios");
        CriarDadosTeste<Cliente>(Clientes, "clientes");
        CriarDadosTeste<Produto>(Produtos, "produtos");
        CriarDadosTeste<Equipamento>(Equipamentos, "equipamentos");
        CriarDadosTeste<Categoria>(Categorias, "categorias");
        CriarDadosTeste<Servico>(Servicos, "servicos");
        CriarDadosTeste<MovimentacaoEstoque>(MovimentacoesEstoque, "movimentacoes");

        CriarDadosTesteOrdensServico();
    }

    public static T ReadJson<T>(string caminho)
    {
        // AppContext.BaseDirectory é o diretório base do projeto, necessário para o caminho completo da leitura.
        // mas só funciona se o arquivo estiver copiado para o OutputDirectory do projeto, em ERP_Oficina.csproj.
        string caminhoCompleto = Path.Combine(AppContext.BaseDirectory, caminho);
        string json = File.ReadAllText(caminhoCompleto);

        InvalidDataException invalidData = new InvalidDataException($"Não foi possível desserializar o arquivo '{caminhoCompleto}'.");

        return JsonSerializer.Deserialize<T>(json) ?? throw invalidData;
    }

    public static void CriarDadosTeste<T>(List<T> Entidades, string arquivo)
    {
        Entidades.Clear();
        // @ para que o sistema entenda que \ não é comando.
        List<T> itens = ReadJson<List<T>>(@$"data\{arquivo}.json");
        Entidades.AddRange(itens);
    }

    // TODO: Apesar de ser mais complexa, vou ver se tem como reduzir essa aqui para formato json. 
    public static void CriarDadosTesteOrdensServico()
    {

        // Ordem de serviço 1:
        OrdemServico os1 = new OrdemServico
        {
            Id = 1,

            ClienteId = 1,
            EquipamentoId = 1,
            ResponsavelId = 1,

            ClienteNome = Clientes.FirstOrDefault(x => x.Id == 1).Nome,
            EquipamentoNome = Equipamentos.FirstOrDefault(x => x.Id == 1).Descricao,
            ResponsavelNome = "Administrador",

            DataAbertura = new DateTime(2026, 8, 5, 9, 30, 0),

            DataFechamento = null,

            Status = "Em andamento",

            Observacoes = "Equipamento apresenta falhas intermitentes ao ligar."
        };

        OrdensServico.Add(os1);

        // SERVIÇO DA OS 1
        Servico servico1 = Servicos.FirstOrDefault(x => x.Id == 1);

        if (servico1 != null)
        {
            OrdensServicoServicos.Add(
                new OrdemServicoServico
                {
                    Id = 1,

                    OrdemServicoId = 1,

                    ServicoId = servico1.Id,

                    ServicoNome = servico1.Nome,

                    Quantidade = 1,

                    PrecoUnitario = servico1.PrecoBase,

                    Subtotal = servico1.PrecoBase
                }
            );
        }

        // MATERIAL DA OS 1
        Produto produto1 = Produtos.FirstOrDefault(x => x.Id == 1);

        if (produto1 != null)
        {
            int quantidade = 2;

            OrdensServicoMateriais.Add(
                new OrdemServicoMaterial
                {
                    Id = 1,

                    OrdemServicoId = 1,

                    ProdutoId = produto1.Id,

                    ProdutoNome = produto1.Nome,

                    Quantidade = quantidade,

                    PrecoUnitario = produto1.Preco,

                    Subtotal = quantidade * produto1.Preco
                }
            );

            produto1.EstoqueAtual -= quantidade;

            MovimentacoesEstoque.Add(
                new MovimentacaoEstoque
                {
                    Id = 1,

                    ProdutoId = produto1.Id,

                    UsuarioId = 1,

                    OrdemServicoId = 1,

                    TipoMovimento = "Saída",

                    Quantidade = quantidade,

                    DataMovimento = new DateTime(2026, 8, 5, 10, 15, 0),

                    Observacao = "Material utilizado na OS #1"
                }
            );
        }

        // OS 2
        OrdemServico os2 = new OrdemServico
        {
            Id = 2,

            ClienteId = 2,
            EquipamentoId = 2,
            ResponsavelId = 2,

            ClienteNome = Clientes.FirstOrDefault(x => x.Id == 2).Nome,
            EquipamentoNome = Equipamentos.FirstOrDefault(x => x.Id == 2).Descricao,
            ResponsavelNome = "Técnico",

            DataAbertura = new DateTime(2026, 8, 7, 14, 0, 0),

            DataFechamento = null,

            Status = "Aberta",

            Observacoes = "Cliente solicita avaliação completa do equipamento."
        };

        OrdensServico.Add(os2);

        // SERVIÇO DA OS 2
        Servico servico2 = Servicos.FirstOrDefault(x => x.Id == 2);

        if (servico2 != null)
        {
            OrdensServicoServicos.Add(
                new OrdemServicoServico
                {
                    Id = 2,

                    OrdemServicoId = 2,

                    ServicoId = servico2.Id,

                    ServicoNome = servico2.Nome,

                    Quantidade = 1,

                    PrecoUnitario = servico2.PrecoBase,

                    Subtotal = servico2.PrecoBase
                }
            );
        }

        // OS 3 - CONCLUÍDA
        OrdemServico os3 = new OrdemServico
        {
            Id = 3,

            ClienteId = 3,
            EquipamentoId = 3,
            ResponsavelId = 1,

            ClienteNome = Clientes.FirstOrDefault(x => x.Id == 3).Nome,
            EquipamentoNome = Equipamentos.FirstOrDefault(x => x.Id == 3).Descricao,
            ResponsavelNome = "Administrador",

            DataAbertura = new DateTime(2026, 7, 28, 8, 30, 0),

            DataFechamento = new DateTime(2026, 7, 29, 16, 45, 0),

            Status = "Concluída",

            Observacoes = "Manutenção realizada com sucesso."
        };

        OrdensServico.Add(os3);

        // SERVIÇO DA OS 3
        Servico servico3 =
            Servicos.FirstOrDefault(x => x.Id == 3);

        if (servico3 != null)
        {
            OrdensServicoServicos.Add(
                new OrdemServicoServico
                {
                    Id = 3,

                    OrdemServicoId = 3,

                    ServicoId = servico3.Id,

                    ServicoNome = servico3.Nome,

                    Quantidade = 1,

                    PrecoUnitario = servico3.PrecoBase,

                    Subtotal = servico3.PrecoBase
                }
            );
        }

        // =========================================================
        // CALCULA TOTAIS
        // =========================================================

        AtualizarTotaisOrdens();
    }

    private static void AtualizarTotaisOrdens()
    {
        foreach (OrdemServico ordem in OrdensServico)
        {
            ordem.ValorServicos = OrdensServicoServicos.Where(x => x.OrdemServicoId == ordem.Id)
                                  .Sum(x => x.Subtotal);

            ordem.ValorMateriais = OrdensServicoMateriais.Where(x => x.OrdemServicoId == ordem.Id)
                                   .Sum(x => x.Subtotal);

            ordem.ValorTotal = ordem.ValorServicos + ordem.ValorMateriais;
        }
    }

}