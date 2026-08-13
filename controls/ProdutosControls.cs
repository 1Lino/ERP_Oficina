
using ERP_Oficina.Forms;

namespace ERP_Oficina.Controls
{
    public class ProdutosControl : UserControl
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

        private DataGridView dgvProdutos;

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
        private List<Produto> produtosFiltrados = new List<Produto>();

        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public ProdutosControl()
        {
            InitializeComponent();

            produtosFiltrados = DadosMock.Produtos.ToList();
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
                Text = "Produtos",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(0, 10)
            };

            // =====================================================
            // EDITAR
            // =====================================================

            btnEditar = CriarBotao("Editar", Color.White, Color.FromArgb(70, 70, 70));
            btnEditar.Width = 90;
            btnEditar.Height = 35;
            btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditar.Location = new Point(pnlHeader.Width - 190, 5);

            btnEditar.Click += BtnEditar_Click;

            // =====================================================
            // NOVO
            // =====================================================

            btnNovo = CriarBotao("Novo", Color.FromArgb(0, 120, 215), Color.White);
            btnNovo.Width = 90;
            btnNovo.Height = 35;
            btnNovo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNovo.Location = new Point(pnlHeader.Width - 90, 5);

            btnNovo.Click += BtnNovo_Click;

            // =====================================================
            // RESPONSIVIDADE
            // =====================================================

            pnlHeader.Resize += (s, e) =>
            {
                btnNovo.Location = new Point(pnlHeader.ClientSize.Width - btnNovo.Width, 5);
                btnEditar.Location = new Point(pnlHeader.ClientSize.Width - btnNovo.Width - btnEditar.Width - 10, 5);
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
                Width = 400,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            btnBuscar = CriarBotao("Buscar", Color.FromArgb(0, 120, 215), Color.White);
            btnBuscar.Width = 80;
            btnBuscar.Height = 32;
            btnBuscar.Location = new Point(490, 10);
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

            dgvProdutos = new DataGridView
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

            // =====================================================
            // HEADER
            // =====================================================

            dgvProdutos.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 246, 248),
                ForeColor = Color.FromArgb(50, 50, 50),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            // =====================================================
            // CÉLULAS
            // =====================================================

