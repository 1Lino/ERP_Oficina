
namespace ERP_Oficina.Controls
{
    public class DashboardControl : UserControl
    {
        private Panel pnlHeader;
        private Label lblTitulo;
        private Label lblSubtitulo;

        private FlowLayoutPanel pnlCards;

        private Panel cardOSAbertas;
        private Panel cardOSEmAndamento;
        private Panel cardOSAguardando;
        private Panel cardValorTotal;

        private Label lblOSAbertas;
        private Label lblOSAbertasValor;

        private Label lblOSEmAndamento;
        private Label lblOSEmAndamentoValor;

        private Label lblOSAguardando;
        private Label lblOSAguardandoValor;

        private Label lblValorTotal;
        private Label lblValorTotalValor;

        private Panel pnlConteudo;

        private Panel pnlOrdensRecentes;
        private Panel pnlEstoqueBaixo;

        private Label lblTituloOrdens;
        private Label lblTituloEstoque;

        private DataGridView dgvOrdensRecentes;
        private DataGridView dgvEstoqueBaixo;

        // TODO: esta variável é provisória, mas deverá haver uma propriedade de limite mínimo para cada item do estoque.
        private const decimal LIMITE_ESTOQUE_BAIXO = 5;

        public DashboardControl()
        {
            InicializarComponentes();
            CarregarDados();
        }

        private void InicializarComponentes()
        {
            SuspendLayout();

            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 246, 248);
            Padding = new Padding(25);

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(245, 246, 248)
            };

