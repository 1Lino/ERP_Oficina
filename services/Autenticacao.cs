namespace ERP_Oficina.Services.Autenticacao;

using BCryptHash = BCrypt.Net.BCrypt; // comando do pacote: dotnet add package BCrypt.Net-Next

// Simulação da base de usuários. A camada de userRepository é uma camada de dados, somente acessada pelo backend da aplicação
// qualquer coisa só pode ser registrada nos dados se passar pelas validações e pelo processo de autenticação.
public class UsuarioRepository
{
    private static List<Usuario> usuarios = DadosMock.Usuarios;

    public void Adicionar(Usuario usuario)
    {
        usuarios.Add(usuario);
    }

    public Usuario Buscar(string login)
    {
        return usuarios.FirstOrDefault(u => u.Email == login);
    }

    public List<Usuario> Entries()
    {
        return usuarios;
    }
}

// // Isso aqui lida com o processo de autenticação. No caso, esta camada tem acesso aos dados da base.
public class AuthService
{
    private static UsuarioRepository Repository;

    public AuthService(UsuarioRepository repository)
    {
        Repository = repository;
    }

    public bool Cadastrar(string nome, string login, string senha)
    {
        // verifica se já existe usuário com este email/login, pra impedir cadastro duplicado.
        if (Repository.Buscar(login) != null)
            return false;

        var hashPass = BCryptHash.HashPassword(senha);
        Console.WriteLine($"Senha convertida para o hash: {hashPass}");

        Repository.Adicionar(new Usuario
        {
            Id = Repository.Entries().Max(entry => entry.Id) + 1, // pega o dado com id maior e cria novo id a partir desse.
            Nome = nome,
            Email = login,
            SenhaHash = hashPass,
            Perfil = "A definir", // perfil padrão
            DataCriacao = DateTime.Today,
            Ativo = true
        });

        return true;
    }

    public AuthResponse Login(string login, string senha)
    {
        var usuario = Repository.Buscar(login); // retorna o usuário correspondente ao email passado

        return new AuthResponse(usuario != null && BCryptHash.Verify(senha, usuario.SenhaHash), usuario);
    }
}

// objeto simples pra controlar tanto o sucesso do login como os dados do usuário, já que a interface precisa dos dois dados como resposta da autenticação.
public class AuthResponse
{
    public bool sucesso { get; private set; }
    public Usuario usuario { get; private set; }
    public AuthResponse(bool userExists, Usuario user)
    {
        sucesso = userExists;
        usuario = user;
    }
}