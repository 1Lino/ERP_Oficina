using BCryptHash = BCrypt.Net.BCrypt; // comando do pacote: dotnet add package BCrypt.Net-Next
namespace ERP_Oficina;

public partial class Authenticator : Form
{
    Button btn_login;
    public static Usuario UsuarioAutenticado { get; private set; }
    public static Authenticator Instance { get; private set; }
    public static UsuarioRepository userRepo = new UsuarioRepository();
    public static AuthService authService = new AuthService(userRepo);

    // NOTE: isto aqui é só um guia/lembrete das senhas dos usuários fictícios de usuarios.json, já que o json guarda somente os hashes.
    List<string> senhas = [
        "#blob123", // Marcos Oliveira | admin@oficina.com
        "#mantis234", // Carlos Silva | carlos@oficina.com
        "#latu456", // Mariana Souza | mariana@oficina.com
        "#gob678" // Rafael Oliveira | rafael@oficina.com
    ];

    public Authenticator()
    {
        Instance = this;
        InitializeForm();
        InitializeComponents();
        DadosMock.CarregarUsuarios();
        this.CenterToScreen();

        // string senhaHash = BCryptHash.HashPassword(senhas[3]);
        // Console.WriteLine(senhaHash);
    }

    // LOGIN FORM
    private void InitializeForm()
    {
        Text = "Login";
        Width = 600;
        Height = 400;
        BackColor = Color.FromArgb(29, 49, 49);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
    }

    private void InitializeComponents()
    {
        var panelWidth = 350;
        var panelHeight = 250;

        Panel panel = new Panel
        {
            Anchor = AnchorStyles.None,
            BackColor = Color.White,
            Width = panelWidth,
            Height = panelHeight,
            Left = this.ClientSize.Width / 2 - (panelWidth / 2),
            Top = this.ClientSize.Height / 2 - (panelHeight / 2)
        };

        Label lbl_email = new Label
        {
            Text = "E-Mail:",
            Left = (panel.Width - 200) / 2,
            Top = 20
        };

        Label lbl_senha = new Label
        {
            Text = "Senha:",
            Left = (panel.Width - 200) / 2,
            Top = 80
        };

        TextBox txt_email = new TextBox
        {
            Width = 200,
            Left = (panel.Width - 200) / 2,
            Top = 45
        };

        TextBox txt_senha = new TextBox
        {
            Width = 200,
            Left = (panel.Width - 200) / 2,
            Top = 105
        };

        LinkLabel link_criar_conta = new LinkLabel
        {
            Text = "criar conta",
            Left = (panel.Width - 200) / 2,
            Top = 145
        };
        link_criar_conta.Click += (_, _) => FormCadastro();

        LinkLabel link_esqueci_senha = new LinkLabel
        {
            Text = "esqueci senha",
            Left = (panel.Width - 200) / 2 + 120,
            Top = 145
        };

        btn_login = new Button
        {
            Size = new Size(50, 30),
            Text = "Login",
            Left = (panel.Width - 50) / 2,
            Top = 190,
            Cursor = Cursors.Hand
        };
        // btn_login.DialogResult = DialogResult.OK;

        btn_login.Click += (_, _) => btnLogin_Click(txt_email, txt_senha, authService);

        panel.Controls.Add(lbl_email);
        panel.Controls.Add(txt_email);

        panel.Controls.Add(lbl_senha);
        panel.Controls.Add(txt_senha);

        panel.Controls.Add(link_criar_conta);
        panel.Controls.Add(link_esqueci_senha);

        panel.Controls.Add(btn_login);

        this.Controls.Add(panel);
    }

