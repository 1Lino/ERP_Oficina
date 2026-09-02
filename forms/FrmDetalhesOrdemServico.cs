// Este formulário se refere às abas de detalhes ao dar duplo clique em alguma Ordem de Serviço
using ERP_Oficina.Models.Permissao;
using ERP_Oficina.Services.Autorizacao;

namespace ERP_Oficina.Forms
{
    public class FormDetalhesOrdemServico : Form
    {
        // =========================================================
        // DADOS
        // =========================================================

        private OrdemServico ordem;

        // =========================================================
        // CONTROLES
        // =========================================================

        private Label lblTitulo;
        private Label lblSubtitulo;

        private TabControl tabControl;

        private TabPage tabInformacoes;
        private TabPage tabServicos;
        private TabPage tabMateriais;
        private TabPage tabHistorico;

        private Button btnFechar;

        // =========================================================
        // INFORMAÇÕES
        // =========================================================

        private Label lblCliente;
        private Label lblClienteValor;

        private Label lblEquipamento;
        private Label lblEquipamentoValor;

        private Label lblResponsavel;
        private Label lblResponsavelValor;

        private Label lblStatus;
        private Label lblStatusValor;

        private Label lblAbertura;
        private Label lblAberturaValor;

        private Label lblFechamento;
        private Label lblFechamentoValor;

        private Label lblObservacoes;
        private TextBox txtObservacoes;

        // =========================================================
        // SERVIÇOS
        // =========================================================

        private DataGridView dgvServicos;
        private Button btnAdicionarServico;
        private Button btnRemoverServico;
        private Label lblTotalServicos;

        // =========================================================
        // MATERIAIS
        // =========================================================

        private DataGridView dgvMateriais;
        private Button btnAdicionarMaterial;
        private Button btnRemoverMaterial;
        private Label lblTotalMateriais;

        // =========================================================
        // HISTÓRICO
        // =========================================================

        private DataGridView dgvHistorico;

        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public FormDetalhesOrdemServico(OrdemServico ordem)
        {
            this.ordem = ordem;

            InicializarFormulario();
            CarregarInformacoes();
            CarregarServicos();
            CarregarMateriais();
            CarregarHistorico();
        }

        // =========================================================
        // FORMULÁRIO
        // =========================================================

        private void InicializarFormulario()
        {
            Text = $"Ordem de Serviço #{ordem.Id}";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(950, 680);
            MinimumSize = new Size(800, 600);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            // =====================================================
            // TÍTULO
            // =====================================================

            lblTitulo = new Label
            {
                Text = $"OS #{ordem.Id}",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                Location = new Point(25, 20),
                AutoSize = true
            };
            Controls.Add(lblTitulo);

            lblSubtitulo = new Label
            {
                Text = ordem.ClienteNome,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(27, 52),
                AutoSize = true
            };
            Controls.Add(lblSubtitulo);

            // =====================================================
            // BOTÃO FECHAR
            // =====================================================

            btnFechar = CriarBotao("Fechar", Color.White, Color.FromArgb(70, 70, 70));
            btnFechar.Width = 90;
            btnFechar.Height = 35;
            btnFechar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFechar.Location = new Point(ClientSize.Width - btnFechar.Width - 25, 20);
            btnFechar.Click += (s, e) => Close();
            Controls.Add(btnFechar);

            // =====================================================
            // TAB CONTROL
            // =====================================================

            tabControl = new TabControl
            {
                Location = new Point(25, 85),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(ClientSize.Width - 50, ClientSize.Height - 135),
                Font = new Font("Segoe UI", 10F)
            };

            tabInformacoes = new TabPage("Informações");
            tabServicos = new TabPage("Serviços");
            tabMateriais = new TabPage("Materiais");
            tabHistorico = new TabPage("Histórico");

            tabControl.TabPages.Add(tabInformacoes);
            tabControl.TabPages.Add(tabServicos);
            tabControl.TabPages.Add(tabMateriais);
            tabControl.TabPages.Add(tabHistorico);
            Controls.Add(tabControl);

            // =====================================================
            // CRIA AS ABAS
            // =====================================================

            CriarAbaInformacoes();
            CriarAbaServicos();
            CriarAbaMateriais();
            CriarAbaHistorico();
        }

        // =========================================================
        // ABA INFORMAÇÕES
        // =========================================================

