
namespace ERP_Oficina.Forms
{
    public class FormMovimentacaoEstoque : Form
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private Label lblTitulo;
        private Label lblProduto;
        private ComboBox cmbProduto;
        private Label lblEstoqueAtual;
        private Label lblEstoqueAtualValor;
        private Label lblQuantidade;
        private NumericUpDown nudQuantidade;
        private Label lblObservacao;
        private TextBox txtObservacao;
        private Button btnCancelar;
        private Button btnConfirmar;

        // =========================================================
        // DADOS
        // =========================================================

        private string tipoMovimento;

        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public FormMovimentacaoEstoque(string tipoMovimento)
        {
            this.tipoMovimento = tipoMovimento;
            InicializarFormulario();
            ConfigurarFormulario();
            CarregarProdutos();
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InicializarFormulario()
        {
            Text = "Movimentação de Estoque";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            Width = 560;
            Height = 440;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            // =====================================================
            // TÍTULO
            // =====================================================

            lblTitulo = new Label
            {
                Text = "Movimentação de estoque",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(30, 25)
            };
            Controls.Add(lblTitulo);

            // =====================================================
            // PRODUTO
            // =====================================================

            lblProduto = CriarLabel("Produto", 30, 75);
            cmbProduto = new ComboBox
            {
                Location = new Point(30, 100),
                Width = 490,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbProduto.SelectedIndexChanged += CmbProduto_SelectedIndexChanged;
            Controls.Add(lblProduto);
            Controls.Add(cmbProduto);

            // =====================================================
            // ESTOQUE ATUAL
            // =====================================================

            lblEstoqueAtual = CriarLabel("Estoque atual", 30, 145);
            lblEstoqueAtualValor = new Label
            {
                Text = "-",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                AutoSize = true,
                Location = new Point(30, 170)
            };
            Controls.Add(lblEstoqueAtual);
            Controls.Add(lblEstoqueAtualValor);

            // =====================================================
            // QUANTIDADE
            // =====================================================

            lblQuantidade = CriarLabel("Quantidade", 30, 205);
            nudQuantidade = new NumericUpDown
            {
                Location = new Point(30, 230),
                Width = 230,
                Height = 32,
                Minimum = 0.01m,
                Maximum = 999999,
                DecimalPlaces = 2,
                Increment = 1,
                ThousandsSeparator = true
            };
            Controls.Add(lblQuantidade);
            Controls.Add(nudQuantidade);

            // =====================================================
            // OBSERVAÇÃO
            // =====================================================

            lblObservacao = CriarLabel("Observação", 285, 205);
            txtObservacao = new TextBox
            {
                Location = new Point(285, 230),
                Width = 235,
                Height = 70,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(lblObservacao);
            Controls.Add(txtObservacao);

            // =====================================================
            // CANCELAR
            // =====================================================

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 100,
                Height = 35,
                Location = new Point(300, 330),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(70, 70, 70),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnCancelar.Click += BtnCancelar_Click;
            Controls.Add(btnCancelar);

            // =====================================================
            // CONFIRMAR
            // =====================================================

            btnConfirmar = new Button
            {
                Text = "Confirmar",
                Width = 100,
                Height = 35,
                Location = new Point(410, 330),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirmar.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
            btnConfirmar.Click += BtnConfirmar_Click;
            Controls.Add(btnConfirmar);

            AcceptButton = btnConfirmar;
            CancelButton = btnCancelar;
        }

        // =========================================================
        // CONFIGURAÇÃO
        // =========================================================

        private void ConfigurarFormulario()
        {
            switch (tipoMovimento)
            {
                case "Entrada":
                    Text = "Entrada de Estoque";
                    lblTitulo.Text = "Entrada de estoque";
                    btnConfirmar.Text = "Adicionar";
                    btnConfirmar.BackColor = Color.FromArgb(25, 135, 84);
                    btnConfirmar.FlatAppearance.BorderColor = Color.FromArgb(25, 135, 84);
                    break;

                case "Saída":
                    Text = "Saída de Estoque";
                    lblTitulo.Text = "Saída de estoque";
                    btnConfirmar.Text = "Retirar";
                    btnConfirmar.BackColor = Color.FromArgb(220, 53, 69);
                    btnConfirmar.FlatAppearance.BorderColor = Color.FromArgb(220, 53, 69);
                    break;

                case "Ajuste":
                    Text = "Ajuste de Estoque";
                    lblTitulo.Text = "Ajuste de estoque";
                    btnConfirmar.Text = "Ajustar";
                    btnConfirmar.BackColor = Color.FromArgb(0, 120, 215);
                    break;

                default:
                    throw new ArgumentException("Tipo de movimentação inválido.");
            }
        }

        // =========================================================
        // PRODUTOS
        // =========================================================

        private void CarregarProdutos()
        {
            cmbProduto.DataSource = null;
            cmbProduto.DataSource = DadosMock.Produtos.Where(x => x.Ativo).OrderBy(x => x.Nome).ToList();
            cmbProduto.DisplayMember = "Nome";
            cmbProduto.ValueMember = "Id";
            cmbProduto.SelectedIndex = -1;
            lblEstoqueAtualValor.Text = "-";
        }

        // =========================================================
        // PRODUTO SELECIONADO
        // =========================================================

        private void CmbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Produto produto = cmbProduto.SelectedItem as Produto;

            if (produto == null)
            {
                lblEstoqueAtualValor.Text = "-";
                return;
            }

            lblEstoqueAtualValor.Text = produto.EstoqueAtual.ToString("N2");
        }

        // =========================================================
        // CONFIRMAR
        // =========================================================

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            Produto produto = cmbProduto.SelectedItem as Produto;

            if (produto == null)
            {
                MessageBox.Show("Selecione um produto.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProduto.Focus();
                return;
            }

            decimal quantidade = nudQuantidade.Value;

            if (quantidade <= 0)
            {
                MessageBox.Show("Informe uma quantidade válida.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudQuantidade.Focus();
                return;
            }

            if (tipoMovimento == "Saída" && quantidade > produto.EstoqueAtual)
            {
                MessageBox.Show("A quantidade da saída não pode ser maior que o estoque atual.", "Estoque insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudQuantidade.Focus();
                return;
            }

            decimal estoqueAnterior = produto.EstoqueAtual;
            decimal novoEstoque = estoqueAnterior;

            switch (tipoMovimento)
            {
                case "Entrada":
                    novoEstoque = estoqueAnterior + quantidade;
                    break;

                case "Saída":
                    novoEstoque = estoqueAnterior - quantidade;
                    break;

                case "Ajuste":
                    novoEstoque = quantidade;
                    break;
            }

            int novoId = DadosMock.MovimentacoesEstoque.Count == 0
                ? 1
                : DadosMock.MovimentacoesEstoque.Max(x => x.Id) + 1;

            MovimentacaoEstoque movimentacao = new MovimentacaoEstoque
            {
                Id = novoId,
                ProdutoId = produto.Id,
                UsuarioId = 1,
                TipoMovimento = tipoMovimento,
                Quantidade = quantidade,
                DataMovimento = DateTime.Now,
                Observacao = txtObservacao.Text.Trim()
            };

            DadosMock.MovimentacoesEstoque.Add(movimentacao);
            produto.EstoqueAtual = novoEstoque;

            DialogResult = DialogResult.OK;
            Close();
        }

        // =========================================================
        // CANCELAR
        // =========================================================

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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
    }
}