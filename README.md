# ERP_Oficina

ERP_Oficina é um sistema desktop em Windows Forms para gestão de oficinas mecânicas.

## Descrição

O projeto é um aplicativo WinForms em C# voltado para controle de clientes, produtos, serviços e ordens de serviço em uma oficina.

## Funcionalidades principais

- Login de usuário
- Cadastro e edição de clientes
- Cadastro e edição de usuários
- Cadastro e edição de produtos
- Controle de estoque e movimentação de produtos
- Cadastro e edição de serviços
- Gerenciamento de ordens de serviço
- Histórico de ordens de serviço
- Inclusão de materiais e serviços em uma ordem de serviço

## Estrutura do projeto

- `Program.cs` - ponto de entrada da aplicação
- `forms/Main.cs` - form principal
- `forms/Login.cs` - tela de login
- `forms/Customers/` - formulários de clientes
- `forms/Products/` - formulários de produtos e estoque
- `forms/Services/` - formulários de serviços
- `forms/ServiceOrders/` - formulários de ordens de serviço
- `forms/Users/` - formulários de usuários

## Tecnologias

- .NET 10.0
- C#
- Windows Forms

## Como executar

1. Abra o projeto `ERP_Oficina.csproj` no Visual Studio.
2. Compile a solução com o `TargetFramework` definido para `net10.0-windows`.
3. Execute o projeto ou abra o executável em `bin\Debug\net10.0-windows\ERP_Oficina.exe`.
