
namespace ERP_Oficina.Forms
{
    public class FormServico : Form
    {
        private TextBox txtNome;
        private TextBox txtDescricao;
        private NumericUpDown nudPrecoBase;
        private CheckBox chkAtivo;

        private Button btnCancelar;
        private Button btnSalvar;

        private Servico servicoEdicao;

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal PrecoBase { get; private set; }
        public bool Ativo { get; private set; }

        // =========================================================
        // NOVO
        // =========================================================

        public FormServico()
        {
            InicializarFormulario();
            chkAtivo.Checked = true;
        }

        // =========================================================
        // EDITAR
        // =========================================================

        public FormServico(Servico servico)
        {
            servicoEdicao = servico;
            InicializarFormulario();

            if (servico == null)
                return;

            txtNome.Text = servico.Nome;
            txtDescricao.Text = servico.Descricao;
            nudPrecoBase.Value = servico.PrecoBase;
            chkAtivo.Checked = servico.Ativo;
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InicializarFormulario()
        {
            Text = "Serviço";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            Width = 560;
            Height = 410;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            // =====================================================
            // TÍTULO
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "Dados do serviço",
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
            // DESCRIÇÃO
            // =====================================================

            Label lblDescricao = CriarLabel("Descrição", 30, 140);
            txtDescricao = new TextBox
            {
                Location = new Point(30, 165),
                Width = 490,
                Height = 70,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(lblDescricao);
            Controls.Add(txtDescricao);

            // =====================================================
            // PREÇO
            // =====================================================

            Label lblPreco = CriarLabel("Preço base", 30, 250);
            nudPrecoBase = new NumericUpDown
            {
                Location = new Point(30, 275),
                Width = 230,
                Height = 30,
                Minimum = 0,
                Maximum = 999999999,
                DecimalPlaces = 2,
                Increment = 1,
                ThousandsSeparator = true
            };
            Controls.Add(lblPreco);
            Controls.Add(nudPrecoBase);

            // =====================================================
            // STATUS
            // =====================================================

            chkAtivo = new CheckBox
            {
                Text = "Serviço ativo",
                AutoSize = true,
                Location = new Point(285, 280),
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
                Location = new Point(300, 325),
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
                Location = new Point(410, 325),
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
        // SALVAR
        // =========================================================

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do serviço.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (nudPrecoBase.Value <= 0)
            {
                MessageBox.Show("Informe um preço base válido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrecoBase.Focus();
                return;
            }

            Nome = txtNome.Text.Trim();
            Descricao = txtDescricao.Text.Trim();
            PrecoBase = nudPrecoBase.Value;
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