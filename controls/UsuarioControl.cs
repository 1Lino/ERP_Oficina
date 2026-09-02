
using ERP_Oficina.Forms;
using ERP_Oficina.Models.Permissao;
using ERP_Oficina.Services.Autorizacao;

namespace ERP_Oficina.Controls
{
    public class UsuariosControl : UserControl
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private Panel pnlHeader;
        private Label lblTitulo;

        private Button btnNovo;
        private Button btnEditar;
        private Button btnAlterarStatus;

        private Panel pnlPesquisa;

        private Label lblPesquisar;
        private TextBox txtPesquisar;

        private Label lblPerfil;
        private ComboBox cmbPerfil;

        private Label lblStatus;
        private ComboBox cmbStatus;

        private Button btnBuscar;

        private DataGridView dgvUsuarios;

        private Panel pnlPaginacao;
        private FlowLayoutPanel pnlBotoesPaginacao;

        private Button btnAnterior;
        private Button btnProximo;

        // =========================================================
        // PAGINAÇÃO / DADOS
        // =========================================================

        private int paginaAtual = 1;

        private int itensPorPagina = 5;

        private int totalPaginas = 1;

        private List<Usuario> todosUsuarios = new();
        private List<Usuario> usuariosFiltrados = new();

        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public UsuariosControl()
        {
            InitializeComponent();

            CarregarDados();

            usuariosFiltrados = todosUsuarios.ToList();

            CarregarPagina();
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InitializeComponent()
        {
            SuspendLayout();

            Dock = DockStyle.Fill;

            BackColor = Color.White;

            Padding = new Padding(20);

            // =====================================================
            // HEADER
            // =====================================================

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };

            lblTitulo = new Label
            {
                Text = "Usuários",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(0, 10)
            };

            btnNovo = CriarBotao(
                "Novo",
                Color.FromArgb(0, 120, 215),
                Color.White);

            btnNovo.Width = 90;
            btnNovo.Height = 35;
            btnNovo.Enabled = Autorizacao.TemPermissao(Permissao.GerenciarUsuarios);
            btnNovo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNovo.Click += BtnNovo_Click;

            btnEditar = CriarBotao(
                "Editar",
                Color.White,
                Color.FromArgb(70, 70, 70));

            btnEditar.Width = 90;
            btnEditar.Height = 35;
            btnEditar.Enabled = Autorizacao.TemPermissao(Permissao.GerenciarUsuarios);
            btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditar.Click += BtnEditar_Click;

            btnAlterarStatus = CriarBotao(
                "Ativar/Desativar",
                Color.White,
                Color.FromArgb(70, 70, 70));

            btnAlterarStatus.Width = 130;
            btnAlterarStatus.Height = 35;
            btnAlterarStatus.Enabled = Autorizacao.TemPermissao(Permissao.GerenciarUsuarios);
            btnAlterarStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAlterarStatus.Click += BtnAlterarStatus_Click;

            pnlHeader.Resize += (s, e) =>
            {
                btnNovo.Location = new Point(
                    pnlHeader.ClientSize.Width - btnNovo.Width,
                    5);

                btnEditar.Location = new Point(
                    pnlHeader.ClientSize.Width
                    - btnNovo.Width
                    - btnEditar.Width
                    - 10,
                    5);

                btnAlterarStatus.Location = new Point(
                    pnlHeader.ClientSize.Width
                    - btnNovo.Width
                    - btnEditar.Width
                    - btnAlterarStatus.Width
                    - 20,
                    5);
            };

            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnNovo);
            pnlHeader.Controls.Add(btnEditar);
            pnlHeader.Controls.Add(btnAlterarStatus);

            // =====================================================
            // PESQUISA
            // =====================================================

