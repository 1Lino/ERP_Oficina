
using ERP_Oficina.Models.Permissao;
using ERP_Oficina.Services.Autorizacao;

namespace ERP_Oficina.Controls
{
    public class ClientesControl : UserControl
    {
        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnNovo;
        private Button btnEditar;
        private Panel pnlPesquisa;
        private Label lblPesquisar;
        private TextBox txtPesquisar;
        private Button btnBuscar;

        private DataGridView dgvClientes;

        private Panel pnlPaginacao;
        private FlowLayoutPanel pnlBotoesPaginacao;
        private Button btnAnterior;
        private Button btnProximo;

        // PAGINAÇÃO / DADOS

        private int paginaAtual = 1;

        private int itensPorPagina = 5;

        private int totalPaginas = 1;

        private List<Cliente> clientesFiltrados = new List<Cliente>();

        // CONSTRUTOR

        public ClientesControl()
        {
            InitializeComponent();
            clientesFiltrados = DadosMock.Clientes.ToList();
            CarregarPagina();
        }

        private void InitializeComponent()
        {
            SuspendLayout(); // convém suspender a lógica padrão do layout para melhor customização.

            // USER CONTROL

            Dock = DockStyle.Fill;

            BackColor = Color.White;

            Padding = new Padding(20);

            // HEADER do painel

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };

            lblTitulo = new Label
            {
                Text = "Clientes",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(0, 10)
            };

            // BOTÃO EDITAR

            btnEditar = CriarBotao(
                "Editar",
                Color.White,
                Color.FromArgb(70, 70, 70)
            );

            btnEditar.Width = 90;
            btnEditar.Height = 35;
            btnEditar.Enabled = Autorizacao.TemPermissao(Permissao.EditarClientes);

            btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnEditar.Location = new Point(pnlHeader.Width - 190, 5);

            btnEditar.Click += BtnEditar_Click;

            btnNovo = CriarBotao(
                "Novo",
                Color.FromArgb(0, 120, 215),
                Color.White
            );

            btnNovo.Width = 90;
            btnNovo.Height = 35;

            btnNovo.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnNovo.Location = new Point(pnlHeader.Width - 90, 5);

            btnNovo.Click += BtnNovo_Click;

            // Pra manter responsividade do header:

            pnlHeader.Resize += (s, e) =>
            {
                btnNovo.Location = new Point(pnlHeader.ClientSize.Width - btnNovo.Width, 5);

                btnEditar.Location = new Point(pnlHeader.ClientSize.Width - btnNovo.Width - btnEditar.Width - 10, 5);
            };

            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnEditar);
            pnlHeader.Controls.Add(btnNovo);

            // PESQUISA

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

            btnBuscar = CriarBotao(
                "Buscar",
                Color.FromArgb(0, 120, 215),
                Color.White
            );

            btnBuscar.Width = 80;
            btnBuscar.Height = 32;

            btnBuscar.Location = new Point(490, 10);

            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnBuscar.Click += BtnBuscar_Click;

            // RESPONSIVIDADE DA PESQUISA

            pnlPesquisa.Resize += (s, e) =>
            {
                btnBuscar.Location = new Point(pnlPesquisa.ClientSize.Width - btnBuscar.Width, 10);
                txtPesquisar.Width = pnlPesquisa.ClientSize.Width - lblPesquisar.Width - btnBuscar.Width - 25;
            };

            // ENTER NO CAMPO DE PESQUISA

            txtPesquisar.KeyDown += TxtPesquisar_KeyDown;

            pnlPesquisa.Controls.Add(lblPesquisar);
            pnlPesquisa.Controls.Add(txtPesquisar);
            pnlPesquisa.Controls.Add(btnBuscar);

            // GRID

            dgvClientes = new DataGridView
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

            // HEADER DO GRID