        private void CriarAbaInformacoes()
        {
            lblCliente = CriarLabelTitulo("Cliente", 25, 25);
            lblClienteValor = CriarLabelValor(25, 52);

            lblEquipamento = CriarLabelTitulo("Equipamento", 300, 25);
            lblEquipamentoValor = CriarLabelValor(300, 52);

            lblResponsavel = CriarLabelTitulo("Responsável", 575, 25);
            lblResponsavelValor = CriarLabelValor(575, 52);

            lblStatus = CriarLabelTitulo("Status", 25, 105);
            lblStatusValor = CriarLabelValor(25, 132);

            lblAbertura = CriarLabelTitulo("Data de abertura", 300, 105);
            lblAberturaValor = CriarLabelValor(300, 132);

            lblFechamento = CriarLabelTitulo("Data de fechamento", 575, 105);
            lblFechamentoValor = CriarLabelValor(575, 132);

            lblObservacoes = CriarLabelTitulo("Observações", 25, 185);

            txtObservacoes = new TextBox
            {
                Location = new Point(25, 212),
                Width = 820,
                Height = 150,
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Vertical,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            tabInformacoes.Controls.Add(lblCliente);
            tabInformacoes.Controls.Add(lblClienteValor);
            tabInformacoes.Controls.Add(lblEquipamento);
            tabInformacoes.Controls.Add(lblEquipamentoValor);
            tabInformacoes.Controls.Add(lblResponsavel);
            tabInformacoes.Controls.Add(lblResponsavelValor);
            tabInformacoes.Controls.Add(lblStatus);
            tabInformacoes.Controls.Add(lblStatusValor);
            tabInformacoes.Controls.Add(lblAbertura);
            tabInformacoes.Controls.Add(lblAberturaValor);
            tabInformacoes.Controls.Add(lblFechamento);
            tabInformacoes.Controls.Add(lblFechamentoValor);
            tabInformacoes.Controls.Add(lblObservacoes);
            tabInformacoes.Controls.Add(txtObservacoes);
        }

        // =========================================================
        // CARREGAR INFORMAÇÕES
        // =========================================================

        private void CarregarInformacoes()
        {
            lblClienteValor.Text = ordem.ClienteNome;
            lblEquipamentoValor.Text = ordem.EquipamentoNome;
            lblResponsavelValor.Text = ordem.ResponsavelNome;
            lblStatusValor.Text = ordem.Status;
            lblAberturaValor.Text = ordem.DataAbertura.ToString("dd/MM/yyyy HH:mm");
            lblFechamentoValor.Text = ordem.DataFechamento.HasValue
                ? ordem.DataFechamento.Value.ToString("dd/MM/yyyy HH:mm")
                : "-";
            txtObservacoes.Text = ordem.Observacoes;
        }

        // =========================================================
        // ABA SERVIÇOS
        // =========================================================

        private void CriarAbaServicos()
        {
            btnAdicionarServico = CriarBotao("Adicionar", Color.FromArgb(0, 120, 215), Color.White);
            btnAdicionarServico.Width = 100;
            btnAdicionarServico.Height = 35;
            btnAdicionarServico.Location = new Point(25, 20);
            btnAdicionarServico.Click += BtnAdicionarServico_Click;

            btnRemoverServico = CriarBotao("Remover", Color.White, Color.FromArgb(220, 53, 69));
            btnRemoverServico.Width = 100;
            btnRemoverServico.Height = 35;
            btnRemoverServico.Enabled = Autorizacao.TemPermissao(Permissao.EditarOrdensServico);
            btnRemoverServico.Location = new Point(135, 20);
            btnRemoverServico.Click += BtnRemoverServico_Click;

            dgvServicos = CriarGrid();
            dgvServicos.Location = new Point(25, 70);
            dgvServicos.Size = new Size(150, 390);
            dgvServicos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgvServicos.AllowUserToResizeRows = false;
            dgvServicos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvServicos.ReadOnly = true;

            dgvServicos.Columns.Add(CriarColuna("ServicoNome", "Serviço", 15)); // 35
            dgvServicos.Columns.Add(CriarColuna("Quantidade", "Qtd.", 12));
            dgvServicos.Columns.Add(CriarColuna("PrecoUnitario", "Valor unitário", 15, "C2"));
            dgvServicos.Columns.Add(CriarColuna("Subtotal", "Subtotal", 15, "C2"));


            lblTotalServicos = new Label
            {
                Text = "Total: R$ 0,00",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(720, 475)
            };

            tabServicos.Controls.Add(btnAdicionarServico);
            tabServicos.Controls.Add(btnRemoverServico);
            tabServicos.Controls.Add(dgvServicos);
            tabServicos.Controls.Add(lblTotalServicos);
        }

        // =========================================================
        // CARREGAR SERVIÇOS
        // =========================================================

        private void CarregarServicos()
        {
            var servicos = DadosMock.OrdensServicoServicos
                .Where(x => x.OrdemServicoId == ordem.Id)
                .ToList();

            dgvServicos.DataSource = null;
            dgvServicos.DataSource = servicos;

            decimal total = servicos.Sum(x => x.Subtotal);
            lblTotalServicos.Text = $"Total: {total:C2}";
        }

        // =========================================================
        // ADICIONAR SERVIÇO
        // =========================================================

        private void BtnAdicionarServico_Click(object sender, EventArgs e)
        {
            using (FormAdicionarServicoOS form = new FormAdicionarServicoOS(ordem.Id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CarregarServicos();
                }
            }
        }

        // =========================================================
        // REMOVER SERVIÇO
        // =========================================================

        private void BtnRemoverServico_Click(object sender, EventArgs e)
        {
            if (dgvServicos.CurrentRow == null)
            {
                MessageBox.Show("Selecione um serviço.", "Remover", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OrdemServicoServico item = dgvServicos.CurrentRow.DataBoundItem as OrdemServicoServico;
            if (item == null)
                return;

            DialogResult resposta = MessageBox.Show(
                "Deseja remover este serviço da ordem?",
                "Remover serviço",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            DadosMock.OrdensServicoServicos.Remove(item);
            CarregarServicos();
        }

        // =========================================================
        // ABA MATERIAIS
        // =========================================================

        private void CriarAbaMateriais()
        {
            btnAdicionarMaterial = CriarBotao("Adicionar", Color.FromArgb(0, 120, 215), Color.White);
            btnAdicionarMaterial.Width = 100;
            btnAdicionarMaterial.Height = 35;
            btnAdicionarMaterial.Location = new Point(25, 20);
            btnAdicionarMaterial.Click += BtnAdicionarMaterial_Click;

            btnRemoverMaterial = CriarBotao("Remover", Color.White, Color.FromArgb(220, 53, 69));
            btnRemoverMaterial.Width = 100;
            btnRemoverMaterial.Height = 35;
            btnRemoverMaterial.Enabled = Autorizacao.TemPermissao(Permissao.EditarOrdensServico);
            btnRemoverMaterial.Location = new Point(135, 20);
            btnRemoverMaterial.Click += BtnRemoverMaterial_Click;

            dgvMateriais = CriarGrid();
            dgvMateriais.Location = new Point(25, 70);
            dgvMateriais.Size = new Size(150, 390);
            dgvMateriais.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgvMateriais.AllowUserToResizeRows = false;
            dgvMateriais.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMateriais.ReadOnly = true;

            dgvMateriais.Columns.Add(CriarColuna("ProdutoNome", "Produto", 15)); // 35
            dgvMateriais.Columns.Add(CriarColuna("Quantidade", "Qtd.", 12));
            dgvMateriais.Columns.Add(CriarColuna("PrecoUnitario", "Valor unitário", 15, "C2"));
            dgvMateriais.Columns.Add(CriarColuna("Subtotal", "Subtotal", 15, "C2"));

            lblTotalMateriais = new Label
            {
                Text = "Total: R$ 0,00",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(720, 475)
            };

            tabMateriais.Controls.Add(btnAdicionarMaterial);
            tabMateriais.Controls.Add(btnRemoverMaterial);
            tabMateriais.Controls.Add(dgvMateriais);
            tabMateriais.Controls.Add(lblTotalMateriais);
        }

        // =========================================================
        // CARREGAR MATERIAIS
        // =========================================================

        private void CarregarMateriais()
        {
            var materiais = DadosMock.OrdensServicoMateriais
                .Where(x => x.OrdemServicoId == ordem.Id)
                .ToList();

            dgvMateriais.DataSource = null;
            dgvMateriais.DataSource = materiais;

            decimal total = materiais.Sum(x => x.Subtotal);
            lblTotalMateriais.Text = $"Total: {total:C2}";
        }

        // =========================================================
        // ADICIONAR MATERIAL
        // =========================================================

        private void BtnAdicionarMaterial_Click(object sender, EventArgs e)
        {
            using (FormAdicionarMaterialOS form = new FormAdicionarMaterialOS(ordem.Id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CarregarMateriais();
                }
            }
        }

        // =========================================================
        // REMOVER MATERIAL
        // =========================================================
        private void BtnRemoverMaterial_Click(object sender, EventArgs e)
        {
            if (dgvMateriais.CurrentRow == null)
            {
                MessageBox.Show("Selecione um material.", "Remover", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OrdemServicoMaterial item = dgvMateriais.CurrentRow.DataBoundItem as OrdemServicoMaterial;
            if (item == null)
                return;

            DialogResult resposta = MessageBox.Show(
                "Deseja remover este material da ordem?",
                "Remover material",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resposta != DialogResult.Yes)
                return;

            // como OrdemServicoMaterial item possui o id do produto em questão, podemos acessar a base do estoque Produtos e 
            // selecionar o item em questão, para então consequentemente devolver este item ao estoque de acordo com a quantidade
            // que foi removida.
            var produto = DadosMock.Produtos.FirstOrDefault(p => p.Id == item.ProdutoId);
            if (produto != null)
            {
                produto.EstoqueAtual += item.Quantidade; // ex.: se removi 2 qtd de um item, essa quantia é devolvida ao estoque.
            }

            DadosMock.OrdensServicoMateriais.Remove(item);

            DadosMock.MovimentacoesEstoque.Add(new MovimentacaoEstoque
            {
                Id = DadosMock.MovimentacoesEstoque.Count == 0 ? 1 : DadosMock.MovimentacoesEstoque.Max(x => x.Id) + 1,
                ProdutoId = item.ProdutoId,
                UsuarioId = 1,
                OrdemServicoId = item.OrdemServicoId,
                TipoMovimento = "Entrada",
                Quantidade = item.Quantidade,
                DataMovimento = DateTime.Now,
                Observacao = $"Devolução de material da OS #{item.OrdemServicoId}"
            });

            AtualizarTotaisOrdemServico();
            CarregarMateriais();
        }

        // =========================================================
        // ABA HISTÓRICO
        // =========================================================

        private void CriarAbaHistorico()
        {
            dgvHistorico = CriarGrid();
            dgvHistorico.Dock = DockStyle.Fill;

            dgvHistorico.AllowUserToResizeRows = false;
            dgvHistorico.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHistorico.ReadOnly = true;

            dgvHistorico.Columns.Add(CriarColuna("DataAlteracao", "Data", 18, "dd/MM/yyyy HH:mm"));
            dgvHistorico.Columns.Add(CriarColuna("UsuarioNome", "Usuário", 20));
            dgvHistorico.Columns.Add(CriarColuna("StatusAnterior", "Status anterior", 18));
            dgvHistorico.Columns.Add(CriarColuna("StatusNovo", "Novo status", 18));
            dgvHistorico.Columns.Add(CriarColuna("Observacao", "Observação", 30));

            tabHistorico.Controls.Add(dgvHistorico);
        }

        // =========================================================
        // CARREGAR HISTÓRICO
        // =========================================================

        private void CarregarHistorico()
        {
            var historico = DadosMock.HistoricoOrdensServico
                .Where(x => x.OrdemServicoId == ordem.Id)
                .OrderByDescending(x => x.DataAlteracao)
                .ToList();

            dgvHistorico.DataSource = null;
            dgvHistorico.DataSource = historico;
        }

        private void AtualizarTotaisOrdemServico()
        {
            ordem.ValorServicos =
                DadosMock.OrdensServicoServicos
                    .Where(x => x.OrdemServicoId == ordem.Id)
                    .Sum(x => x.Subtotal);

            ordem.ValorMateriais =
                DadosMock.OrdensServicoMateriais
                    .Where(x => x.OrdemServicoId == ordem.Id)
                    .Sum(x => x.Subtotal);

            ordem.ValorTotal = ordem.ValorServicos + ordem.ValorMateriais;
        }

        // =========================================================
        // GRID
        // =========================================================

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

        // =========================================================
        // COLUNA
        // =========================================================

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

        // =========================================================
        // LABEL
        // =========================================================

        private Label CriarLabelTitulo(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(x, y),
                AutoSize = true
            };
        }

        private Label CriarLabelValor(int x, int y)
        {
            return new Label
            {
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(x, y),
                AutoSize = true
            };
        }

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
                FlatAppearance =
                {
                    BorderColor = background,
                    BorderSize = 1
                }
            };
        }
    }
}