            pnlPesquisa = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };

            lblPesquisar = new Label
            {
                Text = "Pesquisar:",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = true,
                Location = new Point(0, 14)
            };

            txtPesquisar = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(80, 10),
                Height = 32,
                Width = 260
            };

            lblPerfil = new Label
            {
                Text = "Perfil:",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = true,
                Location = new Point(355, 14)
            };

            cmbPerfil = new ComboBox
            {
                Location = new Point(400, 10),
                Width = 140,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbPerfil.Items.Add("Todos");

            // Os perfis são obtidos dos próprios dados.
            lblStatus = new Label
            {
                Text = "Status:",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = true,
                Location = new Point(555, 14)
            };

            cmbStatus = new ComboBox
            {
                Location = new Point(610, 10),
                Width = 120,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbStatus.Items.AddRange(
                new object[]
                {
                    "Todos",
                    "Ativo",
                    "Inativo"
                });

            cmbStatus.SelectedIndex = 0;

            btnBuscar = CriarBotao(
                "Buscar",
                Color.FromArgb(0, 120, 215),
                Color.White);

            btnBuscar.Width = 80;
            btnBuscar.Height = 32;
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Click += BtnBuscar_Click;

            pnlPesquisa.Resize += (s, e) =>
            {
                btnBuscar.Location = new Point(
                    pnlPesquisa.ClientSize.Width - btnBuscar.Width,
                    10);
            };

            txtPesquisar.KeyDown += TxtPesquisar_KeyDown;

            pnlPesquisa.Controls.Add(lblPesquisar);
            pnlPesquisa.Controls.Add(txtPesquisar);
            pnlPesquisa.Controls.Add(lblPerfil);
            pnlPesquisa.Controls.Add(cmbPerfil);
            pnlPesquisa.Controls.Add(lblStatus);
            pnlPesquisa.Controls.Add(cmbStatus);
            pnlPesquisa.Controls.Add(btnBuscar);

            // =====================================================
            // GRID
            // =====================================================

            dgvUsuarios = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 40,
                GridColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Segoe UI", 10F)
            };

            dgvUsuarios.ColumnHeadersDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(245, 246, 248),
                    ForeColor = Color.FromArgb(50, 50, 50),
                    Font = new Font(
                        "Segoe UI",
                        10F,
                        FontStyle.Bold),
                    Alignment =
                        DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(5)
                };

            dgvUsuarios.DefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(50, 50, 50),
                    SelectionBackColor =
                        Color.FromArgb(230, 240, 250),
                    SelectionForeColor =
                        Color.FromArgb(30, 30, 30),
                    Padding = new Padding(5)
                };

            dgvUsuarios.RowTemplate.Height = 40;

            // =====================================================
            // COLUNAS
            // =====================================================

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 8
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Nome",
                DataPropertyName = "Nome",
                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 25
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 28
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Perfil",
                HeaderText = "Perfil",
                DataPropertyName = "Perfil",
                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DataCriacao",
                HeaderText = "Data de criação",
                DataPropertyName = "DataCriacao",
                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15,
                DefaultCellStyle =
                    new DataGridViewCellStyle
                    {
                        Format = "dd/MM/yyyy"
                    }
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ativo",
                HeaderText = "Status",
                DataPropertyName = "Ativo",
                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 12
            });

            dgvUsuarios.CellFormatting +=
                DgvUsuarios_CellFormatting;

            dgvUsuarios.CellDoubleClick +=
                DgvUsuarios_CellDoubleClick;

            // =====================================================
            // PAGINAÇÃO
            // =====================================================

            pnlPaginacao = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.White
            };

            pnlBotoesPaginacao = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                Height = 40,
                FlowDirection =
                    FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0)
            };

            btnAnterior = CriarBotaoPagina("‹");
            btnProximo = CriarBotaoPagina("›");

            btnAnterior.Click += BtnAnterior_Click;
            btnProximo.Click += BtnProximo_Click;

            pnlBotoesPaginacao.Controls.Add(btnAnterior);
            pnlBotoesPaginacao.Controls.Add(btnProximo);

            pnlPaginacao.Controls.Add(
                pnlBotoesPaginacao);

            // =====================================================
            // CONTROLES
            // =====================================================

            Controls.Add(dgvUsuarios);
            Controls.Add(pnlPaginacao);
            Controls.Add(pnlPesquisa);
            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }

        // =========================================================
        // DADOS
        // =========================================================

        private void CarregarDados()
        {
            todosUsuarios = DadosMock.Usuarios.ToList();

            cmbPerfil.Items.Clear();
            cmbPerfil.Items.Add("Todos");

            foreach (string perfil in todosUsuarios
                .Select(x => x.Perfil)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(x => x))
            {
                cmbPerfil.Items.Add(perfil);
            }

            cmbPerfil.SelectedIndex = 0;
        }

        // =========================================================
        // PÁGINA
        // =========================================================

        private void CarregarPagina()
        {
            int quantidadeTotal =
                usuariosFiltrados.Count;

            totalPaginas =
                (int)Math.Ceiling(
                    (double)quantidadeTotal
                    / itensPorPagina);

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (paginaAtual > totalPaginas)
                paginaAtual = totalPaginas;

            int indiceInicial =
                (paginaAtual - 1)
                * itensPorPagina;

            List<Usuario> pagina =
                usuariosFiltrados
                    .Skip(indiceInicial)
                    .Take(itensPorPagina)
                    .ToList();

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = pagina;

            AtualizarPaginacao();
        }

        // =========================================================
        // PAGINAÇÃO
        // =========================================================

        private void AtualizarPaginacao()
        {
            pnlBotoesPaginacao.Controls.Clear();

            btnAnterior.Enabled =
                paginaAtual > 1;

            pnlBotoesPaginacao.Controls.Add(
                btnAnterior);

            for (int i = 1;
                 i <= totalPaginas;
                 i++)
            {
                Button btnPagina =
                    CriarBotaoPagina(i.ToString());

                int pagina = i;

                btnPagina.Click += (s, e) =>
                {
                    paginaAtual = pagina;
                    CarregarPagina();
                };

                if (pagina == paginaAtual)
                {
                    btnPagina.BackColor =
                        Color.FromArgb(0, 120, 215);

                    btnPagina.ForeColor =
                        Color.White;

                    btnPagina.FlatAppearance.BorderColor =
                        Color.FromArgb(0, 120, 215);
                }

                pnlBotoesPaginacao.Controls.Add(
                    btnPagina);
            }

            btnProximo.Enabled =
                paginaAtual < totalPaginas;

            pnlBotoesPaginacao.Controls.Add(
                btnProximo);
        }

        // =========================================================
        // PESQUISA
        // =========================================================

        private void Pesquisar()
        {
            string pesquisa =
                txtPesquisar.Text.Trim();

            string perfil =
                cmbPerfil.SelectedItem?.ToString();

            string status =
                cmbStatus.SelectedItem?.ToString();

            IEnumerable<Usuario> consulta =
                todosUsuarios;

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = pesquisa.ToLower();

                consulta = consulta.Where(x =>
                    (x.Nome ?? "")
                        .ToLower()
                        .Contains(pesquisa)
                    ||
                    (x.Email ?? "")
                        .ToLower()
                        .Contains(pesquisa)
                );
            }

            if (!string.IsNullOrWhiteSpace(perfil)
                && perfil != "Todos")
            {
                consulta =
                    consulta.Where(x =>
                        x.Perfil == perfil);
            }

            if (!string.IsNullOrWhiteSpace(status)
                && status != "Todos")
            {
                bool ativo = status == "Ativo";

                consulta =
                    consulta.Where(x =>
                        x.Ativo == ativo);
            }

            usuariosFiltrados =
                consulta.ToList();

            paginaAtual = 1;

            CarregarPagina();
        }

        // =========================================================
        // NOVO
        // =========================================================

        private void BtnNovo_Click(
            object sender,
            EventArgs e)
        {
            using (FrmUsuario form =
                new FrmUsuario())
            {
                if (form.ShowDialog()
                    == DialogResult.OK)
                {
                    CarregarDados();

                    usuariosFiltrados =
                        todosUsuarios.ToList();

                    paginaAtual = 1;

                    CarregarPagina();
                }
            }
        }

        // =========================================================
        // EDITAR
        // =========================================================

        private void BtnEditar_Click(
            object sender,
            EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecione um usuário.",
                    "Editar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            Usuario usuario =
                dgvUsuarios.CurrentRow
                    .DataBoundItem as Usuario;

            if (usuario == null)
                return;

            using (FrmUsuario form =
                new FrmUsuario(usuario))
            {
                if (form.ShowDialog()
                    == DialogResult.OK)
                {
                    CarregarDados();

                    usuariosFiltrados =
                        todosUsuarios.ToList();

                    paginaAtual = 1;

                    CarregarPagina();
                }
            }
        }

        // =========================================================
        // ALTERAR STATUS
        // =========================================================

        private void BtnAlterarStatus_Click(
            object sender,
            EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecione um usuário.",
                    "Alterar status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            Usuario usuario =
                dgvUsuarios.CurrentRow
                    .DataBoundItem as Usuario;

            if (usuario == null)
                return;

            string novoStatus =
                usuario.Ativo
                    ? "desativar"
                    : "ativar";

            DialogResult resposta =
                MessageBox.Show(
                    $"Deseja {novoStatus} o usuário " +
                    $"'{usuario.Nome}'?",
                    "Alterar status",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (resposta != DialogResult.Yes)
                return;

            usuario.Ativo = !usuario.Ativo;

            Pesquisar();
        }

        // =========================================================
        // EVENTOS
        // =========================================================

        private void BtnBuscar_Click(
            object sender,
            EventArgs e)
        {
            Pesquisar();
        }

        private void TxtPesquisar_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Pesquisar();
            }
        }

        private void BtnAnterior_Click(
            object sender,
            EventArgs e)
        {
            if (paginaAtual <= 1)
                return;

            paginaAtual--;
            CarregarPagina();
        }

        private void BtnProximo_Click(
            object sender,
            EventArgs e)
        {
            if (paginaAtual >= totalPaginas)
                return;

            paginaAtual++;
            CarregarPagina();
        }

        private void DgvUsuarios_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Usuario usuario =
                dgvUsuarios.Rows[e.RowIndex]
                    .DataBoundItem as Usuario;

            if (usuario == null)
                return;

            using (FrmUsuario form =
                new FrmUsuario(usuario))
            {
                if (form.ShowDialog()
                    == DialogResult.OK)
                {
                    CarregarDados();

                    usuariosFiltrados =
                        todosUsuarios.ToList();

                    paginaAtual = 1;

                    CarregarPagina();
                }
            }
        }

        // =========================================================
        // FORMATAÇÃO
        // =========================================================

        private void DgvUsuarios_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvUsuarios.Columns[e.ColumnIndex]
                .Name != "Ativo")
                return;

            if (e.Value == null)
                return;

            bool ativo =
                Convert.ToBoolean(e.Value);

            e.Value =
                ativo
                    ? "Ativo"
                    : "Inativo";

            e.CellStyle.ForeColor =
                ativo
                    ? Color.FromArgb(25, 135, 84)
                    : Color.FromArgb(220, 53, 69);

            e.CellStyle.Font =
                new Font(
                    dgvUsuarios.Font,
                    FontStyle.Bold);

            e.FormattingApplied = true;
        }

        // =========================================================
        // BOTÕES
        // =========================================================

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
                FlatStyle = FlatStyle.Flat,
                Font = new Font(
                    "Segoe UI",
                    9.5F),
                Cursor = Cursors.Hand,
                FlatAppearance =
                {
                    BorderColor = background,
                    BorderSize = 1
                }
            };
        }

        private Button CriarBotaoPagina(
            string texto)
        {
            return new Button
            {
                Text = texto,
                Width = 36,
                Height = 32,
                Margin = new Padding(
                    3, 0, 3, 0),
                BackColor = Color.White,
                ForeColor =
                    Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font(
                    "Segoe UI",
                    9F),
                Cursor = Cursors.Hand,
                FlatAppearance =
                {
                    BorderColor =
                        Color.FromArgb(220, 220, 220),
                    BorderSize = 1
                }
            };
        }
    }
}