            dgvProdutos.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 50, 50),
                SelectionBackColor = Color.FromArgb(230, 240, 250),
                SelectionForeColor = Color.FromArgb(30, 30, 30),
                Padding = new Padding(5)
            };

            dgvProdutos.RowTemplate.Height = 40;

            // =====================================================
            // PRODUTO
            // =====================================================

            dgvProdutos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Nome",
                    HeaderText = "Produto",
                    DataPropertyName = "Nome",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 25
                }
            );

            // =====================================================
            // SKU
            // =====================================================

            dgvProdutos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "SKU",
                    HeaderText = "SKU",
                    DataPropertyName = "SKU",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15
                }
            );

            // =====================================================
            // CATEGORIA
            // =====================================================

            dgvProdutos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Categoria",
                    HeaderText = "Categoria",
                    DataPropertyName = "CategoriaNome",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 20
                }
            );

            // =====================================================
            // ESTOQUE
            // =====================================================

            dgvProdutos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Estoque",
                    HeaderText = "Estoque",
                    DataPropertyName = "EstoqueAtual",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "N2"
                    }
                }
            );

            // =====================================================
            // PREÇO
            // =====================================================

            dgvProdutos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Preco",
                    HeaderText = "Preço",
                    DataPropertyName = "Preco",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "C2"
                    }
                }
            );

            // =====================================================
            // STATUS
            // =====================================================

            dgvProdutos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Status",
                    HeaderText = "Status",
                    DataPropertyName = "Ativo",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 10
                }
            );
            dgvProdutos.CellFormatting += DgvProdutos_CellFormatting;

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
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0)
            };

            btnAnterior = CriarBotaoPagina("‹");
            btnProximo = CriarBotaoPagina("›");
            btnAnterior.Click += BtnAnterior_Click;
            btnProximo.Click += BtnProximo_Click;
            pnlBotoesPaginacao.Controls.Add(btnAnterior);
            pnlBotoesPaginacao.Controls.Add(btnProximo);
            pnlPaginacao.Controls.Add(pnlBotoesPaginacao);

            // =====================================================
            // CONTROLES
            // =====================================================

            Controls.Add(dgvProdutos);

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
            int quantidadeTotal = produtosFiltrados.Count;
            totalPaginas = (int)Math.Ceiling((double)quantidadeTotal / itensPorPagina);

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (paginaAtual > totalPaginas)
                paginaAtual = totalPaginas;

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            List<Produto> produtosPagina = produtosFiltrados
                .Skip(indiceInicial)
                .Take(itensPorPagina)
                .ToList();

            dgvProdutos.DataSource = null;
            dgvProdutos.DataSource = produtosPagina;

            AtualizarPaginacao();
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

                produtosFiltrados = DadosMock.Produtos
                    .Where(x =>
                        x.Nome.ToLower().Contains(pesquisa) ||
                        x.SKU.ToLower().Contains(pesquisa) ||
                        x.CategoriaNome.ToLower().Contains(pesquisa)
                    )
                    .ToList();
            }

            paginaAtual = 1;
            CarregarPagina();
        }

        // =========================================================
        // NOVO
        // =========================================================

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            using (FormProduto form = new FormProduto())
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                int novoId = DadosMock.Produtos.Count == 0 ? 1 : DadosMock.Produtos.Max(x => x.Id) + 1;

                Produto produto = new Produto
                {
                    Id = novoId,
                    Nome = form.Nome,
                    SKU = form.SKU,
                    CategoriaId = form.CategoriaId,
                    CategoriaNome = form.CategoriaNome,
                    EstoqueAtual = form.EstoqueAtual,
                    Preco = form.Preco,
                    Ativo = form.Ativo,
                    DataCadastro = DateTime.Now
                };

                DadosMock.Produtos.Add(produto);
                Pesquisar();
            }
        }

        // =========================================================
        // EDITAR
        // =========================================================

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um produto.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Produto produto = dgvProdutos.CurrentRow.DataBoundItem as Produto;

            if (produto == null)
                return;

            using (FormProduto form = new FormProduto(produto))
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                produto.Nome = form.Nome;
                produto.SKU = form.SKU;
                produto.CategoriaId = form.CategoriaId;
                produto.CategoriaNome = form.CategoriaNome;
                produto.EstoqueAtual = form.EstoqueAtual;
                produto.Preco = form.Preco;
                produto.Ativo = form.Ativo;

                Pesquisar();
            }
        }

        // =========================================================
        // STATUS
        // =========================================================

        private void DgvProdutos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvProdutos.Columns[e.ColumnIndex].Name != "Status")
                return;

            if (e.Value == null)
                return;

            bool ativo = Convert.ToBoolean(e.Value);

            e.Value = ativo ? "Ativo" : "Inativo";

            e.CellStyle.ForeColor = ativo
                ? Color.FromArgb(25, 135, 84)
                : Color.FromArgb(220, 53, 69);

            e.CellStyle.Font = new Font(dgvProdutos.Font, FontStyle.Bold);
        }

        // =========================================================
        // PESQUISA - EVENTOS
        // =========================================================

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void TxtPesquisar_KeyDown(object sender, KeyEventArgs e)
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

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaAtual <= 1)
                return;

            paginaAtual--;
            CarregarPagina();
        }

        private void BtnProximo_Click(object sender, EventArgs e)
        {
            if (paginaAtual >= totalPaginas)
                return;

            paginaAtual++;
            CarregarPagina();
        }

        private void AtualizarPaginacao()
        {
            pnlBotoesPaginacao.Controls.Clear();

            btnAnterior.Enabled = paginaAtual > 1;
            pnlBotoesPaginacao.Controls.Add(btnAnterior);

            for (int i = 1; i <= totalPaginas; i++)
            {
                Button btnPagina = CriarBotaoPagina(i.ToString());
                int pagina = i;

                btnPagina.Click += (s, e) =>
                {
                    paginaAtual = pagina;
                    CarregarPagina();
                };

                if (pagina == paginaAtual)
                {
                    btnPagina.BackColor = Color.FromArgb(0, 120, 215);
                    btnPagina.ForeColor = Color.White;
                    btnPagina.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
                }

                pnlBotoesPaginacao.Controls.Add(btnPagina);
            }

            btnProximo.Enabled = paginaAtual < totalPaginas;
            pnlBotoesPaginacao.Controls.Add(btnProximo);
        }

        // =========================================================
        // BOTÕES
        // =========================================================

        private Button CriarBotao(string texto, Color background, Color foreground)
        {
            Button btn = new Button
            {
                Text = texto,
                BackColor = background,
                ForeColor = foreground,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = background;
            btn.FlatAppearance.BorderSize = 1;
            return btn;
        }

        private Button CriarBotaoPagina(string texto)
        {
            Button btn = new Button
            {
                Text = texto,
                Width = 36,
                Height = 32,
                Margin = new Padding(3, 0, 3, 0),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
            btn.FlatAppearance.BorderSize = 1;
            return btn;
        }
    }
}