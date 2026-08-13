
using ERP_Oficina.Forms;

namespace ERP_Oficina.Controls
{
    public class EstoqueControl : UserControl
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private Panel pnlHeader;
        private Label lblTitulo;

        private Button btnEntrada;
        private Button btnSaida;
        private Button btnAjuste;

        private Panel pnlResumo;

        private Panel pnlTotalProdutos;
        private Panel pnlEstoqueBaixo;
        private Panel pnlSemEstoque;
        private Panel pnlValorEstoque;

        private Label lblTotalProdutos;
        private Label lblValorTotal;
        private Label lblProdutosBaixo;
        private Label lblProdutosSemEstoque;

        private Panel pnlPesquisa;
        private Label lblPesquisar;
        private TextBox txtPesquisar;
        private Button btnBuscar;

        private DataGridView dgvEstoque;

        // =========================================================
        // DADOS
        // =========================================================

        private List<Produto> produtosFiltrados =
            new List<Produto>();

        private const decimal ESTOQUE_BAIXO = 5;

        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public EstoqueControl()
        {
            InitializeComponent();

            produtosFiltrados =
                DadosMock.Produtos.ToList();

            AtualizarResumo();

            CarregarEstoque();
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
                Text = "Estoque",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(0, 10)
            };

            // =====================================================
            // BOTÃO ENTRADA
            // =====================================================

            btnEntrada = CriarBotao("Entrada", Color.FromArgb(25, 135, 84), Color.White);
            btnEntrada.Width = 90;
            btnEntrada.Height = 35;
            btnEntrada.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEntrada.Click += BtnEntrada_Click;

            // =====================================================
            // BOTÃO SAÍDA
            // =====================================================

            btnSaida = CriarBotao("Saída", Color.FromArgb(220, 53, 69), Color.White);
            btnSaida.Width = 80;
            btnSaida.Height = 35;
            btnSaida.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaida.Click += BtnSaida_Click;

            // =====================================================
            // BOTÃO AJUSTE
            // =====================================================

            btnAjuste = CriarBotao("Ajuste", Color.White, Color.FromArgb(70, 70, 70));
            btnAjuste.Width = 80;
            btnAjuste.Height = 35;
            btnAjuste.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAjuste.Click += BtnAjuste_Click;

            // =====================================================
            // RESPONSIVIDADE DO HEADER
            // =====================================================

