
namespace ERP_Oficina.Forms
{
    public class FormAdicionarMaterialOS : Form
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private Label lblProduto;
        private ComboBox cmbProduto;

        private Label lblEstoque;
        private Label lblEstoqueValor;

        private Label lblQuantidade;
        private NumericUpDown nudQuantidade;

        private Label lblPrecoUnitario;
        private Label lblPrecoUnitarioValor;

        private Label lblSubtotal;
        private Label lblSubtotalValor;

        private Button btnCancelar;
        private Button btnAdicionar;

        // =========================================================
        // DADOS
        // =========================================================

        private int ordemServicoId;

        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public FormAdicionarMaterialOS(int ordemServicoId)
        {
            this.ordemServicoId = ordemServicoId;
            InicializarFormulario();
            CarregarProdutos();
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InicializarFormulario()
        {
            Text = "Adicionar Material";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            Width = 480;
            Height = 410;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            // =====================================================
            // TÍTULO
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "Adicionar material",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(25, 20)
            };

            Controls.Add(lblTitulo);

            // =====================================================
            // PRODUTO
            // =====================================================

            lblProduto = CriarLabel("Produto", 25, 65);

            cmbProduto = new ComboBox
            {
                Location = new Point(25, 90),
                Width = 410,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbProduto.SelectedIndexChanged += CmbProduto_SelectedIndexChanged;

            Controls.Add(lblProduto);
            Controls.Add(cmbProduto);

            // =====================================================
            // ESTOQUE
            // =====================================================

            lblEstoque = CriarLabel("Estoque disponível", 25, 140);
            lblEstoqueValor = CriarLabelValor("0,00", 25, 165);

            Controls.Add(lblEstoque);
            Controls.Add(lblEstoqueValor);

            // =====================================================
            // QUANTIDADE
            // =====================================================

            lblQuantidade = CriarLabel("Quantidade", 235, 140);

            nudQuantidade = new NumericUpDown
            {
                Location = new Point(235, 165),
                Width = 200,
                Height = 32,
                Minimum = 1M,
                Maximum = 999999M,
                DecimalPlaces = 0,
                Increment = 1M,
                Value = 1M
            };

            nudQuantidade.ValueChanged += NudQuantidade_ValueChanged;

            Controls.Add(lblQuantidade);
            Controls.Add(nudQuantidade);

            // =====================================================
            // PREÇO
            // =====================================================

            lblPrecoUnitario = CriarLabel("Preço unitário", 25, 215);
            lblPrecoUnitarioValor = CriarLabelValor("R$ 0,00", 25, 240);

            Controls.Add(lblPrecoUnitario);
            Controls.Add(lblPrecoUnitarioValor);

            // =====================================================
            // SUBTOTAL
            // =====================================================

            lblSubtotal = CriarLabel("Subtotal", 235, 215);
            lblSubtotalValor = CriarLabelValor("R$ 0,00", 235, 240);

            lblSubtotalValor.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            Controls.Add(lblSubtotal);
            Controls.Add(lblSubtotalValor);

            // =====================================================
            // CANCELAR
            // =====================================================

            btnCancelar = CriarBotao("Cancelar", Color.White, Color.FromArgb(70, 70, 70));
            btnCancelar.Width = 100;
            btnCancelar.Height = 35;
            btnCancelar.Location = new Point(225, 305);
            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.Add(btnCancelar);

            // =====================================================
            // ADICIONAR
            // =====================================================

            btnAdicionar = CriarBotao("Adicionar", Color.FromArgb(0, 120, 215), Color.White);
            btnAdicionar.Width = 100;
            btnAdicionar.Height = 35;
            btnAdicionar.Location = new Point(335, 305);
            btnAdicionar.Click += BtnAdicionar_Click;
            Controls.Add(btnAdicionar);
            AcceptButton = btnAdicionar;
            CancelButton = btnCancelar;
        }

        // =========================================================
        // PRODUTOS
        // =========================================================

        private void CarregarProdutos()
        {
            cmbProduto.DataSource = null;
            cmbProduto.DataSource = DadosMock.Produtos.Where(x => x.Ativo && x.EstoqueAtual > 0).OrderBy(x => x.Nome).ToList();
            cmbProduto.DisplayMember = "Nome";
            cmbProduto.ValueMember = "Id";
            cmbProduto.SelectedIndex = -1;
            AtualizarValores();
        }

        // =========================================================
        // PRODUTO SELECIONADO
        // =========================================================

        private void CmbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Produto produto = cmbProduto.SelectedItem as Produto;

            if (produto == null)
            {
                lblEstoqueValor.Text = "0,00";
                lblPrecoUnitarioValor.Text = "R$ 0,00";
                lblSubtotalValor.Text = "R$ 0,00";
                return;
            }

            // Limita a quantidade máxima ao estoque disponível.
            nudQuantidade.Maximum = produto.EstoqueAtual;

            if (nudQuantidade.Value > produto.EstoqueAtual)
                nudQuantidade.Value = produto.EstoqueAtual;

            AtualizarValores();
        }

