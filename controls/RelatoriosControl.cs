
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
            GerarRelatorioOrdensServico();
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

            btnGerar = CriarBotao("Gerar", Color.FromArgb(0, 120, 215), Color.White);

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

            lblTotalRegistros = CriarResumoLabel("Registros: 0", 20);

            lblTotalServicos = CriarResumoLabel("Serviços: R$ 0,00", 200);

            lblTotalMateriais = CriarResumoLabel("Materiais: R$ 0,00", 400);

            lblValorTotal = CriarResumoLabel("Total: R$ 0,00", 600);

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
            dtpDataInicial.Value = DateTime.Today.AddMonths(-1);
            dtpDataFinal.Value = DateTime.Today;
        }

        private void BtnGerar_Click(object sender, EventArgs e)
        {
            if (dtpDataInicial.Value.Date > dtpDataFinal.Value.Date)
            {
                MessageBox.Show("A data inicial não pode ser maior que a data final.", "Período inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

            List<OrdemServico> ordens = DadosMock.OrdensServico.Where(x => x.DataAbertura >= inicio && x.DataAbertura < fim).ToList();

            var resultado = ordens
                .Select(ordem => new
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
                })
                .ToList();

            dgvRelatorio.DataSource = null;
            dgvRelatorio.DataSource = resultado;

            ConfigurarFormatoColuna("Servicos", "C2");
            ConfigurarFormatoColuna("Materiais", "C2");
            ConfigurarFormatoColuna("Total", "C2");
            ConfigurarFormatoColuna("Abertura", "dd/MM/yyyy");

            decimal totalServicos = ordens.Sum(x => x.ValorServicos);

            decimal totalMateriais = ordens.Sum(x => x.ValorMateriais);

            decimal total = ordens.Sum(x => x.ValorTotal);

            AtualizarResumo("Registros", ordens.Count, "Serviços", totalServicos, "Materiais", totalMateriais, "Total", total);
        }

        private void GerarRelatorioFaturamento()
        {
            DateTime inicio = dtpDataInicial.Value.Date;
            DateTime fim = dtpDataFinal.Value.Date.AddDays(1);

            List<OrdemServico> ordens = DadosMock.OrdensServico.Where(x => x.DataAbertura >= inicio && x.DataAbertura < fim).ToList();

            var resultado = ordens
                .Select(ordem => new
                {
                    OS = ordem.Id,
                    Cliente = ordem.ClienteNome,
                    Data = ordem.DataAbertura,
                    Status = ordem.Status,
                    Servicos = ordem.ValorServicos,
                    Materiais = ordem.ValorMateriais,
                    Total = ordem.ValorTotal
                })
                .OrderByDescending(x => x.Data)
                .ToList();

            dgvRelatorio.DataSource = null;
            dgvRelatorio.DataSource = resultado;

            ConfigurarFormatoColuna("Data", "dd/MM/yyyy");
            ConfigurarFormatoColuna("Servicos", "C2");
            ConfigurarFormatoColuna("Materiais", "C2");
            ConfigurarFormatoColuna("Total", "C2");

            decimal totalServicos = ordens.Sum(x => x.ValorServicos);

            decimal totalMateriais = ordens.Sum(x => x.ValorMateriais);

            decimal total = ordens.Sum(x => x.ValorTotal);

            AtualizarResumo("OS", ordens.Count, "Serviços", totalServicos, "Materiais", totalMateriais, "Total", total);
        }

        private void GerarRelatorioEstoque()
        {
            DateTime inicio = dtpDataInicial.Value.Date;
            DateTime fim = dtpDataFinal.Value.Date.AddDays(1);

            List<MovimentacaoEstoque> movimentacoes = DadosMock.MovimentacoesEstoque.Where(x => x.DataMovimento >= inicio && x.DataMovimento < fim).ToList();

            var resultado = movimentacoes
                    .GroupBy(x => x.ProdutoId)
                    .Select(grupo =>
                    {
                        Produto produto = DadosMock.Produtos.FirstOrDefault(x => x.Id == grupo.Key);

                        decimal entradas = grupo.Where(x => x.TipoMovimento == "Entrada").Sum(x => x.Quantidade);

                        decimal saidas = grupo.Where(x => x.TipoMovimento == "Saída").Sum(x => x.Quantidade);

                        return new
                        {
                            Produto = produto?.Nome ?? $"Produto #{grupo.Key}",
                            EstoqueAtual = produto?.EstoqueAtual ?? 0,
                            Entradas = entradas,
                            Saidas = saidas,
                            Movimentacoes = grupo.Count()
                        };
                    })
                    .OrderBy(x => x.Produto)
                    .ToList();

            dgvRelatorio.DataSource = null;
            dgvRelatorio.DataSource = resultado;

            ConfigurarFormatoColuna("EstoqueAtual", "N2");
            ConfigurarFormatoColuna("Entradas", "N2");
            ConfigurarFormatoColuna("Saidas", "N2");

            decimal totalEntradas = movimentacoes.Where(x => x.TipoMovimento == "Entrada").Sum(x => x.Quantidade);

            decimal totalSaidas = movimentacoes.Where(x => x.TipoMovimento == "Saída").Sum(x => x.Quantidade);

            AtualizarResumo("Produtos", resultado.Count, "Entradas", totalEntradas, "Saídas", totalSaidas, "Movimentações", movimentacoes.Count);
        }

        private void GerarRelatorioServicos()
        {
            DateTime inicio = dtpDataInicial.Value.Date;
            DateTime fim = dtpDataFinal.Value.Date.AddDays(1);

            // Primeiro identifica as OS do período.
            HashSet<int> idsOrdens = DadosMock.OrdensServico.Where(x => x.DataAbertura >= inicio && x.DataAbertura < fim).Select(x => x.Id).ToHashSet();

            var servicos = DadosMock.OrdensServicoServicos.Where(x => idsOrdens.Contains(x.OrdemServicoId)).ToList();

            var resultado = servicos
                    .GroupBy(x => x.ServicoId)
                    .Select(grupo =>
                    {
                        Servico servico = DadosMock.Servicos.FirstOrDefault(x => x.Id == grupo.Key);

                        return new
                        {
                            Servico = servico?.Nome
                                ?? grupo.First().ServicoNome
                                ?? $"Serviço #{grupo.Key}",

                            Quantidade =
                                grupo.Sum(x => x.Quantidade),

                            ValorMedio =
                                grupo.Any()
                                    ? grupo.Average(x => x.PrecoUnitario)
                                    : 0,

                            Total =
                                grupo.Sum(x => x.Subtotal)
                        };
                    })
                    .OrderByDescending(x => x.Total)
                    .ToList();

            dgvRelatorio.DataSource = null;
            dgvRelatorio.DataSource = resultado;

            ConfigurarFormatoColuna("Quantidade", "N2");
            ConfigurarFormatoColuna("ValorMedio", "C2");
            ConfigurarFormatoColuna("Total", "C2");

            decimal quantidadeTotal = servicos.Sum(x => x.Quantidade);

            decimal valorTotal = servicos.Sum(x => x.Subtotal);

            decimal valorMedio = servicos.Any() ? servicos.Average(x => x.PrecoUnitario) : 0;

            AtualizarResumo("Serviços", resultado.Count, "Quantidade", quantidadeTotal, "Valor médio", valorMedio, "Total", valorTotal);
        }

        private void AtualizarResumo(
            string titulo1,
            object valor1,
            string titulo2,
            object valor2,
            string titulo3,
            object valor3,
            string titulo4,
            object valor4)
        {
            lblTotalRegistros.Text = $"{titulo1}: {valor1}";

            lblTotalServicos.Text = $"{titulo2}: {FormatarResumo(valor2)}";

            lblTotalMateriais.Text = $"{titulo3}: {FormatarResumo(valor3)}";

            lblValorTotal.Text = $"{titulo4}: {FormatarResumo(valor4)}";
        }

        private string FormatarResumo(object valor)
        {
            if (valor is decimal decimalValue)
                return decimalValue.ToString("C2");

            if (valor is double doubleValue)
                return doubleValue.ToString("N2");

            if (valor is int intValue)
                return intValue.ToString("N0");

            return valor?.ToString() ?? "0";
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

                SelectionMode = DataGridViewSelectionMode.FullRowSelect,

                MultiSelect = false,
                RowHeadersVisible = false,

                EnableHeadersVisualStyles = false,

                ColumnHeadersHeight = 40,

                GridColor = Color.FromArgb(230, 230, 230),

                Font = new Font("Segoe UI", 10F)
            };

            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 246, 248),

                ForeColor = Color.FromArgb(50, 50, 50),

                Font = new Font("Segoe UI", 10F, FontStyle.Bold),

                Padding = new Padding(5)
            };

            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,

                ForeColor = Color.FromArgb(50, 50, 50),

                SelectionBackColor = Color.FromArgb(230, 240, 250),

                SelectionForeColor = Color.FromArgb(30, 30, 30),

                Padding = new Padding(5)
            };

            grid.RowTemplate.Height = 40;

            return grid;
        }

        private void ConfigurarFormatoColuna(string nome, string formato)
        {
            if (dgvRelatorio.Columns.Contains(nome))
            {
                dgvRelatorio.Columns[nome].DefaultCellStyle.Format = formato;
            }
        }

        private Label CriarLabel(string texto, int x, int y)
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

        private Label CriarResumoLabel(string texto, int x)
        {
            return new Label
            {
                Text = texto,

                Font = new Font("Segoe UI", 10F, FontStyle.Bold),

                ForeColor = Color.FromArgb(50, 50, 50),

                AutoSize = true,

                Location = new Point(x, 28)
            };
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
    }
}
