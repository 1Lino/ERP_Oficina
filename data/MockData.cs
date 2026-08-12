public static class DadosMock
{
    public static List<Cliente> Clientes { get; } = new();
    public static List<Equipamento> Equipamentos { get; } = new();
    public static List<Produto> Produtos { get; } = new();
    public static List<Categoria> Categorias { get; } = new();
    public static List<Servico> Servicos { get; } = new();
    public static List<OrdemServico> OrdensServico { get; } = new();
    public static List<Usuario> Usuarios { get; } = new();

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
                    Status = "Ativo"
                },

                new Cliente
                {
                    Id = 2,
                    Nome = "Pedro",
                    Cidade = "Recife",
                    Telefone = "(81) 98888-2222",
                    Status = "Ativo"
                },

                new Cliente
                {
                    Id = 3,
                    Nome = "Maria",
                    Cidade = "Natal",
                    Telefone = "(84) 97777-3333",
                    Status = "Inativo"
                },

                new Cliente
                {
                    Id = 4,
                    Nome = "Carlos",
                    Cidade = "Salvador",
                    Telefone = "(71) 96666-4444",
                    Status = "Ativo"
                },

                new Cliente
                {
                    Id = 5,
                    Nome = "Ana",
                    Cidade = "Fortaleza",
                    Telefone = "(85) 95555-5555",
                    Status = "Ativo"
                },

                new Cliente
                {
                    Id = 6,
                    Nome = "Lucas",
                    Cidade = "Recife",
                    Telefone = "(81) 94444-6666",
                    Status = "Inativo"
                },

                new Cliente
                {
                    Id = 7,
                    Nome = "Mariana",
                    Cidade = "Natal",
                    Telefone = "(84) 93333-7777",
                    Status = "Ativo"
                },

                new Cliente
                {
                    Id = 8,
                    Nome = "Rafael",
                    Cidade = "Fortaleza",
                    Telefone = "(85) 92222-8888",
                    Status = "Ativo"
                },

                new Cliente
                {
                    Id = 9,
                    Nome = "Beatriz",
                    Cidade = "Salvador",
                    Telefone = "(71) 91111-9999",
                    Status = "Inativo"
                },

                new Cliente
                {
                    Id = 10,
                    Nome = "Gabriel",
                    Cidade = "Recife",
                    Telefone = "(81) 90000-1010",
                    Status = "Ativo"
                },

                new Cliente
                {
                    Id = 11,
                    Nome = "Juliana",
                    Cidade = "Natal",
                    Telefone = "(84) 98888-2020",
                    Status = "Ativo"
                },

                new Cliente
                {
                    Id = 12,
                    Nome = "Felipe",
                    Cidade = "Fortaleza",
                    Telefone = "(85) 97777-3030",
                    Status = "Inativo"
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
}