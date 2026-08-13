
using ERP_Oficina.Forms;

namespace ERP_Oficina.Controls
{
    public class OrdensServicoControl : UserControl
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

        private Label lblStatus;
        private ComboBox cmbStatus;

        private Button btnBuscar;

        private DataGridView dgvOrdens;

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

        private List<OrdemServico> todasOrdens = new List<OrdemServico>();
        private List<OrdemServico> ordensFiltradas = new List<OrdemServico>();

        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public OrdensServicoControl()
        {
            InitializeComponent();

            CarregarDados();

            ordensFiltradas =
                todasOrdens.ToList();

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

            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White };

            lblTitulo = new Label
            {
                Text = "Ordens de Serviço",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(0, 10)
            };

            btnEditar = CriarBotao("Editar", Color.White, Color.FromArgb(70, 70, 70));

            btnEditar.Width = 90;
            btnEditar.Height = 35;
            btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditar.Click += BtnEditar_Click;

            btnNovo = CriarBotao("Novo", Color.FromArgb(0, 120, 215), Color.White);

            btnNovo.Width = 90;
            btnNovo.Height = 35;
            btnNovo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNovo.Click += BtnNovo_Click;

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

            pnlPesquisa = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White };

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
                Width = 300
            };

            lblStatus = new Label
            {
                Text = "Status:",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = true,
                Location = new Point(395, 14)
            };

            cmbStatus = new ComboBox
            {
                Location = new Point(445, 10),
                Width = 150,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbStatus.Items.AddRange(
                new object[]
                {
                    "Todos",
                    "Aberta",
                    "Em andamento",
                    "Aguardando",
                    "Concluída",
                    "Cancelada"
                }
            );

            cmbStatus.SelectedIndex = 0;

            btnBuscar = CriarBotao("Buscar", Color.FromArgb(0, 120, 215), Color.White);

            btnBuscar.Width = 80;
            btnBuscar.Height = 32;
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Click += BtnBuscar_Click;

            pnlPesquisa.Resize += (s, e) =>
            {
                btnBuscar.Location = new Point(pnlPesquisa.ClientSize.Width - btnBuscar.Width, 10);
            };

            txtPesquisar.KeyDown += TxtPesquisar_KeyDown;
            pnlPesquisa.Controls.Add(lblPesquisar);
            pnlPesquisa.Controls.Add(txtPesquisar);
            pnlPesquisa.Controls.Add(lblStatus);
            pnlPesquisa.Controls.Add(cmbStatus);
            pnlPesquisa.Controls.Add(btnBuscar);

            // =====================================================
            // GRID
            // =====================================================

            dgvOrdens = new DataGridView
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

            dgvOrdens.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 246, 248),
                ForeColor = Color.FromArgb(50, 50, 50),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            dgvOrdens.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 50, 50),
                SelectionBackColor = Color.FromArgb(230, 240, 250),
                SelectionForeColor = Color.FromArgb(30, 30, 30),
                Padding = new Padding(5)
            };

            dgvOrdens.RowTemplate.Height = 40;

            // =====================================================
            // COLUNAS
            // =====================================================

            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "OS",
                DataPropertyName = "Id",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 8
            });

            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "ClienteNome",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 22
            });

            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Equipamento",
                HeaderText = "Equipamento",
                DataPropertyName = "EquipamentoNome",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 20
            });

            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Responsavel",
                HeaderText = "Responsável",
                DataPropertyName = "ResponsavelNome",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 18
            });

            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DataAbertura",
                HeaderText = "Abertura",
                DataPropertyName = "DataAbertura",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15
            });

            dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorTotal",
                HeaderText = "Valor",
                DataPropertyName = "ValorTotal",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvOrdens.CellFormatting += DgvOrdens_CellFormatting;

            // =====================================================
            // PAGINAÇÃO
            // =====================================================

            pnlPaginacao = new Panel { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.White };

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

            dgvOrdens.CellDoubleClick += DgvOrdens_CellDoubleClick;

            // =====================================================
            // CONTROLES
            // =====================================================

            Controls.Add(dgvOrdens);
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
            todasOrdens = DadosMock.OrdensServico.ToList();
        }

        // =========================================================
        // CARREGAR PÁGINA
        // =========================================================

        private void CarregarPagina()
        {
            int quantidadeTotal = ordensFiltradas.Count;
            totalPaginas = (int)Math.Ceiling((double)quantidadeTotal / itensPorPagina);

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (paginaAtual > totalPaginas)
                paginaAtual = totalPaginas;

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;
            List<OrdemServico> pagina = ordensFiltradas.Skip(indiceInicial).Take(itensPorPagina).ToList();
            dgvOrdens.DataSource = null;
            dgvOrdens.DataSource = pagina;

            AtualizarPaginacao();
        }

        // =========================================================
        // PAGINAÇÃO
        // =========================================================

        private void AtualizarPaginacao()
        {
            pnlBotoesPaginacao.Controls.Clear();
            btnAnterior.Enabled = paginaAtual > 1;

            pnlBotoesPaginacao.Controls.Add(
                btnAnterior
            );

            for (int i = 1; i <= totalPaginas; i++)
            {
                Button btnPagina = CriarBotaoPagina(i.ToString());
                int pagina = i;
                btnPagina.Click += (s, e) => { paginaAtual = pagina; CarregarPagina(); };

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
        // PESQUISA
        // =========================================================

        private void Pesquisar()
        {
            string pesquisa = txtPesquisar.Text.Trim();
            string status = cmbStatus.SelectedItem?.ToString();
            IEnumerable<OrdemServico> consulta = todasOrdens;

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = pesquisa.ToLower();
                consulta = consulta.Where(x =>
                        x.Id.ToString().Contains(pesquisa)
                    || (x.ClienteNome ?? "").ToLower().Contains(pesquisa)
                    || (x.EquipamentoNome ?? "").ToLower().Contains(pesquisa)
                    || (x.Status ?? "").ToLower().Contains(pesquisa)
                );
            }

            if (!string.IsNullOrWhiteSpace(status) && status != "Todos")
            {
                consulta = consulta.Where(x => x.Status == status);
            }

            ordensFiltradas = consulta.ToList();
            paginaAtual = 1;
            CarregarPagina();
        }

        // =========================================================
        // EVENTOS
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

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaAtual <= 1) return;
            paginaAtual--;
            CarregarPagina();
        }

        private void BtnProximo_Click(object sender, EventArgs e)
        {
            if (paginaAtual >= totalPaginas) return;
            paginaAtual++;
            CarregarPagina();
        }

        // =========================================================
        // NOVA OS
        // =========================================================

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            using (FormOrdemServico form = new FormOrdemServico())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CarregarDados();
                    ordensFiltradas = todasOrdens.ToList();
                    paginaAtual = 1;
                    CarregarPagina();
                }
            }
        }

        // =========================================================
        // EDITAR OS
        // =========================================================

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvOrdens.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma ordem de serviço.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OrdemServico ordem = dgvOrdens.CurrentRow.DataBoundItem as OrdemServico;
            if (ordem == null) return;

            using (FormOrdemServico form = new FormOrdemServico(ordem))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CarregarDados();
                    ordensFiltradas = todasOrdens.ToList();
                    CarregarPagina();
                }
            }
        }

        // =========================================================
        // FORMATAÇÃO DO STATUS
        // =========================================================
        private void DgvOrdens_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            OrdemServico ordem = dgvOrdens.Rows[e.RowIndex].DataBoundItem as OrdemServico;
            if (ordem == null) return;

            using (FormDetalhesOrdemServico form = new FormDetalhesOrdemServico(ordem))
            {
                form.ShowDialog();
            }
        }
        private void DgvOrdens_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvOrdens.Columns[e.ColumnIndex].Name != "Status") return;
            if (e.Value == null) return;

            string status = e.Value.ToString();
            Color cor = Color.FromArgb(70, 70, 70);

            if (status == "Aberta") cor = Color.FromArgb(0, 120, 215);
            else if (status == "Em andamento") cor = Color.FromArgb(230, 126, 34);
            else if (status == "Aguardando") cor = Color.FromArgb(111, 78, 155);
            else if (status == "Concluída") cor = Color.FromArgb(25, 135, 84);
            else if (status == "Cancelada") cor = Color.FromArgb(220, 53, 69);

            e.CellStyle.ForeColor = cor;
            e.CellStyle.Font = new Font(dgvOrdens.Font, FontStyle.Bold);
        }

        // =========================================================
        // BOTÕES
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
                FlatAppearance = { BorderColor = Color.FromArgb(220, 220, 220), BorderSize = 1 }
            };
        }
    }
}