        // =========================================================
        // QUANTIDADE
        // =========================================================

        private void NudQuantidade_ValueChanged(object sender, EventArgs e)
        {
            AtualizarValores();
        }

        // =========================================================
        // VALORES
        // =========================================================

        private void AtualizarValores()
        {
            Produto produto = cmbProduto.SelectedItem as Produto;

            if (produto == null)
                return;

            int quantidade = (int)nudQuantidade.Value;
            decimal subtotal = produto.Preco * quantidade;
            lblEstoqueValor.Text = produto.EstoqueAtual.ToString("N0");
            lblPrecoUnitarioValor.Text = produto.Preco.ToString("C2");
            lblSubtotalValor.Text = subtotal.ToString("C2");
        }

        // =========================================================
        // ADICIONAR
        // =========================================================

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            Produto produto = cmbProduto.SelectedItem as Produto;

            if (produto == null)
            {
                MessageBox.Show(
                    "Selecione um produto.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cmbProduto.Focus();

                return;
            }

            int quantidade = (int)nudQuantidade.Value;

            if (quantidade <= 0)
            {
                MessageBox.Show(
                    "Informe uma quantidade válida.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                nudQuantidade.Focus();

                return;
            }

            // =====================================================
            // VALIDA ESTOQUE
            // =====================================================

            if (quantidade > produto.EstoqueAtual)
            {
                MessageBox.Show("A quantidade informada é maior que o estoque disponível.", "Estoque insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudQuantidade.Focus();
                return;
            }

            decimal subtotal = quantidade * produto.Preco;

            // =====================================================
            // NOVO ITEM
            // =====================================================

            int novoId = DadosMock.OrdensServicoMateriais.Count == 0 ? 1 : DadosMock.OrdensServicoMateriais.Max(x => x.Id) + 1;

            OrdemServicoMaterial item = new OrdemServicoMaterial
            {
                Id = novoId,
                OrdemServicoId = ordemServicoId,
                ProdutoId = produto.Id,
                ProdutoNome = produto.Nome,
                Quantidade = quantidade,
                PrecoUnitario = produto.Preco,
                Subtotal = subtotal
            };

            DadosMock.OrdensServicoMateriais.Add(item);

            produto.EstoqueAtual -= quantidade; // NOTE!
            DialogResult = DialogResult.OK;

            // Registra a movimentação de estoque
            MovimentacaoEstoque movimento = new MovimentacaoEstoque
            {
                Id = DadosMock.MovimentacoesEstoque.Count == 0 ? 1 : DadosMock.MovimentacoesEstoque.Max(x => x.Id) + 1,
                ProdutoId = produto.Id,
                UsuarioId = 1,
                OrdemServicoId = ordemServicoId,
                TipoMovimento = "Saída",
                Quantidade = quantidade,
                DataMovimento = DateTime.Now,
                Observacao = $"Material utilizado na OS #{ordemServicoId}"
            };

            DadosMock.MovimentacoesEstoque.Add(movimento);

            // Atualiza os totais da OS
            AtualizarTotaisOrdemServico();

            Close();
        }



        // =========================================================
        // TOTAIS DA OS
        // =========================================================

        private void AtualizarTotaisOrdemServico()
        {
            OrdemServico ordem = DadosMock.OrdensServico.FirstOrDefault(x => x.Id == ordemServicoId);

            if (ordem == null)
                return;

            ordem.ValorServicos = DadosMock.OrdensServicoServicos.Where(x => x.OrdemServicoId == ordemServicoId).Sum(x => x.Subtotal);
            ordem.ValorMateriais = DadosMock.OrdensServicoMateriais.Where(x => x.OrdemServicoId == ordemServicoId).Sum(x => x.Subtotal);
            ordem.ValorTotal = ordem.ValorServicos + ordem.ValorMateriais;
        }

        // =========================================================
        // LABEL
        // =========================================================

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

        private Label CriarLabelValor(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(35, 35, 35),
                Location = new Point(x, y)
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
                FlatAppearance = { BorderColor = background, BorderSize = 1 }
            };
        }
    }
}