            pnlHeader.Resize += (s, e) =>
            {
                btnAjuste.Location = new Point(pnlHeader.ClientSize.Width - btnAjuste.Width, 5);
                btnSaida.Location = new Point(btnAjuste.Left - btnSaida.Width - 10, 5);
                btnEntrada.Location = new Point(btnSaida.Left - btnEntrada.Width - 10, 5);
            };

            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnEntrada);
            pnlHeader.Controls.Add(btnSaida);
            pnlHeader.Controls.Add(btnAjuste);

            // =====================================================
            // RESUMO
            // =====================================================

            pnlResumo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.White
            };

            pnlTotalProdutos = CriarCardResumo("Produtos", out lblTotalProdutos);
            pnlEstoqueBaixo = CriarCardResumo("Estoque baixo", out lblProdutosBaixo);
            pnlSemEstoque = CriarCardResumo("Sem estoque", out lblProdutosSemEstoque);
            pnlValorEstoque = CriarCardResumo("Valor em estoque", out lblValorTotal);

            pnlResumo.Controls.Add(pnlTotalProdutos);
            pnlResumo.Controls.Add(pnlEstoqueBaixo);
            pnlResumo.Controls.Add(pnlSemEstoque);
            pnlResumo.Controls.Add(pnlValorEstoque);
            pnlResumo.Resize += AjustarCardsResumo;

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

                Location =
                    new Point(
                        0,
                        14
                    )
            };

            txtPesquisar = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(80, 10),
                Height = 32,
                Width = 400,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            btnBuscar = CriarBotao("Buscar", Color.FromArgb(0, 120, 215), Color.White);
            btnBuscar.Width = 80;
            btnBuscar.Height = 32;
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Click += BtnBuscar_Click;

            pnlPesquisa.Resize += (s, e) =>
            {
                btnBuscar.Location = new Point(pnlPesquisa.ClientSize.Width - btnBuscar.Width, 10);
                txtPesquisar.Width = pnlPesquisa.ClientSize.Width - lblPesquisar.Width - btnBuscar.Width - 25;
            };
            txtPesquisar.KeyDown += TxtPesquisar_KeyDown;

            pnlPesquisa.Controls.Add(lblPesquisar);
            pnlPesquisa.Controls.Add(txtPesquisar);
            pnlPesquisa.Controls.Add(btnBuscar);

            // =====================================================
            // GRID
            // =====================================================

            dgvEstoque = new DataGridView
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

            dgvEstoque.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 246, 248),
                ForeColor = Color.FromArgb(50, 50, 50),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            dgvEstoque.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 50, 50),
                SelectionBackColor = Color.FromArgb(230, 240, 250),
                SelectionForeColor = Color.FromArgb(30, 30, 30),
                Padding = new Padding(5)
            };

            dgvEstoque.RowTemplate.Height = 40;

            // =====================================================
            // COLUNAS
            // =====================================================

            dgvEstoque.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Nome",
                    HeaderText = "Produto",
                    DataPropertyName = "Nome",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 25
                }
            );

            dgvEstoque.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "SKU",
                    HeaderText = "SKU",
                    DataPropertyName = "SKU",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15
                }
            );

            dgvEstoque.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Categoria",
                    HeaderText = "Categoria",
                    DataPropertyName = "CategoriaNome",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 18
                }
            );

            dgvEstoque.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Estoque",
                    HeaderText = "Estoque",
                    DataPropertyName = "EstoqueAtual",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 12,
                    DefaultCellStyle =
                        new DataGridViewCellStyle
                        {
                            Alignment =
                                DataGridViewContentAlignment.MiddleRight,
                            Format = "N2"
                        }
                }
            );

            dgvEstoque.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Preco",
                    HeaderText = "Preço",
                    DataPropertyName = "Preco",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15,
                    DefaultCellStyle =
                        new DataGridViewCellStyle
                        {
                            Alignment =
                                DataGridViewContentAlignment.MiddleRight,
                            Format = "C2"
                        }
                }
            );

            dgvEstoque.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Valor",
                    HeaderText = "Valor em estoque",
                    DataPropertyName = "Valor",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 18,
                    DefaultCellStyle =
                        new DataGridViewCellStyle
                        {
                            Alignment =
                                DataGridViewContentAlignment.MiddleRight,
                            Format = "C2"
                        }
                }
            );

            dgvEstoque.CellFormatting += DgvEstoque_CellFormatting;

            // =====================================================
            // CONTROLES
            // =====================================================

            Controls.Add(dgvEstoque);
            Controls.Add(pnlPesquisa);
            Controls.Add(pnlResumo);
            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }

        // =========================================================
        // CARDS
        // =========================================================

        private Panel CriarCardResumo(string titulo, out Label valor)
        {
            Panel card = new Panel
            {
                Height = 70,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            Label lbl = new Label
            {
                Text = titulo,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(15, 10)
            };

            valor = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(15, 32)
            };

            card.Controls.Add(lbl);
            card.Controls.Add(valor);

            return card;
        }

        private void AjustarCardsResumo(object sender, EventArgs e)
        {
            int espaco = 10;
            int largura = (pnlResumo.ClientSize.Width - (espaco * 3)) / 4;

            pnlTotalProdutos.Width = largura;
            pnlEstoqueBaixo.Width = largura;
            pnlSemEstoque.Width = largura;
            pnlValorEstoque.Width = largura;

            pnlTotalProdutos.Location = new Point(0, 5);
            pnlEstoqueBaixo.Location = new Point(largura + espaco, 5);
            pnlSemEstoque.Location = new Point((largura + espaco) * 2, 5);
            pnlValorEstoque.Location = new Point((largura + espaco) * 3, 5);
        }

        // =========================================================
        // RESUMO
        // =========================================================

        private void AtualizarResumo()
        {
            List<Produto> produtos = DadosMock.Produtos;
            int total = produtos.Count;
            int baixo = produtos.Count(x => x.EstoqueAtual > 0 && x.EstoqueAtual <= ESTOQUE_BAIXO);
            int semEstoque = produtos.Count(x => x.EstoqueAtual <= 0);
            decimal valor = produtos.Sum(x => x.EstoqueAtual * x.Preco);

            lblTotalProdutos.Text = total.ToString();
            lblProdutosBaixo.Text = baixo.ToString();
            lblProdutosSemEstoque.Text = semEstoque.ToString();
            lblValorTotal.Text = valor.ToString("C2");

            lblProdutosBaixo.ForeColor = baixo > 0 ? Color.FromArgb(230, 126, 34) : Color.FromArgb(40, 40, 40);
            lblProdutosSemEstoque.ForeColor = semEstoque > 0 ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 40, 40);
        }

        // =========================================================
        // GRID
        // =========================================================

        private void CarregarEstoque()
        {
            dgvEstoque.DataSource = null;
            dgvEstoque.DataSource = produtosFiltrados.Select(x => new
            {
                x.Id,
                x.Nome,
                x.SKU,
                x.CategoriaNome,
                x.EstoqueAtual,
                x.Preco,
                Valor = x.EstoqueAtual * x.Preco
            }).ToList();
        }

        // =========================================================
        // PESQUISA
        // =========================================================

        private void Pesquisar()
        {
            string pesquisa = txtPesquisar.Text.Trim();

            if (string.IsNullOrWhiteSpace(pesquisa))
            {
                produtosFiltrados = DadosMock.Produtos.ToList();
            }
            else
            {
                pesquisa = pesquisa.ToLower();
                produtosFiltrados = DadosMock.Produtos.Where(x =>
                    x.Nome.ToLower().Contains(pesquisa) ||
                    x.SKU.ToLower().Contains(pesquisa) ||
                    (x.CategoriaNome ?? "").ToLower().Contains(pesquisa)
                ).ToList();
            }

            CarregarEstoque();
        }

        // =========================================================
        // PESQUISA - EVENTOS
        // =========================================================

        private void BtnBuscar_Click(object sender, EventArgs e) => Pesquisar();

        private void TxtPesquisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                Pesquisar();
            }
        }

        // =========================================================
        // FORMATAÇÃO
        // =========================================================

        private void DgvEstoque_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvEstoque.Columns[e.ColumnIndex].Name != "Estoque")
                return;

            if (e.Value == null)
                return;

            decimal estoque = Convert.ToDecimal(e.Value);

            if (estoque <= 0)
            {
                e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
                e.CellStyle.Font = new Font(dgvEstoque.Font, FontStyle.Bold);
            }
            else if (estoque <= ESTOQUE_BAIXO)
            {
                e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                e.CellStyle.Font = new Font(dgvEstoque.Font, FontStyle.Bold);
            }
        }

        // =========================================================
        // MOVIMENTAÇÕES
        // =========================================================

        private void AbrirFormMovimentacao(string tipo)
        {
            using (FormMovimentacaoEstoque form = new FormMovimentacaoEstoque(tipo))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    AtualizarResumo();
                    produtosFiltrados = DadosMock.Produtos.ToList();
                    CarregarEstoque();
                }
            }
        }

        private void BtnEntrada_Click(object sender, EventArgs e) => AbrirFormMovimentacao("Entrada");

        private void BtnSaida_Click(object sender, EventArgs e) => AbrirFormMovimentacao("Saída");

        private void BtnAjuste_Click(object sender, EventArgs e) => AbrirFormMovimentacao("Ajuste");

        // =========================================================
        // BOTÃO
        // =========================================================

        private Button CriarBotao(string texto, Color background, Color foreground)
        {
            return new Button
            {
                Text = texto,
                BackColor = background,
                ForeColor = foreground,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderColor = background, BorderSize = 1 }
            };
        }
    }
}