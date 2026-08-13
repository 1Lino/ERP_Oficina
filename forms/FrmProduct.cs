
namespace ERP_Oficina.Forms
{
    public class FormProduto : Form
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private TextBox txtNome;
        private TextBox txtSKU;
        private ComboBox cmbCategoria;
        private NumericUpDown nudEstoque;
        private NumericUpDown nudPreco;
        private CheckBox chkAtivo;
        private Button btnCancelar;
        private Button btnSalvar;

        // =========================================================
        // PROPRIEDADES
        // =========================================================

        public string Nome { get; private set; }
        public string SKU { get; private set; }
        public int CategoriaId { get; private set; }
        public string CategoriaNome { get; private set; }
        public decimal EstoqueAtual { get; private set; }
        public decimal Preco { get; private set; }
        public bool Ativo { get; private set; }

        // =========================================================
        // CONSTRUTOR - NOVO
        // =========================================================

        public FormProduto()
        {
            InicializarFormulario();
            CarregarCategorias();
            chkAtivo.Checked = true;
        }

        // =========================================================
        // CONSTRUTOR - EDITAR
        // =========================================================

        public FormProduto(Produto produto)
        {
            InicializarFormulario();
            CarregarCategorias();

            if (produto == null)
                return;

            txtNome.Text = produto.Nome;
            txtSKU.Text = produto.SKU;
            nudEstoque.Value = produto.EstoqueAtual;
            nudPreco.Value = produto.Preco;
            chkAtivo.Checked = produto.Ativo;
            cmbCategoria.SelectedValue = produto.CategoriaId;
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InicializarFormulario()
        {
            Text = "Produto";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            Width = 560;
            Height = 430;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            // =====================================================
            // TÍTULO
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "Dados do produto",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(30, 25)
            };
            Controls.Add(lblTitulo);

            // =====================================================
            // NOME
            // =====================================================

            Label lblNome = CriarLabel("Nome", 30, 75);
            txtNome = new TextBox
            {
                Location = new Point(30, 100),
                Width = 490,
                Height = 30
            };
            Controls.Add(lblNome);
            Controls.Add(txtNome);

            // =====================================================
            // SKU
            // =====================================================

            Label lblSKU = CriarLabel("SKU", 30, 140);
            txtSKU = new TextBox
            {
                Location = new Point(30, 165),
                Width = 230,
                Height = 30
            };
            Controls.Add(lblSKU);
            Controls.Add(txtSKU);

            // =====================================================
            // CATEGORIA
            // =====================================================

            Label lblCategoria = CriarLabel("Categoria", 285, 140);
            cmbCategoria = new ComboBox
            {
                Location = new Point(285, 165),
                Width = 235,
                Height = 30,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            Controls.Add(lblCategoria);
            Controls.Add(cmbCategoria);

            // =====================================================
            // ESTOQUE
            // =====================================================

            Label lblEstoque = CriarLabel("Estoque atual", 30, 205);
            nudEstoque = new NumericUpDown
            {
                Location = new Point(30, 230),
                Width = 230,
                Height = 30,
                Minimum = 0,
                Maximum = 999999,
                DecimalPlaces = 2,
                Increment = 1
            };
            Controls.Add(lblEstoque);
            Controls.Add(nudEstoque);

            // =====================================================
            // PREÇO
            // =====================================================

            Label lblPreco = CriarLabel("Preço", 285, 205);
            nudPreco = new NumericUpDown
            {
                Location = new Point(285, 230),
                Width = 235,
                Height = 30,
                Minimum = 0,
                Maximum = 999999999,
                DecimalPlaces = 2,
                Increment = 1,
                ThousandsSeparator = true
            };
            Controls.Add(lblPreco);
            Controls.Add(nudPreco);

            // =====================================================
            // STATUS
            // =====================================================

            chkAtivo = new CheckBox
            {
                Text = "Produto ativo",
                AutoSize = true,
                Location = new Point(30, 285),
                Checked = true
            };
            Controls.Add(chkAtivo);

            // =====================================================
            // CANCELAR
            // =====================================================

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 100,
                Height = 35,
                Location = new Point(300, 335),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(70, 70, 70),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnCancelar.Click += BtnCancelar_Click;
            Controls.Add(btnCancelar);

            // =====================================================
            // SALVAR
            // =====================================================

            btnSalvar = new Button
            {
                Text = "Salvar",
                Width = 100,
                Height = 35,
                Location = new Point(410, 335),
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

        // =========================================================
        // CARREGAR CATEGORIAS
        // =========================================================

        private void CarregarCategorias()
        {
            cmbCategoria.DataSource = null;
            cmbCategoria.DataSource = DadosMock.Categorias.OrderBy(x => x.Nome).ToList();
            cmbCategoria.DisplayMember = "Nome";
            cmbCategoria.ValueMember = "Id";
            cmbCategoria.SelectedIndex = -1;
        }

        // =========================================================
        // SALVAR
        // =========================================================

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do produto.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSKU.Text))
            {
                MessageBox.Show("Informe o SKU do produto.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSKU.Focus();
                return;
            }

            if (cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma categoria.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoria.Focus();
                return;
            }

            string sku = txtSKU.Text.Trim();
            bool skuExiste = DadosMock.Produtos.Any(x => x.SKU.Equals(sku, StringComparison.OrdinalIgnoreCase));

            Categoria categoria = cmbCategoria.SelectedItem as Categoria;
            if (categoria == null)
                return;

            Nome = txtNome.Text.Trim();
            SKU = sku;
            CategoriaId = categoria.Id;
            CategoriaNome = categoria.Nome;
            EstoqueAtual = nudEstoque.Value;
            Preco = nudPreco.Value;
            Ativo = chkAtivo.Checked;

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