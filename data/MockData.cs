public static class DadosMock
{
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
        // NOTE: a ordem de carregamento importa
        CriarDadosTesteUsuarios();
        CriarDadosTesteClientes();
        CriarDadosTesteProdutos();
        CriarDadosTesteEquipamento();
        CriarDadosTesteCategorias();
        CriarDadosTesteServicos();
        CriarMovimentacoesEstoque();
        CriarDadosTesteOrdensServico();
    }
    public static void CriarDadosTesteClientes()
    {
        Clientes.Clear();
        Clientes.AddRange(
                new Cliente
                {
                    Id = 1,
                    Nome = "João",
                    Cidade = "Fortaleza",
                    Telefone = "(85) 99999-1111",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 2,
                    Nome = "Pedro",
                    Cidade = "Recife",
                    Telefone = "(81) 98888-2222",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 3,
                    Nome = "Maria",
                    Cidade = "Natal",
                    Telefone = "(84) 97777-3333",
                    Ativo = false
                },

                new Cliente
                {
                    Id = 4,
                    Nome = "Carlos",
                    Cidade = "Salvador",
                    Telefone = "(71) 96666-4444",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 5,
                    Nome = "Ana",
                    Cidade = "Fortaleza",
                    Telefone = "(85) 95555-5555",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 6,
                    Nome = "Lucas",
                    Cidade = "Recife",
                    Telefone = "(81) 94444-6666",
                    Ativo = false
                },

                new Cliente
                {
                    Id = 7,
                    Nome = "Mariana",
                    Cidade = "Natal",
                    Telefone = "(84) 93333-7777",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 8,
                    Nome = "Rafael",
                    Cidade = "Fortaleza",
                    Telefone = "(85) 92222-8888",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 9,
                    Nome = "Beatriz",
                    Cidade = "Salvador",
                    Telefone = "(71) 91111-9999",
                    Ativo = false
                },

                new Cliente
                {
                    Id = 10,
                    Nome = "Gabriel",
                    Cidade = "Recife",
                    Telefone = "(81) 90000-1010",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 11,
                    Nome = "Juliana",
                    Cidade = "Natal",
                    Telefone = "(84) 98888-2020",
                    Ativo = true
                },

                new Cliente
                {
                    Id = 12,
                    Nome = "Felipe",
                    Cidade = "Fortaleza",
                    Telefone = "(85) 97777-3030",
                    Ativo = false
                }
            );
    }

    public static void CriarDadosTesteEquipamento()
    {
        Equipamentos.Clear();
        Equipamentos.AddRange(
                new Equipamento
                {
                    Id = 1,
                    ClienteId = 1,
                    // ClienteNome = "João",
                    Descricao = "Notebook pessoal",
                    Marca = "Dell",
                    Modelo = "Inspiron 15",
                    NumeroSerie = "DELL001234",
                    DataCadastro = new DateTime(2026, 7, 10)
                },

                new Equipamento
                {
                    Id = 2,
                    ClienteId = 2,
                    // ClienteNome = "Pedro",
                    Descricao = "Impressora",
                    Marca = "HP",
                    Modelo = "LaserJet M15",
                    NumeroSerie = "HP002345",
                    DataCadastro = new DateTime(2026, 7, 12)
                },

                new Equipamento
                {
                    Id = 3,
                    ClienteId = 3,
                    // ClienteNome = "Maria",
                    Descricao = "Notebook de trabalho",
                    Marca = "Samsung",
                    Modelo = "Galaxy Book",
                    NumeroSerie = "SAM003456",
                    DataCadastro = new DateTime(2026, 7, 14)
                },

                new Equipamento
                {
                    Id = 4,
                    ClienteId = 4,
                    // ClienteNome = "Carlos",
                    Descricao = "Computador Desktop",
                    Marca = "Lenovo",
                    Modelo = "ThinkCentre",
                    NumeroSerie = "LEN004567",
                    DataCadastro = new DateTime(2026, 7, 15)
                },

                new Equipamento
                {
                    Id = 5,
                    ClienteId = 5,
                    // ClienteNome = "Ana",
                    Descricao = "Notebook",
                    Marca = "Acer",
                    Modelo = "Aspire 5",
                    NumeroSerie = "ACE005678",
                    DataCadastro = new DateTime(2026, 7, 17)
                },

                new Equipamento
                {
                    Id = 6,
                    ClienteId = 6,
                    // ClienteNome = "Lucas",
                    Descricao = "Computador Desktop",
                    Marca = "Dell",
                    Modelo = "OptiPlex 3080",
                    NumeroSerie = "DELL006789",
                    DataCadastro = new DateTime(2026, 7, 18)
                },

                new Equipamento
                {
                    Id = 7,
                    ClienteId = 7,
                    // ClienteNome = "Mariana",
                    Descricao = "Impressora Multifuncional",
                    Marca = "Epson",
                    Modelo = "EcoTank L3250",
                    NumeroSerie = "EPS007890",
                    DataCadastro = new DateTime(2026, 7, 20)
                },

                new Equipamento
                {
                    Id = 8,
                    ClienteId = 8,
                    // ClienteNome = "Rafael",
                    Descricao = "Notebook",
                    Marca = "Lenovo",
                    Modelo = "IdeaPad 3",
                    NumeroSerie = "LEN008901",
                    DataCadastro = new DateTime(2026, 7, 21)
                },

                new Equipamento
                {
                    Id = 9,
                    ClienteId = 9,
                    // ClienteNome = "Beatriz",
                    Descricao = "Impressora",
                    Marca = "Brother",
                    Modelo = "DCP-L2540DW",
                    NumeroSerie = "BRO009012",
                    DataCadastro = new DateTime(2026, 7, 23)
                },

                new Equipamento
                {
                    Id = 10,
                    ClienteId = 10,
                    // ClienteNome = "Gabriel",
                    Descricao = "Notebook",
                    Marca = "Asus",
                    Modelo = "VivoBook 15",
                    NumeroSerie = "ASU010123",
                    DataCadastro = new DateTime(2026, 7, 25)
                },

                new Equipamento
                {
                    Id = 11,
                    ClienteId = 11,
                    // ClienteNome = "Juliana",
                    Descricao = "Computador Desktop",
                    Marca = "HP",
                    Modelo = "ProDesk 400",
                    NumeroSerie = "HP011234",
                    DataCadastro = new DateTime(2026, 7, 27)
                },

                new Equipamento
                {
                    Id = 12,
                    ClienteId = 12,
                    // ClienteNome = "Felipe",
                    Descricao = "Notebook",
                    Marca = "Dell",
                    Modelo = "Vostro 3510",
                    NumeroSerie = "DELL012345",
                    DataCadastro = new DateTime(2026, 7, 28)
                },

                new Equipamento
                {
                    Id = 13,
                    ClienteId = 1,
                    // ClienteNome = "João",
                    Descricao = "Monitor",
                    Marca = "LG",
                    Modelo = "24MP400",
                    NumeroSerie = "LG013456",
                    DataCadastro = new DateTime(2026, 7, 30)
                },

                new Equipamento
                {
                    Id = 14,
                    ClienteId = 4,
                    // ClienteNome = "Carlos",
                    Descricao = "Notebook",
                    Marca = "Acer",
                    Modelo = "Nitro 5",
                    NumeroSerie = "ACE014567",
                    DataCadastro = new DateTime(2026, 8, 1)
                },

                new Equipamento
                {
                    Id = 15,
                    ClienteId = 8,
                    // ClienteNome = "Rafael",
                    Descricao = "Monitor",
                    Marca = "Samsung",
                    Modelo = "T350",
                    NumeroSerie = "SAM015678",
                    DataCadastro = new DateTime(2026, 8, 2)
                }
            );
    }

    public static void CriarDadosTesteProdutos()
    {
        Produtos.AddRange(new List<Produto>
    {
        new Produto
        {
            Id = 1,
            Nome = "Notebook Dell Inspiron",
            SKU = "NOTE-DELL-001",
            CategoriaId = 1,
            CategoriaNome = "Informática",
            EstoqueAtual = 8,
            Preco = 3499.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-30)
        },

        new Produto
        {
            Id = 2,
            Nome = "Mouse Logitech M170",
            SKU = "MOUSE-LOG-001",
            CategoriaId = 1,
            CategoriaNome = "Informática",
            EstoqueAtual = 25,
            Preco = 79.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-25)
        },

        new Produto
        {
            Id = 3,
            Nome = "Teclado Mecânico Redragon",
            SKU = "TEC-RED-001",
            CategoriaId = 1,
            CategoriaNome = "Informática",
            EstoqueAtual = 12,
            Preco = 249.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-20)
        },

        new Produto
        {
            Id = 4,
            Nome = "SSD Kingston 480GB",
            SKU = "SSD-KNG-480",
            CategoriaId = 9,
            CategoriaNome = "Armazenamento",
            EstoqueAtual = 15,
            Preco = 289.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-18)
        },

        new Produto
        {
            Id = 5,
            Nome = "HD Externo Seagate 1TB",
            SKU = "HD-SEA-1TB",
            CategoriaId = 9,
            CategoriaNome = "Armazenamento",
            EstoqueAtual = 7,
            Preco = 399.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-15)
        },

        new Produto
        {
            Id = 6,
            Nome = "Cabo HDMI 2.0",
            SKU = "CAB-HDMI-002",
            CategoriaId = 4,
            CategoriaNome = "Acessórios",
            EstoqueAtual = 40,
            Preco = 39.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-12)
        },

        new Produto
        {
            Id = 7,
            Nome = "Adaptador USB Wi-Fi",
            SKU = "USB-WIFI-001",
            CategoriaId = 8,
            CategoriaNome = "Redes",
            EstoqueAtual = 18,
            Preco = 89.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-10)
        },

        new Produto
        {
            Id = 8,
            Nome = "Fonte ATX 500W",
            SKU = "FONTE-ATX-500",
            CategoriaId = 6,
            CategoriaNome = "Componentes",
            EstoqueAtual = 6,
            Preco = 279.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-8)
        },

        new Produto
        {
            Id = 9,
            Nome = "Memória RAM 8GB DDR4",
            SKU = "RAM-DDR4-8GB",
            CategoriaId = 6,
            CategoriaNome = "Componentes",
            EstoqueAtual = 14,
            Preco = 159.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-6)
        },

        new Produto
        {
            Id = 10,
            Nome = "Cartucho HP 667 Preto",
            SKU = "CART-HP667-P",
            CategoriaId = 10,
            CategoriaNome = "Impressão",
            EstoqueAtual = 4,
            Preco = 119.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-5)
        },

        new Produto
        {
            Id = 11,
            Nome = "Cabo de Rede CAT6",
            SKU = "CAB-CAT6-010",
            CategoriaId = 8,
            CategoriaNome = "Redes",
            EstoqueAtual = 0,
            Preco = 29.90m,
            Ativo = true,
            DataCadastro = DateTime.Now.AddDays(-3)
        },

        new Produto
        {
            Id = 12,
            Nome = "Placa de Rede PCI Express",
            SKU = "NIC-PCI-001",
            CategoriaId = 8,
            CategoriaNome = "Redes",
            EstoqueAtual = 3,
            Preco = 139.90m,
            Ativo = false,
            DataCadastro = DateTime.Now.AddDays(-2)
        }
    });
    }

    public static void CriarDadosTesteCategorias()
    {
        Categorias.AddRange(new List<Categoria>
        {
            new Categoria
            {
                Id = 1,
                Nome = "Informática",
                Descricao = "Computadores, notebooks e periféricos."
            },

            new Categoria
            {
                Id = 2,
                Nome = "Eletrônicos",
                Descricao = "Equipamentos e componentes eletrônicos."
            },

            new Categoria
            {
                Id = 3,
                Nome = "Ferramentas",
                Descricao = "Ferramentas manuais e elétricas."
            },

            new Categoria
            {
                Id = 4,
                Nome = "Acessórios",
                Descricao = "Cabos, adaptadores, conectores e acessórios."
            },

            new Categoria
            {
                Id = 5,
                Nome = "Peças de Reposição",
                Descricao = "Peças utilizadas na manutenção de equipamentos."
            },

            new Categoria
            {
                Id = 6,
                Nome = "Componentes",
                Descricao = "Componentes para montagem e manutenção."
            },

            new Categoria
            {
                Id = 7,
                Nome = "Materiais de Consumo",
                Descricao = "Materiais utilizados durante os serviços."
            },

            new Categoria
            {
                Id = 8,
                Nome = "Redes",
                Descricao = "Equipamentos e acessórios para redes."
            },

            new Categoria
            {
                Id = 9,
                Nome = "Armazenamento",
                Descricao = "HDs, SSDs, pendrives e dispositivos de armazenamento."
            },

            new Categoria
            {
                Id = 10,
                Nome = "Impressão",
                Descricao = "Impressoras, cartuchos e suprimentos."
            },

            new Categoria
            {
                Id = 11,
                Nome = "Telefonia",
                Descricao = "Equipamentos e acessórios de telefonia."
            },

            new Categoria
            {
                Id = 12,
                Nome = "Outros",
                Descricao = "Produtos que não se enquadram nas demais categorias."
            }
        });
    }

    public static void CriarDadosTesteServicos()
    {
        Servicos.AddRange(new List<Servico>
    {
        new Servico
        {
            Id = 1,
            Nome = "Manutenção Preventiva",
            Descricao = "Limpeza, inspeção e revisão geral do equipamento.",
            PrecoBase = 150.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 2,
            Nome = "Formatação",
            Descricao = "Formatação do sistema operacional e instalação dos componentes básicos.",
            PrecoBase = 120.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 3,
            Nome = "Instalação de Sistema Operacional",
            Descricao = "Instalação e configuração do sistema operacional.",
            PrecoBase = 180.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 4,
            Nome = "Limpeza Interna",
            Descricao = "Limpeza interna, remoção de poeira e aplicação de pasta térmica.",
            PrecoBase = 90.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 5,
            Nome = "Troca de Pasta Térmica",
            Descricao = "Remoção da pasta térmica antiga e aplicação de nova pasta térmica.",
            PrecoBase = 70.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 6,
            Nome = "Instalação de SSD",
            Descricao = "Instalação física e configuração de unidade SSD.",
            PrecoBase = 100.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 7,
            Nome = "Instalação de Memória RAM",
            Descricao = "Instalação e configuração de novos módulos de memória.",
            PrecoBase = 60.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 8,
            Nome = "Configuração de Rede",
            Descricao = "Configuração de conexão de rede cabeada ou Wi-Fi.",
            PrecoBase = 130.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 9,
            Nome = "Configuração de Impressora",
            Descricao = "Instalação de drivers e configuração da impressora no computador.",
            PrecoBase = 80.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 10,
            Nome = "Diagnóstico de Hardware",
            Descricao = "Análise do equipamento para identificação de falhas de hardware.",
            PrecoBase = 100.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 11,
            Nome = "Diagnóstico de Software",
            Descricao = "Análise do sistema para identificação de erros e problemas de software.",
            PrecoBase = 80.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 12,
            Nome = "Recuperação de Dados",
            Descricao = "Tentativa de recuperação de arquivos de unidades de armazenamento.",
            PrecoBase = 250.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 13,
            Nome = "Remoção de Vírus",
            Descricao = "Análise e remoção de vírus, malware e softwares indesejados.",
            PrecoBase = 120.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 14,
            Nome = "Configuração de Backup",
            Descricao = "Configuração de rotina de backup dos arquivos do cliente.",
            PrecoBase = 150.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 15,
            Nome = "Manutenção em Notebook",
            Descricao = "Manutenção geral em notebook, incluindo desmontagem e inspeção.",
            PrecoBase = 200.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 16,
            Nome = "Manutenção em Computador",
            Descricao = "Manutenção geral em computador desktop.",
            PrecoBase = 180.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 17,
            Nome = "Atualização de BIOS",
            Descricao = "Atualização e configuração da BIOS/UEFI da placa-mãe.",
            PrecoBase = 120.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 18,
            Nome = "Montagem de Computador",
            Descricao = "Montagem completa do computador a partir dos componentes fornecidos.",
            PrecoBase = 300.00m,
            Ativo = true
        },

        new Servico
        {
            Id = 19,
            Nome = "Otimização do Sistema",
            Descricao = "Otimização do sistema operacional e dos programas instalados.",
            PrecoBase = 100.00m,
            Ativo = false
        },

        new Servico
        {
            Id = 20,
            Nome = "Instalação de Software",
            Descricao = "Instalação e configuração de software solicitado pelo cliente.",
            PrecoBase = 60.00m,
            Ativo = false
        }
    });
    }

    public static void CriarDadosTesteOrdensServico()
    {
        // =========================================================
        // OS 1
        // =========================================================

        OrdemServico os1 = new OrdemServico
        {
            Id = 1,

            ClienteId = 1,
            EquipamentoId = 1,
            ResponsavelId = 1,

            ClienteNome = Clientes.FirstOrDefault(x => x.Id == 1).Nome,
            EquipamentoNome = Equipamentos.FirstOrDefault(x => x.Id == 1).Descricao,
            ResponsavelNome = "Administrador",

            DataAbertura =
                new DateTime(2026, 8, 5, 9, 30, 0),

            DataFechamento = null,

            Status = "Em andamento",

            Observacoes =
                "Equipamento apresenta falhas intermitentes ao ligar."
        };

        OrdensServico.Add(os1);

        // =========================================================
        // SERVIÇO DA OS 1
        // =========================================================

        Servico servico1 =
            Servicos.FirstOrDefault(x => x.Id == 1);

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

                    PrecoUnitario =
                        servico1.PrecoBase,

                    Subtotal =
                        servico1.PrecoBase
                }
            );
        }

        // =========================================================
        // MATERIAL DA OS 1
        // =========================================================

        Produto produto1 =
            Produtos.FirstOrDefault(x => x.Id == 1);

        if (produto1 != null)
        {
            decimal quantidade = 2;

            OrdensServicoMateriais.Add(
                new OrdemServicoMaterial
                {
                    Id = 1,

                    OrdemServicoId = 1,

                    ProdutoId = produto1.Id,

                    ProdutoNome = produto1.Nome,

                    Quantidade = quantidade,

                    PrecoUnitario =
                        produto1.Preco,

                    Subtotal =
                        quantidade * produto1.Preco
                }
            );

            produto1.EstoqueAtual -= quantidade;

            MovimentacoesEstoque.Add(
                new MovimentacaoEstoque
                {
                    Id = 1,

                    ProdutoId =
                        produto1.Id,

                    UsuarioId = 1,

                    OrdemServicoId = 1,

                    TipoMovimento = "Saída",

                    Quantidade = quantidade,

                    DataMovimento =
                        new DateTime(
                            2026,
                            8,
                            5,
                            10,
                            15,
                            0
                        ),

                    Observacao =
                        "Material utilizado na OS #1"
                }
            );
        }

        // =========================================================
        // OS 2
        // =========================================================

        OrdemServico os2 = new OrdemServico
        {
            Id = 2,

            ClienteId = 2,
            EquipamentoId = 2,
            ResponsavelId = 2,

            ClienteNome = Clientes.FirstOrDefault(x => x.Id == 2).Nome,
            EquipamentoNome = Equipamentos.FirstOrDefault(x => x.Id == 2).Descricao,
            ResponsavelNome = "Técnico",

            DataAbertura =
                new DateTime(
                    2026,
                    8,
                    7,
                    14,
                    0,
                    0
                ),

            DataFechamento = null,

            Status = "Aberta",

            Observacoes =
                "Cliente solicita avaliação completa do equipamento."
        };

        OrdensServico.Add(os2);

        // =========================================================
        // SERVIÇO DA OS 2
        // =========================================================

        Servico servico2 =
            Servicos.FirstOrDefault(x => x.Id == 2);

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

                    PrecoUnitario =
                        servico2.PrecoBase,

                    Subtotal =
                        servico2.PrecoBase
                }
            );
        }

        // =========================================================
        // OS 3 - CONCLUÍDA
        // =========================================================

        OrdemServico os3 = new OrdemServico
        {
            Id = 3,

            ClienteId = 3,
            EquipamentoId = 3,
            ResponsavelId = 1,

            ClienteNome = Clientes.FirstOrDefault(x => x.Id == 3).Nome,
            EquipamentoNome = Equipamentos.FirstOrDefault(x => x.Id == 3).Descricao,
            ResponsavelNome = "Administrador",

            DataAbertura =
                new DateTime(
                    2026,
                    7,
                    28,
                    8,
                    30,
                    0
                ),

            DataFechamento =
                new DateTime(
                    2026,
                    7,
                    29,
                    16,
                    45,
                    0
                ),

            Status = "Concluída",

            Observacoes =
                "Manutenção realizada com sucesso."
        };

        OrdensServico.Add(os3);

        // =========================================================
        // SERVIÇO DA OS 3
        // =========================================================

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

                    PrecoUnitario =
                        servico3.PrecoBase,

                    Subtotal =
                        servico3.PrecoBase
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

    public static void CriarDadosTesteUsuarios()
    {
        Usuarios.AddRange(
        new List<Usuario>
        {
            new Usuario
            {
                Id = 1,
                Nome = "Administrador",
                Email = "admin@oficina.com",
                SenhaHash = "123456",
                Perfil = "Administrador",
                DataCriacao =
                    new DateTime(
                        2026,
                        1,
                        10
                    ),
                Ativo = true
            },

            new Usuario
            {
                Id = 2,
                Nome = "Carlos Silva",
                Email = "carlos@oficina.com",
                SenhaHash = "123456",
                Perfil = "Técnico",
                DataCriacao =
                    new DateTime(
                        2026,
                        2,
                        5
                    ),
                Ativo = true
            },

            new Usuario
            {
                Id = 3,
                Nome = "Mariana Souza",
                Email = "mariana@oficina.com",
                SenhaHash = "123456",
                Perfil = "Atendente",
                DataCriacao =
                    new DateTime(
                        2026,
                        3,
                        12
                    ),
                Ativo = true
            },

            new Usuario
            {
                Id = 4,
                Nome = "Rafael Oliveira",
                Email = "rafael@oficina.com",
                SenhaHash = "123456",
                Perfil = "Técnico",
                DataCriacao =
                    new DateTime(
                        2026,
                        4,
                        18
                    ),
                Ativo = true
            }
        }
    );
    }

    public static void CriarMovimentacoesEstoque()
    {
        MovimentacoesEstoque.AddRange(
        new List<MovimentacaoEstoque>
        {
            new MovimentacaoEstoque
            {
                Id = 1,
                ProdutoId = 1,
                UsuarioId = 1,
                TipoMovimento = "Entrada",
                Quantidade = 10,
                DataMovimento = DateTime.Now.AddDays(-20),
                Observacao = "Compra de mercadoria"
            },

            new MovimentacaoEstoque
            {
                Id = 2,
                ProdutoId = 2,
                UsuarioId = 1,
                TipoMovimento = "Entrada",
                Quantidade = 30,
                DataMovimento = DateTime.Now.AddDays(-18),
                Observacao = "Reposição de estoque"
            },

            new MovimentacaoEstoque
            {
                Id = 3,
                ProdutoId = 3,
                UsuarioId = 1,
                TipoMovimento = "Saída",
                Quantidade = 5,
                DataMovimento = DateTime.Now.AddDays(-15),
                Observacao = "Utilização em ordem de serviço"
            },

            new MovimentacaoEstoque
            {
                Id = 4,
                ProdutoId = 4,
                UsuarioId = 1,
                TipoMovimento = "Entrada",
                Quantidade = 20,
                DataMovimento = DateTime.Now.AddDays(-12),
                Observacao = "Compra de mercadoria"
            },

            new MovimentacaoEstoque
            {
                Id = 5,
                ProdutoId = 6,
                UsuarioId = 1,
                TipoMovimento = "Saída",
                Quantidade = 8,
                DataMovimento = DateTime.Now.AddDays(-10),
                Observacao = "Material utilizado em serviço"
            },

            new MovimentacaoEstoque
            {
                Id = 6,
                ProdutoId = 8,
                UsuarioId = 1,
                TipoMovimento = "Entrada",
                Quantidade = 10,
                DataMovimento = DateTime.Now.AddDays(-8),
                Observacao = "Reposição de estoque"
            },

            new MovimentacaoEstoque
            {
                Id = 7,
                ProdutoId = 9,
                UsuarioId = 1,
                TipoMovimento = "Saída",
                Quantidade = 4,
                DataMovimento = DateTime.Now.AddDays(-5),
                Observacao = "Utilização em ordem de serviço"
            },

            new MovimentacaoEstoque
            {
                Id = 8,
                ProdutoId = 11,
                UsuarioId = 1,
                TipoMovimento = "Saída",
                Quantidade = 10,
                DataMovimento = DateTime.Now.AddDays(-3),
                Observacao = "Material utilizado em serviço"
            }
        }
    );
    }

}