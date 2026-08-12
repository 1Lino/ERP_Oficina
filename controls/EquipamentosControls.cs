
using System.Diagnostics.Tracing;

namespace ERP_Oficina.Controls
{
    public class EquipamentosControl : UserControl
    {
        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnNovo;
        private Button btnEditar;
        private Panel pnlPesquisa;
        private Label lblPesquisar;
        private TextBox txtPesquisar;
        private Button btnBuscar;

        private DataGridView dgvEquipamentos;

        private Panel pnlPaginacao;
        private FlowLayoutPanel pnlBotoesPaginacao;
        private Button btnAnterior;
        private Button btnProximo;

        // PAGINAÇÃO / DADOS

        private int paginaAtual = 1;
        private int itensPorPagina = 5;
        private int totalPaginas = 1;

        private List<Equipamento> equipamentosFiltrados = new List<Equipamento>();

        public EquipamentosControl()
        {
            InitializeComponent();
            equipamentosFiltrados = DadosMock.Equipamentos.ToList();
            // CarregarPagina();
            Load += (s, e) => CarregarPagina();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            Dock = DockStyle.Fill;
            BackColor = Color.White;
            Padding = new Padding(20);

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };

            lblTitulo = new Label
            {
                Text = "Equipamentos",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(0, 10)
            };

            btnEditar = CriarBotao(
                "Editar",
                Color.White,
                Color.FromArgb(70, 70, 70)
            );

            btnEditar.Width = 90;
            btnEditar.Height = 35;
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


