
namespace ERP_Oficina.Controls
{
    public class CategoriasControl : UserControl
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnNovo;
        private Button btnEditar;

        private Panel pnlPesquisa;
        private Label lblPesquisar;
        private TextBox txtPesquisar;
        private Button btnBuscar;

        private DataGridView dgvCategorias;

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

        private List<Categoria> categoriasFiltradas =
            new List<Categoria>();

        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public CategoriasControl()
        {
            InitializeComponent();

            categoriasFiltradas =
                DadosMock.Categorias.ToList();

            CarregarPagina();
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InitializeComponent()
        {
            SuspendLayout();

            // =====================================================
            // USER CONTROL
            // =====================================================

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
                Text = "Categorias",

                Font = new Font(
                    "Segoe UI",
                    18F,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(
                        35,
                        35,
                        35
                    ),

                AutoSize = true,

                Location =
                    new Point(
                        0,
                        10
                    )
            };

            // =====================================================
            // BOTÃO EDITAR
            // =====================================================

            btnEditar = CriarBotao(
                "Editar",
                Color.White,
                Color.FromArgb(
                    70,
                    70,
                    70
                )
            );

            btnEditar.Width = 90;
            btnEditar.Height = 35;

            btnEditar.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnEditar.Location =
                new Point(
                    pnlHeader.Width - 190,
                    5
                );

            btnEditar.Click += BtnEditar_Click;

            // =====================================================
            // BOTÃO NOVO
            // =====================================================

            btnNovo = CriarBotao(
                "Novo",
                Color.FromArgb(
                    0,
                    120,
                    215
                ),
                Color.White
            );

            btnNovo.Width = 90;
            btnNovo.Height = 35;

            btnNovo.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnNovo.Location =
                new Point(
                    pnlHeader.Width - 90,
                    5
                );

            btnNovo.Click += BtnNovo_Click;

            // =====================================================
            // RESPONSIVIDADE
            // =====================================================

            pnlHeader.Resize += (s, e) =>
            {
                btnNovo.Location =
                    new Point(
                        pnlHeader.ClientSize.Width -
                        btnNovo.Width,
                        5
                    );

                btnEditar.Location =
                    new Point(
                        pnlHeader.ClientSize.Width -
                        btnNovo.Width -
                        btnEditar.Width -
                        10,
                        5
                    );
            };

            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnEditar);
            pnlHeader.Controls.Add(btnNovo);

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

                Font = new Font(
                    "Segoe UI",
                    10F
                ),

                ForeColor =
                    Color.FromArgb(
                        60,
                        60,
                        60
                    ),

                AutoSize = true,

                Location =
                    new Point(
                        0,
                        14
                    )
            };

            txtPesquisar = new TextBox
            {
                Font = new Font(
                    "Segoe UI",
                    10F
                ),

                Location =
                    new Point(
                        80,
                        10
                    ),

                Height = 32,

                Width = 400,

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            btnBuscar = CriarBotao(
                "Buscar",
                Color.FromArgb(
                    0,
                    120,
                    215
                ),
                Color.White
            );

            btnBuscar.Width = 80;
            btnBuscar.Height = 32;

            btnBuscar.Location =
                new Point(
                    490,
                    10
                );

            btnBuscar.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnBuscar.Click += BtnBuscar_Click;

            // =====================================================
            // RESPONSIVIDADE DA PESQUISA
            // =====================================================

            pnlPesquisa.Resize += (s, e) =>
            {
                btnBuscar.Location =
                    new Point(
                        pnlPesquisa.ClientSize.Width -
                        btnBuscar.Width,
                        10
                    );

                txtPesquisar.Width =
                    pnlPesquisa.ClientSize.Width -
                    lblPesquisar.Width -
                    btnBuscar.Width -
                    25;
            };

            txtPesquisar.KeyDown +=
                TxtPesquisar_KeyDown;

            pnlPesquisa.Controls.Add(lblPesquisar);
            pnlPesquisa.Controls.Add(txtPesquisar);
            pnlPesquisa.Controls.Add(btnBuscar);

            // =====================================================
            // GRID
            // =====================================================

            dgvCategorias = new DataGridView
            {
                Dock = DockStyle.Fill,

                BackgroundColor = Color.White,

                BorderStyle =
                    BorderStyle.None,

                AllowUserToAddRows = false,

                AllowUserToDeleteRows = false,

                AllowUserToResizeRows = false,

                AutoGenerateColumns = false,

                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,

                MultiSelect = false,

                RowHeadersVisible = false,

                EnableHeadersVisualStyles = false,

                ColumnHeadersHeight = 40,

                GridColor =
                    Color.FromArgb(
                        230,
                        230,
                        230
                    ),

                Font = new Font(
                    "Segoe UI",
                    10F
                )
            };

            // =====================================================
            // HEADER DO GRID
            // =====================================================

            dgvCategorias.ColumnHeadersDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor =
                        Color.FromArgb(
                            245,
                            246,
                            248
                        ),

                    ForeColor =
                        Color.FromArgb(
                            50,
                            50,
                            50
                        ),

                    Font = new Font(
                        "Segoe UI",
                        10F,
                        FontStyle.Bold
                    ),

                    Alignment =
                        DataGridViewContentAlignment.MiddleLeft,

                    Padding = new Padding(5)
                };