            dgvClientes.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 246, 248),
                ForeColor = Color.FromArgb(50, 50, 50),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            // CÉLULAS

            dgvClientes.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 50, 50),
                SelectionBackColor = Color.FromArgb(230, 240, 250),
                SelectionForeColor = Color.FromArgb(30, 30, 30),
                Padding = new Padding(5)
            };

            dgvClientes.RowTemplate.Height = 40;

            dgvClientes.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Nome",
                    HeaderText = "Nome",
                    DataPropertyName = "Nome",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 30
                }
            );

            dgvClientes.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Cidade",
                    HeaderText = "Cidade",
                    DataPropertyName = "Cidade",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 25
                }
            );

            dgvClientes.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Telefone",
                    HeaderText = "Telefone",
                    DataPropertyName = "Telefone",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 25
                }
            );

            dgvClientes.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Status",

                    HeaderText = "Status",

                    DataPropertyName = "Status",

                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,

                    FillWeight = 20
                }
            );

            dgvClientes.CellFormatting += DgvClientes_CellFormatting;

            // PAGINAÇÃO

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

            Controls.Add(dgvClientes);
            Controls.Add(pnlPaginacao);
            Controls.Add(pnlPesquisa);
            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }

        private void CarregarPagina()
        {
            int quantidadeTotal = clientesFiltrados.Count;

            totalPaginas = (int)Math.Ceiling((double)quantidadeTotal / itensPorPagina);

            // Evita que totalPaginas = 0
            if (totalPaginas == 0)
                totalPaginas = 1;

            // Segurança pra caso a pesquisa reduza
            // o número de páginas.
            if (paginaAtual > totalPaginas)
                paginaAtual = totalPaginas;

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            List<Cliente> clientesPagina = clientesFiltrados.Skip(indiceInicial).Take(itensPorPagina).ToList();

            dgvClientes.DataSource = null;
            dgvClientes.DataSource = clientesPagina;

            AtualizarPaginacao();
        }

        private void AtualizarPaginacao()
        {
            // Remove somente os botões de página.
            // Os botões anterior/próximo permanecem.
            pnlBotoesPaginacao.Controls.Clear();

            btnAnterior.Enabled = paginaAtual > 1;

            pnlBotoesPaginacao.Controls.Add(btnAnterior);

            // BOTÕES DE PÁGINA

            for (int i = 1; i <= totalPaginas; i++)
            {
                Button btnPagina = CriarBotaoPagina(i.ToString());

                // Captura o valor atual.
                int pagina = i;

                btnPagina.Click += (s, e) =>
                {
                    paginaAtual = pagina;

                    CarregarPagina();
                };

                // Página atual
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

        private void Pesquisar()
        {
            string pesquisa = txtPesquisar.Text.Trim();

            if (string.IsNullOrWhiteSpace(pesquisa))
            {
                clientesFiltrados = DadosMock.Clientes.ToList();
            }
            else
            {
                pesquisa = pesquisa.ToLower();

                clientesFiltrados = DadosMock.Clientes.Where(
                    x => x.Nome.ToLower().Contains(pesquisa) ||
                    x.Cidade.ToLower().Contains(pesquisa) ||
                    x.Telefone.ToLower().Contains(pesquisa) ||
                    x.Status.ToLower().Contains(pesquisa))
                    .ToList();
            }

            // Sempre retorna para a primeira página depois de uma pesquisa.
            paginaAtual = 1;

            CarregarPagina();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        // Evento Enter
        private void TxtPesquisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                Pesquisar();
            }
        }

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
        private void BtnNovo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Abrir tela de novo cliente.",
                "Novo Cliente",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecione um cliente.",
                    "Editar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Cliente cliente = dgvClientes.CurrentRow.DataBoundItem as Cliente;

            if (cliente == null)
                return;

            MessageBox.Show(
                $"Editar cliente: {cliente.Nome}",
                "Editar Cliente",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void DgvClientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvClientes.Columns[e.ColumnIndex].Name != "Status")
            {
                return;
            }

            if (e.Value == null)
                return;

            string status = e.Value.ToString();

            if (status == "Ativo")
            {
                e.CellStyle.ForeColor = Color.FromArgb(25, 135, 84);

                e.CellStyle.Font = new Font(dgvClientes.Font, FontStyle.Bold);
            }
            else if (status == "Inativo")
            {
                e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);

                e.CellStyle.Font = new Font(dgvClientes.Font, FontStyle.Bold);
            }
        }

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
                FlatAppearance =
                {
                    BorderColor = background,
                    BorderSize = 1
                }
            };
        }

        private Button CriarBotaoPagina(string texto)
        {
            return new Button
            {
                Text = texto,
                Width = 36,
                Height = 32,
                Margin = new Padding(3, 0, 3, 0),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand,
                FlatAppearance =
                {
                    BorderColor = Color.FromArgb(220, 220, 220),
                    BorderSize = 1
                }
            };
        }

    }
}