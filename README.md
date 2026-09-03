# ERP Oficina

Sistema desktop em Windows Forms para gestão de oficina, com foco em atendimento técnico, controle de clientes, equipamentos, produtos, ordens de serviço e autenticação de usuários.

> Aviso importante: este projeto é de natureza demonstrativa e foi desenvolvido como protótipo de estudo/prova de conceito para uma aplicação ERP desktop de oficina. Ele não deve ser tratado como um sistema de produção pronto, com arquitetura completa, governança de dados ou segurança operacional de nível empresarial.

## Visão geral

O ERP foi desenvolvido em C# com .NET 10 para ambiente Windows, utilizando formulários dinâmicos e armazenamento em arquivos JSON localmente. Ele permite centralizar o cadastro e o acompanhamento de atividades da rotina de uma assistência técnica ou oficina mecânica.

## Funcionalidades implementadas

- Autenticação de usuários com hash de senha
- Cadastro e listagem de clientes
- Cadastro e controle de equipamentos
- Gestão de produtos e categorias
- Controle de estoque e movimentações de entrada/saída
- Cadastro de serviços da oficina
- Abertura, acompanhamento e fechamento de ordens de serviço
- Inclusão de materiais e serviços na ordem de serviço
- Visualização de relatórios e dashboard inicial
- Controle de permissões por perfil
- Estrutura de sessão do usuário logado

## Fluxo principal do sistema

A aplicação inicia com a tela de login e, após autenticação bem-sucedida, abre a interface principal com menu lateral e painel de conteúdo. O menu principal inclui:

- Dashboard
- Clientes
- Equipamentos
- Produtos
- Categorias
- Serviços
- Ordens de Serviço
- Estoque
- Relatórios
- Usuários
- Configurações

## Tecnologias

- .NET 10.0
- C#
- Windows Forms
- JSON para persistência local de dados
- BCrypt.Net-Next para hashing de senhas

## Estrutura do projeto

```text
ERP_Oficina/
├── Program.cs
├── ERP_Oficina.csproj
├── README.md
├── controls/
│   ├── DashboardControl.cs
│   ├── CustomerControls.cs
│   ├── EquipamentosControls.cs
│   ├── EstoqueControls.cs
│   ├── ProdutosControls.cs
│   ├── RelatoriosControl.cs
│   ├── ServicosControls.cs
│   ├── OrdensServicoControl.cs
│   ├── UsuarioControl.cs
│   └── CategoriasControls.cs
├── data/
│   ├── clientes.json
│   ├── categorias.json
│   ├── equipamentos.json
│   ├── movimentacoes.json
│   ├── produtos.json
│   ├── servicos.json
│   ├── usuarios.json
│   └── MockData.cs
├── forms/
│   ├── Login.cs
│   ├── Main.cs
│   ├── FrmUsuario.cs
│   ├── FrmProduct.cs
│   ├── FrmService.cs
│   ├── FrmOrdemServico.cs
│   ├── FrmDetalhesOrdemServico.cs
│   ├── FrmAdicionarMaterialOS.cs
│   ├── FrmAdicionarServicoOS.cs
│   ├── FrmStockMovement.cs
│   └── ...
├── models/
│   ├── User.cs
│   ├── Costumer.cs
│   ├── Equipment.cs
│   ├── Product.cs
│   ├── Service.cs
│   ├── Category.cs
│   ├── ServiceOrder.cs
│   ├── StockMovement.cs
│   ├── Permission.cs
│   ├── HistoricoOrdemServico.cs
│   ├── OrdemServicoMaterial.cs
│   └── OrdemServicoServico.cs
├── services/
│   ├── Autenticacao.cs
│   ├── Autorizacao.cs
│   └── Sessao.cs
└── obj/
```

## Dados iniciais e login de demonstração

Os dados de usuários são carregados a partir de `data/usuarios.json` e a autenticação compara a senha digitada com o hash armazenado.

Credenciais de exemplo incluídas no projeto:

- admin@oficina.com / #blob123
- carlos@oficina.com / #mantis234
- mariana@oficina.com / #latu456
- rafael@oficina.com / #gob678

Esses usuários representam perfis como Administrador, Técnico e Atendente.

## Como executar

### Requisitos

- Windows
- Visual Studio 2022 ou superior
- .NET SDK compatible com `net10.0-windows`

### Passos

1. Abra o arquivo `ERP_Oficina.csproj` no Visual Studio.
2. Verifique se o projeto está configurado para `net10.0-windows`.
3. Restaure as dependências do NuGet, se necessário.
4. Compile a solução.
5. Execute o projeto em modo Debug ou Release.

Também é possível localizar o executável em:

```text
bin\Debug\net10.0-windows\ERP_Oficina.exe
```

## Observações importantes

- Os dados são persistidos em arquivos JSON dentro da pasta `data/`.
- O projeto inclui cópia desses arquivos para o diretório de saída no `.csproj`.
- Este é um projeto de caráter demonstrativo, ideal para estudo, apresentação e validação de conceitos de interface e fluxo de negócio.
- A aplicação pode ser evoluída com banco de dados real, validações adicionais, regras de negócio mais robustas e melhorias de segurança e arquitetura.

## Próximos passos sugeridos

- Implementar banco de dados relacional com Entity Framework
- Adicionar testes automatizados
- Melhorar validações de formulário e mensagens de erro
- Expandir relatórios e exportação de dados
- Refinar permissões e controle de acesso por ação
