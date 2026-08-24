namespace ERP_Oficina.Forms
{
    public class FrmUsuario : Form
    {
        private Usuario usuario;
        private bool editando;

        private TextBox txtNome;
        private TextBox txtEmail;
        private TextBox txtSenha;

        private ComboBox cmbPerfil;

        private CheckBox chkAtivo;

        private Button btnSalvar;
        private Button btnCancelar;

        public FrmUsuario()
        {
            editando = false;

            InicializarFormulario();

            Text = "Novo usuário";

            chkAtivo.Checked = true;
        }

        public FrmUsuario(Usuario usuario)
        {
            this.usuario = usuario;
            editando = true;

            InicializarFormulario();

            Text = "Editar usuário";

            CarregarDados();
        }

        private void InicializarFormulario()
        {
            StartPosition = FormStartPosition.CenterParent;

            Size = new Size(500, 430);

            MinimumSize = new Size(450, 400);
            MaximizeBox = false;
            MinimizeBox = false;

            BackColor = Color.White;

            Font = new Font("Segoe UI", 10F);

            Label lblTitulo = new Label
            {
                Text = editando ? "Editar usuário" : "Novo usuário",

                Font = new Font("Segoe UI", 18F, FontStyle.Bold),

                ForeColor =
                    Color.FromArgb(
                        35,
                        35,
                        35),

                Location =
                    new Point(30, 25),

                AutoSize = true
            };

            Controls.Add(lblTitulo);

            Label lblNome =
                CriarLabel(
                    "Nome",
                    30,
                    80);

            txtNome =
                CriarTextBox(
                    30,
                    105,
                    420);

            Label lblEmail =
                CriarLabel(
                    "Email",
                    30,
                    145);

            txtEmail =
                CriarTextBox(
                    30,
                    170,
                    420);

            Label lblSenha =
                CriarLabel(
                    editando
                        ? "Nova senha (opcional)"
                        : "Senha",
                    30,
                    210);

            txtSenha =
                CriarTextBox(
                    30,
                    235,
                    420);

            txtSenha.UseSystemPasswordChar = true;

            Label lblPerfil =
                CriarLabel(
                    "Perfil",
                    30,
                    275);

            cmbPerfil = new ComboBox
            {
                Location =
                    new Point(30, 300),

                Width = 200,

                Height = 32,

                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

            cmbPerfil.Items.AddRange(
                new object[]
                {
                    "Administrador",
                    "Técnico",
                    "Atendente"
                });

            chkAtivo = new CheckBox
            {
                Text = "Usuário ativo",
                Location =
                    new Point(260, 302),

                AutoSize = true
            };

            btnCancelar =
                CriarBotao(
                    "Cancelar",
                    Color.White,
                    Color.FromArgb(
                        70,
                        70,
                        70));

            btnCancelar.Width = 100;
            btnCancelar.Height = 35;

            btnCancelar.Location =
                new Point(
                    230,
                    350);

            btnCancelar.Click +=
                (s, e) =>
                {
                    DialogResult =
                        DialogResult.Cancel;

                    Close();
                };

            btnSalvar =
                CriarBotao(
                    "Salvar",
                    Color.FromArgb(
                        0,
                        120,
                        215),
                    Color.White);

            btnSalvar.Width = 100;
            btnSalvar.Height = 35;

            btnSalvar.Location =
                new Point(
                    340,
                    350);

            btnSalvar.Click +=
                BtnSalvar_Click;

            Controls.Add(lblNome);
            Controls.Add(txtNome);

            Controls.Add(lblEmail);
            Controls.Add(txtEmail);

            Controls.Add(lblSenha);
            Controls.Add(txtSenha);

            Controls.Add(lblPerfil);
            Controls.Add(cmbPerfil);

            Controls.Add(chkAtivo);

            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
        }

        private void CarregarDados()
        {
            txtNome.Text =
                usuario.Nome;

            txtEmail.Text =
                usuario.Email;

            cmbPerfil.SelectedItem =
                usuario.Perfil;

            chkAtivo.Checked =
                usuario.Ativo;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            string nome =
                txtNome.Text.Trim();

            string email =
                txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show(
                    "Informe o nome do usuário.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNome.Focus();

                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(
                    "Informe o email do usuário.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtEmail.Focus();

                return;
            }

            if (cmbPerfil.SelectedItem == null)
            {
                MessageBox.Show(
                    "Selecione um perfil.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbPerfil.Focus();

                return;
            }

            string senha =
                txtSenha.Text;

            if (!editando &&
                string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show(
                    "Informe uma senha.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSenha.Focus();

                return;
            }

            if (!editando)
            {
                int novoId =
                    DadosMock.Usuarios.Any()
                        ? DadosMock.Usuarios.Max(x => x.Id) + 1
                        : 1;

                Usuario novoUsuario =
                    new Usuario
                    {
                        Id = novoId,

                        Nome = nome,

                        Email = email,

                        // Mock temporário.
                        // Em uma implementação real,
                        // isso deverá ser um hash.
                        SenhaHash =
                            string.IsNullOrWhiteSpace(senha)
                                ? ""
                                : senha,

                        Perfil =
                            cmbPerfil.SelectedItem
                                .ToString(),

                        DataCriacao =
                            DateTime.Now,

                        Ativo =
                            chkAtivo.Checked
                    };

                DadosMock.Usuarios.Add(
                    novoUsuario);
            }

            else
            {
                usuario.Nome = nome;

                usuario.Email = email;

                usuario.Perfil =
                    cmbPerfil.SelectedItem
                        .ToString();

                usuario.Ativo =
                    chkAtivo.Checked;

                // Só altera a senha se o usuário
                // realmente informou uma nova.
                if (!string.IsNullOrWhiteSpace(senha))
                {
                    usuario.SenhaHash = senha;
                }
            }

            DialogResult =
                DialogResult.OK;

            Close();
        }

        private Label CriarLabel(
            string texto,
            int x,
            int y)
        {
            return new Label
            {
                Text = texto,

                Font =
                    new Font(
                        "Segoe UI",
                        9F,
                        FontStyle.Bold),

                ForeColor =
                    Color.FromArgb(
                        100,
                        100,
                        100),

                Location =
                    new Point(x, y),

                AutoSize = true
            };
        }

        private TextBox CriarTextBox(
            int x,
            int y,
            int width)
        {
            return new TextBox
            {
                Location =
                    new Point(x, y),

                Width = width,

                Height = 32,

                Font =
                    new Font(
                        "Segoe UI",
                        10F)
            };
        }

        private Button CriarBotao(
            string texto,
            Color background,
            Color foreground)
        {
            return new Button
            {
                Text = texto,

                BackColor = background,

                ForeColor = foreground,

                FlatStyle =
                    FlatStyle.Flat,

                Font =
                    new Font(
                        "Segoe UI",
                        9.5F),

                Cursor =
                    Cursors.Hand,

                FlatAppearance =
                {
                    BorderColor =
                        background,

                    BorderSize = 1
                }
            };
        }
    }
}