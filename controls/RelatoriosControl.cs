
namespace ERP_Oficina.Controls
{
    public class RelatoriosControl : UserControl
    {
        private Panel pnlHeader;
        private Label lblTitulo;

        private Panel pnlFiltros;

        private Label lblTipo;
        private ComboBox cmbTipo;

        private Label lblDataInicial;
        private DateTimePicker dtpDataInicial;

        private Label lblDataFinal;
        private DateTimePicker dtpDataFinal;

        private Button btnGerar;

        private Panel pnlResumo;

        private Label lblTotalRegistros;
        private Label lblTotalServicos;
        private Label lblTotalMateriais;
        private Label lblValorTotal;

        private DataGridView dgvRelatorio;

        public RelatoriosControl()
        {
            InitializeComponent();
            ConfigurarFiltros();
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
                Text = "Relatórios",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(0, 10)
            };

            pnlHeader.Controls.Add(lblTitulo);

            pnlFiltros = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.White
            };

            lblTipo = CriarLabel("Relatório:", 0, 14);

            cmbTipo = new ComboBox
            {
                Location = new Point(75, 10),
                Width = 180,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbTipo.Items.AddRange(new object[]
            {
                "Ordens de Serviço",
                "Faturamento",
                "Estoque",
                "Serviços"
            });

            cmbTipo.SelectedIndex = 0;

            lblDataInicial = CriarLabel("De:", 280, 14);

            dtpDataInicial = new DateTimePicker
            {
                Location = new Point(315, 10),
                Width = 120,
                Format = DateTimePickerFormat.Short
            };

            lblDataFinal = CriarLabel("Até:", 450, 14);

            dtpDataFinal = new DateTimePicker
            {
                Location = new Point(490, 10),
                Width = 120,
                Format = DateTimePickerFormat.Short
            };

            btnGerar = CriarBotao(
                "Gerar",
                Color.FromArgb(0, 120, 215),
                Color.White);

            btnGerar.Width = 90;
            btnGerar.Height = 32;
            btnGerar.Location = new Point(630, 10);
            btnGerar.Click += BtnGerar_Click;

            pnlFiltros.Controls.Add(lblTipo);
            pnlFiltros.Controls.Add(cmbTipo);
            pnlFiltros.Controls.Add(lblDataInicial);
            pnlFiltros.Controls.Add(dtpDataInicial);
            pnlFiltros.Controls.Add(lblDataFinal);
            pnlFiltros.Controls.Add(dtpDataFinal);
            pnlFiltros.Controls.Add(btnGerar);

            pnlResumo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            lblTotalRegistros = CriarResumoLabel(
                "Registros: 0",
                20);

            lblTotalServicos = CriarResumoLabel(
                "Serviços: R$ 0,00",
                200);

            lblTotalMateriais = CriarResumoLabel(
                "Materiais: R$ 0,00",
                400);

            lblValorTotal = CriarResumoLabel(
                "Total: R$ 0,00",
                600);

            pnlResumo.Controls.Add(lblTotalRegistros);
            pnlResumo.Controls.Add(lblTotalServicos);
            pnlResumo.Controls.Add(lblTotalMateriais);
            pnlResumo.Controls.Add(lblValorTotal);

            dgvRelatorio = CriarGrid();

            Controls.Add(dgvRelatorio);
            Controls.Add(pnlResumo);
            Controls.Add(pnlFiltros);
            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }

        private void ConfigurarFiltros()
        {
            if (DadosMock.OrdensServico.Any())
            {
                dtpDataInicial.Value =
                    DadosMock.OrdensServico.Min(x => x.DataAbertura).Date;

                dtpDataFinal.Value =
                    DadosMock.OrdensServico.Max(x => x.DataAbertura).Date;
            }
            else
            {
                dtpDataInicial.Value = DateTime.Today.AddMonths(-1);
                dtpDataFinal.Value = DateTime.Today;
            }
        }