            // =====================================================
            // CÉLULAS
            // =====================================================

            dgvCategorias.DefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor = Color.White,

                    ForeColor =
                        Color.FromArgb(
                            50,
                            50,
                            50
                        ),

                    SelectionBackColor =
                        Color.FromArgb(
                            230,
                            240,
                            250
                        ),

                    SelectionForeColor =
                        Color.FromArgb(
                            30,
                            30,
                            30
                        ),

                    Padding = new Padding(5)
                };

            dgvCategorias.RowTemplate.Height = 40;

            // =====================================================
            // NOME
            // =====================================================

            dgvCategorias.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Nome",

                    HeaderText = "Nome",

                    DataPropertyName =
                        "Nome",

                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,

                    FillWeight = 35
                }
            );

            // =====================================================
            // DESCRIÇÃO
            // =====================================================

            dgvCategorias.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Descricao",

                    HeaderText = "Descrição",

                    DataPropertyName =
                        "Descricao",

                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,

                    FillWeight = 65
                }
            );

            // =====================================================
            // PAGINAÇÃO
            // =====================================================

            pnlPaginacao = new Panel
            {
                Dock = DockStyle.Bottom,

                Height = 50,

                BackColor = Color.White
            };

            pnlBotoesPaginacao =
                new FlowLayoutPanel
                {
                    Dock = DockStyle.Right,

                    AutoSize = true,

                    Height = 40,

                    FlowDirection =
                        FlowDirection.LeftToRight,

                    WrapContents = false,

                    Padding =
                        new Padding(
                            0,
                            5,
                            0,
                            0
                        )
                };

            btnAnterior =
                CriarBotaoPagina("‹");

            btnProximo =
                CriarBotaoPagina("›");

            btnAnterior.Click +=
                BtnAnterior_Click;

            btnProximo.Click +=
                BtnProximo_Click;

            pnlBotoesPaginacao.Controls.Add(
                btnAnterior
            );

            pnlBotoesPaginacao.Controls.Add(
                btnProximo
            );

            pnlPaginacao.Controls.Add(
                pnlBotoesPaginacao
            );

            // =====================================================
            // CONTROLES
            // =====================================================

            Controls.Add(dgvCategorias);

            Controls.Add(pnlPaginacao);

            Controls.Add(pnlPesquisa);

            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }

        // =========================================================
        // CARREGAR PÁGINA
        // =========================================================

        private void CarregarPagina()
        {
            int quantidadeTotal =
                categoriasFiltradas.Count;

            totalPaginas =
                (int)Math.Ceiling(
                    (double)quantidadeTotal /
                    itensPorPagina
                );

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (paginaAtual > totalPaginas)
                paginaAtual = totalPaginas;

            int indiceInicial =
                (paginaAtual - 1) *
                itensPorPagina;

            List<Categoria> categoriasPagina =
                categoriasFiltradas
                    .Skip(indiceInicial)
                    .Take(itensPorPagina)
                    .ToList();

            dgvCategorias.DataSource = null;

            dgvCategorias.DataSource =
                categoriasPagina;

            AtualizarPaginacao();
        }

        // =========================================================
        // PESQUISA
        // =========================================================

        private void Pesquisar()
        {
            string pesquisa =
                txtPesquisar.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                pesquisa))
            {
                categoriasFiltradas =
                    DadosMock.Categorias.ToList();
            }
            else
            {
                pesquisa =
                    pesquisa.ToLower();

                categoriasFiltradas =
                    DadosMock.Categorias
                        .Where(x =>
                            x.Nome
                                .ToLower()
                                .Contains(pesquisa)

                            ||

                            x.Descricao
                                .ToLower()
                                .Contains(pesquisa)
                        )
                        .ToList();
            }

            paginaAtual = 1;

            CarregarPagina();
        }

        // =========================================================
        // NOVA CATEGORIA
        // =========================================================

        private void BtnNovo_Click(
            object sender,
            EventArgs e)
        {
            using (FormCategoria form =
                new FormCategoria())
            {
                if (form.ShowDialog() !=
                    DialogResult.OK)
                {
                    return;
                }

                int novoId =
                    DadosMock.Categorias.Count == 0
                        ? 1
                        : DadosMock.Categorias.Max(
                            x => x.Id
                        ) + 1;

                Categoria categoria =
                    new Categoria
                    {
                        Id = novoId,

                        Nome =
                            form.Nome,

                        Descricao =
                            form.Descricao
                    };

                DadosMock.Categorias.Add(
                    categoria
                );

                Pesquisar();
            }
        }

        // =========================================================
        // EDITAR CATEGORIA
        // =========================================================

        private void BtnEditar_Click(
            object sender,
            EventArgs e)
        {
            EditarCategoriaSelecionada();
        }

        private void EditarCategoriaSelecionada()
        {
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecione uma categoria.",
                    "Editar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Categoria categoria =
                dgvCategorias
                    .CurrentRow
                    .DataBoundItem as Categoria;

            if (categoria == null)
                return;

            using (FormCategoria form =
                new FormCategoria(categoria))
            {
                if (form.ShowDialog() !=
                    DialogResult.OK)
                {
                    return;
                }

                categoria.Nome =
                    form.Nome;

                categoria.Descricao =
                    form.Descricao;

                Pesquisar();
            }
        }

        // =========================================================
        // PESQUISA - EVENTOS
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

        // =========================================================
        // PAGINAÇÃO
        // =========================================================

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

        private void AtualizarPaginacao()
        {
            pnlBotoesPaginacao.Controls.Clear();

            btnAnterior.Enabled =
                paginaAtual > 1;

            pnlBotoesPaginacao.Controls.Add(
                btnAnterior
            );

            for (
                int i = 1;
                i <= totalPaginas;
                i++
            )
            {
                Button btnPagina =
                    CriarBotaoPagina(
                        i.ToString()
                    );

                int pagina = i;

                btnPagina.Click += (s, e) =>
                {
                    paginaAtual = pagina;

                    CarregarPagina();
                };

                if (pagina == paginaAtual)
                {
                    btnPagina.BackColor =
                        Color.FromArgb(
                            0,
                            120,
                            215
                        );

                    btnPagina.ForeColor =
                        Color.White;

                    btnPagina.FlatAppearance
                        .BorderColor =
                        Color.FromArgb(
                            0,
                            120,
                            215
                        );
                }

                pnlBotoesPaginacao.Controls.Add(
                    btnPagina
                );
            }

            btnProximo.Enabled =
                paginaAtual < totalPaginas;

            pnlBotoesPaginacao.Controls.Add(
                btnProximo
            );
        }

        // =========================================================
        // BOTÃO PADRÃO
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

                FlatStyle =
                    FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    9.5F
                ),

                Cursor = Cursors.Hand,

                FlatAppearance =
                {
                    BorderColor = background,
                    BorderSize = 1
                }
            };
        }

        // =========================================================
        // BOTÃO PAGINAÇÃO
        // =========================================================

        private Button CriarBotaoPagina(
            string texto)
        {
            return new Button
            {
                Text = texto,

                Width = 36,

                Height = 32,

                Margin =
                    new Padding(
                        3,
                        0,
                        3,
                        0
                    ),

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(
                        60,
                        60,
                        60
                    ),

                FlatStyle =
                    FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    9F
                ),

                Cursor = Cursors.Hand,

                FlatAppearance =
                {
                    BorderColor =
                        Color.FromArgb(
                            220,
                            220,
                            220
                        ),

                    BorderSize = 1
                }
            };
        }
    }

    // =============================================================
    // FORMULÁRIO DE CATEGORIA
    // =============================================================

    public class FormCategoria : Form
    {
        private TextBox txtNome;

        private TextBox txtDescricao;

        private Button btnCancelar;

        private Button btnSalvar;

        // =========================================================
        // PROPRIEDADES
        // =========================================================

        public string Nome { get; private set; }

        public string Descricao { get; private set; }

        // =========================================================
        // NOVA CATEGORIA
        // =========================================================

        public FormCategoria()
        {
            InicializarFormulario();
        }

        // =========================================================
        // EDITAR CATEGORIA
        // =========================================================

        public FormCategoria(
            Categoria categoria)
        {
            InicializarFormulario();

            if (categoria != null)
            {
                txtNome.Text =
                    categoria.Nome;

                txtDescricao.Text =
                    categoria.Descricao;
            }
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InicializarFormulario()
        {
            Text = "Categoria";

            StartPosition =
                FormStartPosition.CenterParent;

            FormBorderStyle =
                FormBorderStyle.FixedDialog;

            MaximizeBox = false;

            MinimizeBox = false;

            ShowInTaskbar = false;

            Width = 520;

            Height = 330;

            BackColor = Color.White;

            Font = new Font(
                "Segoe UI",
                10F
            );

            // =====================================================
            // TÍTULO
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "Dados da categoria",

                Font = new Font(
                    "Segoe UI",
                    16F,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(
                        35,
                        35,
                        35
                    ),

                AutoSize = true,

                Location =
                    new Point(
                        30,
                        25
                    )
            };

            Controls.Add(lblTitulo);

            // =====================================================
            // NOME
            // =====================================================

            Label lblNome =
                CriarLabel(
                    "Nome",
                    30,
                    75
                );

            txtNome = new TextBox
            {
                Location =
                    new Point(
                        30,
                        100
                    ),

                Width = 445,

                Height = 30
            };

            Controls.Add(lblNome);

            Controls.Add(txtNome);

            // =====================================================
            // DESCRIÇÃO
            // =====================================================

            Label lblDescricao =
                CriarLabel(
                    "Descrição",
                    30,
                    140
                );

            txtDescricao = new TextBox
            {
                Location =
                    new Point(
                        30,
                        165
                    ),

                Width = 445,

                Height = 65,

                Multiline = true,

                ScrollBars =
                    ScrollBars.Vertical
            };

            Controls.Add(lblDescricao);

            Controls.Add(txtDescricao);

            // =====================================================
            // CANCELAR
            // =====================================================

            btnCancelar = new Button
            {
                Text = "Cancelar",

                Width = 100,

                Height = 35,

                Location =
                    new Point(
                        265,
                        250
                    ),

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(
                        70,
                        70,
                        70
                    ),

                FlatStyle =
                    FlatStyle.Flat,

                Cursor = Cursors.Hand
            };

            btnCancelar.FlatAppearance
                .BorderColor =
                Color.FromArgb(
                    200,
                    200,
                    200
                );

            btnCancelar.Click +=
                (s, e) =>
                {
                    DialogResult =
                        DialogResult.Cancel;

                    Close();
                };

            Controls.Add(btnCancelar);

            // =====================================================
            // SALVAR
            // =====================================================

            btnSalvar = new Button
            {
                Text = "Salvar",

                Width = 100,

                Height = 35,

                Location =
                    new Point(
                        375,
                        250
                    ),

                BackColor =
                    Color.FromArgb(
                        0,
                        120,
                        215
                    ),

                ForeColor = Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Cursor = Cursors.Hand
            };

            btnSalvar.FlatAppearance
                .BorderColor =
                Color.FromArgb(
                    0,
                    120,
                    215
                );

            btnSalvar.Click +=
                BtnSalvar_Click;

            Controls.Add(btnSalvar);

            AcceptButton = btnSalvar;

            CancelButton = btnCancelar;
        }

        // =========================================================
        // SALVAR
        // =========================================================

        private void BtnSalvar_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtNome.Text))
            {
                MessageBox.Show(
                    "Informe o nome da categoria.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtNome.Focus();

                return;
            }

            Nome =
                txtNome.Text.Trim();

            Descricao =
                txtDescricao.Text.Trim();

            DialogResult =
                DialogResult.OK;

            Close();
        }

        // =========================================================
        // LABEL
        // =========================================================

        private Label CriarLabel(
            string texto,
            int x,
            int y)
        {
            return new Label
            {
                Text = texto,

                AutoSize = true,

                Font = new Font(
                    "Segoe UI",
                    9.5F
                ),

                ForeColor =
                    Color.FromArgb(
                        60,
                        60,
                        60
                    ),

                Location =
                    new Point(
                        x,
                        y
                    )
            };
        }
    }
}