    private void FormCadastro()
    {
        Form formDialog = new Form
        {
            Text = "Cadastrar",
            Size = new Size(350, 450),
            FormBorderStyle = FormBorderStyle.FixedSingle,
            StartPosition = FormStartPosition.CenterScreen,
            MaximizeBox = false
        };

        Label lbl_nome = new Label
        {
            Width = 200,
            Text = "Nome:",
            Left = (formDialog.Width - 200) / 2,
            Top = 20
        };

        Label lbl_email = new Label
        {
            Width = 200,
            Text = "E-Mail:",
            Left = (formDialog.Width - 200) / 2,
            Top = 80
        };

        Label lbl_email_confirm = new Label
        {
            Width = 200,
            Text = "Confirme o E-Mail",
            Left = (formDialog.Width - 200) / 2,
            Top = 140
        };

        Label lbl_senha = new Label
        {
            Width = 200,
            Text = "Senha",
            Left = (formDialog.Width - 200) / 2,
            Top = 200
        };

        Label lbl_senha_confirm = new Label
        {
            Width = 200,
            Text = "Confirme a senha",
            Left = (formDialog.Width - 200) / 2,
            Top = 260
        };

        TextBox txt_nome = new TextBox
        {
            Width = 200,
            Left = (formDialog.Width - 200) / 2,
            Top = 45
        };

        TextBox txt_email = new TextBox
        {
            Width = 200,
            Left = (formDialog.Width - 200) / 2,
            Top = 105
        };

        TextBox txt_email_confirm = new TextBox
        {
            Width = 200,
            Left = (formDialog.Width - 200) / 2,
            Top = 165
        };

        TextBox txt_senha = new TextBox
        {
            Width = 200,
            Left = (formDialog.Width - 200) / 2,
            Top = 225
        };

        TextBox txt_senha_confirm = new TextBox
        {
            Width = 200,
            Left = (formDialog.Width - 200) / 2,
            Top = 285
        };

        Button btn_cadastrar = new Button
        {
            Size = new Size(100, 30),
            Text = "Cadastrar",
            Left = (formDialog.Width - 100) / 2,
            Top = 345,
            Cursor = Cursors.Hand
        };
        btn_cadastrar.DialogResult = DialogResult.None;

        btn_cadastrar.Click += (_, _) =>
        {
            btnCadastrar_Click(txt_nome, txt_email, txt_senha, authService);
            formDialog.Close();
        };

        formDialog.Controls.Add(lbl_nome);
        formDialog.Controls.Add(txt_nome);

        formDialog.Controls.Add(lbl_email);
        formDialog.Controls.Add(txt_email);

        formDialog.Controls.Add(lbl_email_confirm);
        formDialog.Controls.Add(txt_email_confirm);

        formDialog.Controls.Add(lbl_senha);
        formDialog.Controls.Add(txt_senha);

        formDialog.Controls.Add(lbl_senha_confirm);
        formDialog.Controls.Add(txt_senha_confirm);

        formDialog.Controls.Add(btn_cadastrar);

        formDialog.ShowDialog();
    }

    private void btnCadastrar_Click(TextBox txtNome, TextBox txtLogin, TextBox txtSenha, AuthService auth)
    {
        if (!Validator.CamposValidos(txtNome.Text, txtLogin.Text, txtSenha.Text))
        {
            MessageBox.Show("Preencha todos os campos.");
            return;
        }

        bool sucesso = auth.Cadastrar(txtNome.Text, txtLogin.Text, txtSenha.Text);

        MessageBox.Show(sucesso ? "Usuário cadastrado." : "Usuário já existe.");
    }

    private void btnLogin_Click(TextBox txtLogin, TextBox txtSenha, AuthService auth)
    {
        AuthResponse autenticacao = auth.Login(txtLogin.Text, txtSenha.Text); // tenta fazer o login
        if (autenticacao.sucesso)
        {
            UsuarioAutenticado = autenticacao.usuario;
            Console.WriteLine(UsuarioAutenticado.Nome);
            DialogResult = DialogResult.OK;

            Close();
        }

        MessageBox.Show(autenticacao.sucesso ? "Login realizado." : "Login inválido.");
    }
}


// // Simulação da base de usuários. A camada de userRepository é uma camada de dados, somente acessada pelo backend da aplicação
// // qualquer coisa só pode ser registrada nos dados se passar pelas validações e pelo processo de autenticação.
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
}

// // Isso aqui lida com o processo de autenticação. No caso, esta camada tem acesso aos dados da base.
public class AuthService
{
    private static UsuarioRepository Repository;
    // public static Usuario UsuarioAutenticado { get; private set; }

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
            Nome = nome,
            Email = login,
            SenhaHash = hashPass
        });

        return true;
    }

    public AuthResponse Login(string login, string senha)
    {
        var usuario = Repository.Buscar(login); // retorna o usuário correspondente ao email passado

        return new AuthResponse(usuario != null && BCryptHash.Verify(senha, usuario.SenhaHash), usuario);
    }
}

// objeto simples pra controlar tanto o sucesso do login como o nome do usuário, já que preciso dos dois dados.
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

// isto aqui lida com a validação dos campos.
public class Validator
{
    public static bool CamposValidos(string nome, string login, string senha)
    {
        return !string.IsNullOrWhiteSpace(nome) &&
               !string.IsNullOrWhiteSpace(login) &&
               !string.IsNullOrWhiteSpace(senha);
    }
}