            pnlHeader.Resize += (s, e) =>
            {
                btnNovo.Location = new Point(pnlHeader.ClientSize.Width - btnNovo.Width, 5);
                btnEditar.Location = new Point(pnlHeader.ClientSize.Width - btnNovo.Width - btnEditar.Width - 10, 5);
            };

            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnEditar);
            pnlHeader.Controls.Add(btnNovo);

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
                Width = 400
            };

            btnBuscar = CriarBotao("Buscar", Color.FromArgb(0, 120, 215), Color.White);

            btnBuscar.Width = 80;
            btnBuscar.Height = 32;
            btnBuscar.Location = new Point(490, 10);

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

            dgvEquipamentos = new DataGridView
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

            dgvEquipamentos.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 246, 248),
                ForeColor = Color.FromArgb(50, 50, 50),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            dgvEquipamentos.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 50, 50),
                SelectionBackColor = Color.FromArgb(230, 240, 250),
                SelectionForeColor = Color.FromArgb(30, 30, 30),
                Padding = new Padding(5)
            };
            dgvEquipamentos.RowTemplate.Height = 40;

            dgvEquipamentos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Cliente",
                    HeaderText = "Cliente",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 25
                }
            );

            dgvEquipamentos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Descricao",
                    HeaderText = "Descrição",
                    DataPropertyName = "Descricao",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 25
                }
            );

            dgvEquipamentos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Marca",
                    HeaderText = "Marca",
                    DataPropertyName = "Marca",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15
                }
            );

            dgvEquipamentos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Modelo",
                    HeaderText = "Modelo",
                    DataPropertyName = "Modelo",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 20
                }
            );

            dgvEquipamentos.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "NumeroSerie",
                    HeaderText = "Nº de Série",
                    DataPropertyName = "NumeroSerie",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    FillWeight = 15
                }
            );

            dgvEquipamentos.CellFormatting += DgvEquipamentos_CellFormatting;
            dgvEquipamentos.CellDoubleClick += DgvEquipamentos_CellDoubleClick;


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

            Controls.Add(dgvEquipamentos);
            Controls.Add(pnlPaginacao);
            Controls.Add(pnlPesquisa);
            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }


        private void CarregarPagina()
        {
            int quantidadeTotal = equipamentosFiltrados.Count;

            totalPaginas = (int)Math.Ceiling((double)quantidadeTotal / itensPorPagina);

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (paginaAtual > totalPaginas)
                paginaAtual = totalPaginas;

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            List<Equipamento> equipamentosPagina =
                equipamentosFiltrados
                    .Skip(indiceInicial)
                    .Take(itensPorPagina)
                    .ToList();

            dgvEquipamentos.DataSource = null;
            dgvEquipamentos.DataSource = equipamentosPagina;

            Console.WriteLine("Todos os comandos executados!");

            AtualizarPaginacao();
        }


        private string ObterNomeCliente(int clienteId)
        {
            Cliente cliente = DadosMock.Clientes.FirstOrDefault(x => x.Id == clienteId);

            if (cliente == null)
                return "Cliente não encontrado";

            Console.WriteLine($"Cliente encontrado: {cliente.Nome}");
            return cliente.Nome;
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

        private void Pesquisar()
        {
            string pesquisa = txtPesquisar.Text.Trim();

            if (string.IsNullOrWhiteSpace(pesquisa))
            {
                equipamentosFiltrados = DadosMock.Equipamentos.ToList();
            }
            else
            {
                pesquisa = pesquisa.ToLower();

                equipamentosFiltrados =
                    DadosMock.Equipamentos
                        .Where(x =>
                            ObterNomeCliente(x.ClienteId).ToLower().Contains(pesquisa) ||
                            x.Descricao.ToLower().Contains(pesquisa) ||
                            x.Marca.ToLower().Contains(pesquisa) ||
                            x.Modelo.ToLower().Contains(pesquisa) ||
                            x.NumeroSerie.ToLower().Contains(pesquisa)
                        ).ToList();
            }

            paginaAtual = 1;

            CarregarPagina();
        }

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
            using (FormEquipamento form = new FormEquipamento())
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                int novoId = DadosMock.Equipamentos.Count == 0 ?
                             1 : DadosMock.Equipamentos.Max(x => x.Id) + 1;

                Equipamento equipamento = new Equipamento
                {
                    Id = novoId,
                    ClienteId = form.ClienteId,
                    Descricao = form.Descricao,
                    Marca = form.Marca,
                    Modelo = form.Modelo,
                    NumeroSerie = form.NumeroSerie,
                    DataCadastro = DateTime.Now
                };

                DadosMock.Equipamentos.Add(equipamento);

                Pesquisar();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            EditarEquipamentoSelecionado();
        }

        private void DgvEquipamentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            EditarEquipamentoSelecionado();
        }

        private void EditarEquipamentoSelecionado()
        {
            if (dgvEquipamentos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecione um equipamento.",
                    "Editar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Captura o conteúdo da linha selecionada da tabela e converte num objeto Equipamento
            Equipamento equipamento = dgvEquipamentos.CurrentRow.DataBoundItem as Equipamento;

            if (equipamento == null)
                return;

            using (FormEquipamento form = new FormEquipamento(equipamento))
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                equipamento.ClienteId = form.ClienteId;
                equipamento.Descricao = form.Descricao;
                equipamento.Marca = form.Marca;
                equipamento.Modelo = form.Modelo;
                equipamento.NumeroSerie = form.NumeroSerie;

                Pesquisar();
            }
        }

        private void DgvEquipamentos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvEquipamentos.Columns[e.ColumnIndex].Name != "Cliente")
                return;

            Equipamento equipamento =
                dgvEquipamentos.Rows[e.RowIndex].DataBoundItem as Equipamento;

            if (equipamento == null)
                return;

            e.Value = ObterNomeCliente(equipamento.ClienteId);
            e.CellStyle.ForeColor = Color.FromArgb(50, 50, 50);
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
                    BorderColor = Color.FromArgb( 220, 220, 220),
                    BorderSize = 1
                }
            };
        }
    }

    public class FormEquipamento : Form
    {
        private ComboBox cboCliente;
        private TextBox txtDescricao;
        private TextBox txtMarca;
        private TextBox txtModelo;
        private TextBox txtNumeroSerie;
        private Button btnCancelar;
        private Button btnSalvar;

        public int ClienteId { get; private set; }
        public string ClienteNome { get; private set; }
        public string Descricao { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string NumeroSerie { get; private set; }

        public FormEquipamento()
        {
            InicializarFormulario();
            CarregarClientes();
        }

        public FormEquipamento(Equipamento equipamento)
        {
            InicializarFormulario();
            CarregarClientes();
            PreencherDados(equipamento);
        }

        private void InicializarFormulario()
        {
            Text = "Equipamento";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            Width = 520;
            Height = 430;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            Label lblTitulo = new Label
            {
                Text = "Dados do equipamento",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Controls.Add(lblTitulo);

            Label lblCliente = CriarLabel("Cliente", 30, 75);

            cboCliente = new ComboBox
            {
                Location = new Point(30, 100),
                Width = 445,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Controls.Add(lblCliente);
            Controls.Add(cboCliente);

            Label lblDescricao = CriarLabel("Descrição", 30, 140);

            txtDescricao = new TextBox
            {
                Location = new Point(30, 165),
                Width = 445,
                Height = 30
            };

            Controls.Add(lblDescricao);
            Controls.Add(txtDescricao);

            Label lblMarca = CriarLabel("Marca", 30, 205);

            txtMarca = new TextBox
            {
                Location = new Point(30, 230),
                Width = 210,
                Height = 30
            };

            Controls.Add(lblMarca);
            Controls.Add(txtMarca);

            Label lblModelo = CriarLabel("Modelo", 265, 205);

            txtModelo = new TextBox
            {
                Location = new Point(265, 230),
                Width = 210,
                Height = 30
            };

            Controls.Add(lblModelo);
            Controls.Add(txtModelo);

            Label lblNumeroSerie = CriarLabel("Número de série", 30, 270);

            txtNumeroSerie = new TextBox
            {
                Location = new Point(30, 295),
                Width = 445,
                Height = 30
            };

            Controls.Add(lblNumeroSerie);
            Controls.Add(txtNumeroSerie);

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 100,
                Height = 35,
                Location = new Point(265, 350),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(70, 70, 70),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);

            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;

                Close();
            };

            Controls.Add(btnCancelar);


            btnSalvar = new Button
            {
                Text = "Salvar",
                Width = 100,
                Height = 35,
                Location = new Point(375, 350),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,

                Cursor = Cursors.Hand
            };

            btnSalvar.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);

            btnSalvar.Click += BtnSalvar_Click;

            Controls.Add(btnSalvar);
            AcceptButton = btnSalvar;
            CancelButton = btnCancelar;
        }

        private void CarregarClientes()
        {
            cboCliente.DataSource = null;
            cboCliente.DisplayMember = "Nome";
            cboCliente.ValueMember = "Id";
            cboCliente.DataSource = DadosMock.Clientes.ToList();
        }


        private void PreencherDados(Equipamento equipamento)
        {
            if (equipamento == null)
                return;

            cboCliente.SelectedValue = equipamento.ClienteId;
            txtDescricao.Text = equipamento.Descricao;
            txtMarca.Text = equipamento.Marca;
            txtModelo.Text = equipamento.Modelo;
            txtNumeroSerie.Text = equipamento.NumeroSerie;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedItem == null)
            {
                MessageBox.Show(
                    "Selecione um cliente.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cboCliente.Focus();

                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show(
                    "Informe a descrição do equipamento.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtDescricao.Focus();

                return;
            }

            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                MessageBox.Show(
                    "Informe a marca do equipamento.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtMarca.Focus();

                return;
            }

            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show(
                    "Informe o modelo do equipamento.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtModelo.Focus();

                return;
            }

            Cliente cliente = cboCliente.SelectedItem as Cliente;

            if (cliente == null)
                return;

            ClienteId = cliente.Id;
            Descricao = txtDescricao.Text.Trim();
            Marca = txtMarca.Text.Trim();
            Modelo = txtModelo.Text.Trim();
            NumeroSerie = txtNumeroSerie.Text.Trim();
            DialogResult = DialogResult.OK;

            Close();
        }

        private Label CriarLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(x, y)
            };
        }
    }
}