        private void BtnGerar_Click(object sender, EventArgs e)
        {
            if (dtpDataInicial.Value.Date > dtpDataFinal.Value.Date)
            {
                MessageBox.Show(
                    "A data inicial não pode ser maior que a data final.",
                    "Período inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            switch (cmbTipo.SelectedItem?.ToString())
            {
                case "Ordens de Serviço":
                    GerarRelatorioOrdensServico();
                    break;

                case "Faturamento":
                    GerarRelatorioFaturamento();
                    break;

                case "Estoque":
                    GerarRelatorioEstoque();
                    break;

                case "Serviços":
                    GerarRelatorioServicos();
                    break;
            }
        }

        private void GerarRelatorioOrdensServico()
        {
            DateTime inicio = dtpDataInicial.Value.Date;
            DateTime fim = dtpDataFinal.Value.Date.AddDays(1);

            List<OrdemServico> ordens =
                DadosMock.OrdensServico
                    .Where(x =>
                        x.DataAbertura >= inicio &&
                        x.DataAbertura < fim)
                    .ToList();

            var resultado = ordens.Select(ordem => new
            {
                OS = ordem.Id,
                Cliente = ordem.ClienteNome,
                Equipamento = ordem.EquipamentoNome,
                Responsavel = ordem.ResponsavelNome,
                Abertura = ordem.DataAbertura,
                Status = ordem.Status,
                Servicos = ordem.ValorServicos,
                Materiais = ordem.ValorMateriais,
                Total = ordem.ValorTotal
            }).ToList();

            dgvRelatorio.DataSource = null;
            dgvRelatorio.DataSource = resultado;

            dgvRelatorio.Columns["Servicos"]
                .DefaultCellStyle.Format = "C2";

            dgvRelatorio.Columns["Materiais"]
                .DefaultCellStyle.Format = "C2";

            dgvRelatorio.Columns["Total"]
                .DefaultCellStyle.Format = "C2";

            dgvRelatorio.Columns["Abertura"]
                .DefaultCellStyle.Format = "dd/MM/yyyy";

            decimal totalServicos =
                ordens.Sum(x => x.ValorServicos);

            decimal totalMateriais =
                ordens.Sum(x => x.ValorMateriais);

            decimal total =
                ordens.Sum(x => x.ValorTotal);

            lblTotalRegistros.Text =
                $"Registros: {ordens.Count}";

            lblTotalServicos.Text =
                $"Serviços: {totalServicos:C2}";

            lblTotalMateriais.Text =
                $"Materiais: {totalMateriais:C2}";

            lblValorTotal.Text =
                $"Total: {total:C2}";
        }

        private void GerarRelatorioFaturamento()
        {
            MessageBox.Show(
                "Relatório de faturamento será implementado nesta etapa.",
                "Relatórios",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void GerarRelatorioEstoque()
        {
            MessageBox.Show(
                "Relatório de estoque será implementado nesta etapa.",
                "Relatórios",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void GerarRelatorioServicos()
        {
            MessageBox.Show(
                "Relatório de serviços será implementado nesta etapa.",
                "Relatórios",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private DataGridView CriarGrid()
        {
            DataGridView grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,

                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,

                AutoGenerateColumns = true,

                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,

                MultiSelect = false,
                RowHeadersVisible = false,

                EnableHeadersVisualStyles = false,

                ColumnHeadersHeight = 40,

                GridColor = Color.FromArgb(230, 230, 230),

                Font = new Font("Segoe UI", 10F)
            };

            grid.ColumnHeadersDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(245, 246, 248),
                    ForeColor = Color.FromArgb(50, 50, 50),
                    Font = new Font(
                        "Segoe UI",
                        10F,
                        FontStyle.Bold),

                    Padding = new Padding(5)
                };

            grid.DefaultCellStyle =
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

            grid.RowTemplate.Height = 40;

            return grid;
        }

        private Label CriarLabel(
            string texto,
            int x,
            int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private Label CriarResumoLabel(
            string texto,
            int x)
        {
            return new Label
            {
                Text = texto,
                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),

                ForeColor =
                    Color.FromArgb(50, 50, 50),

                AutoSize = true,
                Location = new Point(x, 28)
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
    }
}