            lblTitulo = new Label
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(0, 5)
            };

            lblSubtitulo = new Label
            {
                Text = "Visão geral da assistência técnica",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(2, 40)
            };

            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(lblSubtitulo);

            // Cards do dashboard
            pnlCards = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 125,
                BackColor = Color.FromArgb(245, 246, 248),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 5),
                AutoScroll = false
            };

            cardOSAbertas = CriarCard(
                "OS Abertas",
                out lblOSAbertas,
                out lblOSAbertasValor
            );

            cardOSEmAndamento = CriarCard(
                "OS em andamento",
                out lblOSEmAndamento,
                out lblOSEmAndamentoValor
            );

            cardOSAguardando = CriarCard(
                "OS aguardando",
                out lblOSAguardando,
                out lblOSAguardandoValor
            );

            cardValorTotal = CriarCard(
                "Valor das OS",
                out lblValorTotal,
                out lblValorTotalValor
            );

            pnlCards.Controls.Add(cardOSAbertas);
            pnlCards.Controls.Add(cardOSEmAndamento);
            pnlCards.Controls.Add(cardOSAguardando);
            pnlCards.Controls.Add(cardValorTotal);

            // painel de conteúdo
            pnlConteudo = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 246, 248),
                Padding = new Padding(0, 15, 0, 0)
            };

            //Ordens recentes
            pnlOrdensRecentes = new Panel
            {
                Dock = DockStyle.Left,
                Width = 600,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            lblTituloOrdens = new Label
            {
                Text = "Ordens de serviço recentes",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                Dock = DockStyle.Top,
                Height = 35
            };

            dgvOrdensRecentes = CriarGrid();
            dgvOrdensRecentes.Dock = DockStyle.Fill;

            CriarColunasOrdensRecentes();

            pnlOrdensRecentes.Controls.Add(dgvOrdensRecentes);
            pnlOrdensRecentes.Controls.Add(lblTituloOrdens);

            //painel de estoque baixo
            pnlEstoqueBaixo = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            lblTituloEstoque = new Label
            {
                Text = "Estoque baixo",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                Dock = DockStyle.Top,
                Height = 35
            };

            dgvEstoqueBaixo = CriarGrid();
            dgvEstoqueBaixo.Dock = DockStyle.Fill;

            CriarColunasEstoqueBaixo();

            pnlEstoqueBaixo.Controls.Add(dgvEstoqueBaixo);
            pnlEstoqueBaixo.Controls.Add(lblTituloEstoque);

            pnlConteudo.Controls.Add(pnlEstoqueBaixo);
            pnlConteudo.Controls.Add(pnlOrdensRecentes);

            Controls.Add(pnlConteudo);
            Controls.Add(pnlCards);
            Controls.Add(pnlHeader);

            Resize += DashboardControl_Resize;

            ResumeLayout(false);
        }

        // Evento resize
        private void DashboardControl_Resize(object sender, EventArgs e)
        {
            if (pnlCards == null)
                return;

            int larguraDisponivel = pnlCards.ClientSize.Width;

            int espacamento = 15;

            int larguraCard = (larguraDisponivel - (espacamento * 3)) / 4;

            if (larguraCard < 180)
                larguraCard = 180;

            foreach (Control controle in pnlCards.Controls)
            {
                controle.Width = larguraCard;
                controle.Margin = new Padding(0, 0, espacamento, 0);
            }

            if (pnlOrdensRecentes != null)
            {
                pnlOrdensRecentes.Width = Math.Max(400, pnlConteudo.ClientSize.Width / 2);
            }
        }


        private void CarregarDados()
        {
            CarregarIndicadores();
            CarregarOrdensRecentes();
            CarregarEstoqueBaixo();
        }

        // Funções de carregamentos dos dados
        private void CarregarIndicadores()
        {
            var ordens = DadosMock.OrdensServico;

            int abertas = ordens.Count(x => x.Status == "Aberta");
            int emAndamento = ordens.Count(x => x.Status == "Em andamento");
            int aguardando = ordens.Count(x => x.Status == "Aguardando");

            decimal valorTotal = ordens.Sum(x => x.ValorTotal);

            lblOSAbertasValor.Text = abertas.ToString();
            lblOSEmAndamentoValor.Text = emAndamento.ToString();
            lblOSAguardandoValor.Text = aguardando.ToString();
            lblValorTotalValor.Text = valorTotal.ToString("C2");
        }

        private void CarregarOrdensRecentes()
        {
            var ordens = DadosMock.OrdensServico
                .OrderByDescending(x => x.DataAbertura)
                .Take(5)
                .Select(x => new
                {
                    OS = $"#{x.Id}",
                    Cliente = x.ClienteNome,
                    Equipamento = x.EquipamentoNome,
                    Status = x.Status,
                    Valor = x.ValorTotal
                }).ToList();

            dgvOrdensRecentes.DataSource = null;
            dgvOrdensRecentes.DataSource = ordens;
        }

        private void CarregarEstoqueBaixo()
        {
            var produtos = DadosMock.Produtos
                .Where(x => x.Ativo && x.EstoqueAtual <= LIMITE_ESTOQUE_BAIXO)
                .OrderBy(x => x.EstoqueAtual)
                .Take(5)
                .Select(x => new
                {
                    Produto = x.Nome,
                    SKU = x.SKU,
                    Estoque = x.EstoqueAtual
                })
                .ToList();

            dgvEstoqueBaixo.DataSource = null;
            dgvEstoqueBaixo.DataSource = produtos;
        }

        private void CriarColunasOrdensRecentes()
        {
            dgvOrdensRecentes.Columns.Add(CriarColuna("OS", "OS", 10));
            dgvOrdensRecentes.Columns.Add(CriarColuna("Cliente", "Cliente", 30));
            dgvOrdensRecentes.Columns.Add(CriarColuna("Equipamento", "Equipamento", 25));
            dgvOrdensRecentes.Columns.Add(CriarColuna("Status", "Status", 20));
            dgvOrdensRecentes.Columns.Add(CriarColuna("Valor", "Valor", 15, "C2"));
        }

        private void CriarColunasEstoqueBaixo()
        {
            dgvEstoqueBaixo.Columns.Add(CriarColuna("Produto", "Produto", 50));
            dgvEstoqueBaixo.Columns.Add(CriarColuna("SKU", "SKU", 25));
            dgvEstoqueBaixo.Columns.Add(CriarColuna("Estoque", "Estoque atual", 25, "N2"));
        }

        private DataGridView CriarGrid()
        {
            DataGridView grid = new DataGridView
            {
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
                Font = new Font("Segoe UI", 9.5F)
            };

            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 246, 248),
                ForeColor = Color.FromArgb(50, 50, 50),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
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

            grid.RowTemplate.Height = 38;

            return grid;
        }

        private DataGridViewTextBoxColumn CriarColuna(string propriedade, string titulo, float peso, string formato = null)
        {
            DataGridViewTextBoxColumn coluna = new DataGridViewTextBoxColumn
            {
                Name = propriedade,
                HeaderText = titulo,
                DataPropertyName = propriedade,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = peso
            };

            if (!string.IsNullOrWhiteSpace(formato))
            {
                coluna.DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = formato
                };
            }

            return coluna;
        }

        private Panel CriarCard(string titulo, out Label lblTituloCard, out Label lblValor)
        {
            Panel card = new Panel
            {
                Width = 220,
                Height = 105,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(15)
            };

            lblTituloCard = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(15, 15)
            };

            lblValor = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(15, 42)
            };

            card.Controls.Add(lblTituloCard);
            card.Controls.Add(lblValor);

            return card;
        }
    }
}