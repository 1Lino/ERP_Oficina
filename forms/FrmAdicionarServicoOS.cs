
namespace ERP_Oficina.Forms
{
    public class FormAdicionarServicoOS : Form
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private Label lblServico;
        private ComboBox cmbServico;

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

        public FormAdicionarServicoOS(int ordemServicoId)
        {
            this.ordemServicoId = ordemServicoId;

            InicializarFormulario();

            CarregarServicos();
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InicializarFormulario()
        {
            Text = "Adicionar Serviço";
            StartPosition = FormStartPosition.CenterParent;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            Width = 480;
            Height = 360;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            // =====================================================
            // TÍTULO
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "Adicionar serviço",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(25, 20)
            };

            Controls.Add(lblTitulo);

            // =====================================================
            // SERVIÇO
            // =====================================================

            lblServico = CriarLabel("Serviço", 25, 65);

            cmbServico = new ComboBox
            {
                Location = new Point(25, 90),
                Width = 410,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbServico.SelectedIndexChanged += CmbServico_SelectedIndexChanged;

            Controls.Add(lblServico);
            Controls.Add(cmbServico);

            // =====================================================
            // QUANTIDADE
            // =====================================================

            lblQuantidade = CriarLabel("Quantidade", 25, 140);

            nudQuantidade = new NumericUpDown
            {
                Location = new Point(25, 165),
                Width = 180,
                Height = 32,
                Minimum = 1M,
                Maximum = 9999M,
                DecimalPlaces = 0,
                Increment = 1M,
                Value = 1M
            };

            nudQuantidade.ValueChanged += NudQuantidade_ValueChanged;

            Controls.Add(lblQuantidade);
            Controls.Add(nudQuantidade);

            // =====================================================
            // PREÇO UNITÁRIO
            // =====================================================

            lblPrecoUnitario = CriarLabel("Preço unitário", 235, 140);

            lblPrecoUnitarioValor = CriarLabelValor("R$ 0,00", 235, 165);

            Controls.Add(lblPrecoUnitario);
            Controls.Add(lblPrecoUnitarioValor);

            // =====================================================
            // SUBTOTAL
            // =====================================================

            lblSubtotal = CriarLabel("Subtotal", 25, 215);

            lblSubtotalValor = CriarLabelValor("R$ 0,00", 25, 240);

            lblSubtotalValor.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            Controls.Add(lblSubtotal);
            Controls.Add(lblSubtotalValor);

            // =====================================================
            // CANCELAR
            // =====================================================

            btnCancelar = CriarBotao("Cancelar", Color.White, Color.FromArgb(70, 70, 70));

            btnCancelar.Width = 100;
            btnCancelar.Height = 35;

            btnCancelar.Location = new Point(225, 275);

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

            btnAdicionar.Location = new Point(335, 275);

            btnAdicionar.Click += BtnAdicionar_Click;

            Controls.Add(btnAdicionar);

            AcceptButton = btnAdicionar;
            CancelButton = btnCancelar;
        }

        // =========================================================
        // SERVIÇOS
        // =========================================================

        private void CarregarServicos()
        {
            cmbServico.DataSource = null;

            cmbServico.DataSource = DadosMock.Servicos.Where(x => x.Ativo).OrderBy(x => x.Nome).ToList();

            cmbServico.DisplayMember = "Nome";
            cmbServico.ValueMember = "Id";
            cmbServico.SelectedIndex = -1;

            AtualizarValores();
        }

        // =========================================================
        // SERVIÇO SELECIONADO
        // =========================================================

        private void CmbServico_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        // ATUALIZAR VALORES
        // =========================================================

        private void AtualizarValores()
        {
            Servico servico = cmbServico.SelectedItem as Servico;

            if (servico == null)
            {
                lblPrecoUnitarioValor.Text = "R$ 0,00";
                lblSubtotalValor.Text = "R$ 0,00";

                return;
            }

            decimal preco = servico.PrecoBase;

            int quantidade = (int)nudQuantidade.Value;
            decimal subtotal = preco * quantidade;

            lblPrecoUnitarioValor.Text = preco.ToString("C2");

            lblSubtotalValor.Text = subtotal.ToString("C2");
        }

        // =========================================================
        // ADICIONAR
        // =========================================================

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            Servico servico = cmbServico.SelectedItem as Servico;

            if (servico == null)
            {
                MessageBox.Show(
                    "Selecione um serviço.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cmbServico.Focus();

                return;
            }

            int quantidade = (int)nudQuantidade.Value;

            decimal precoUnitario = servico.PrecoBase;

            decimal subtotal = quantidade * precoUnitario;

            // =====================================================
            // NOVO ITEM
            // =====================================================

            int novoId = DadosMock.OrdensServicoServicos.Count == 0
                ? 1
                : DadosMock.OrdensServicoServicos.Max(x => x.Id) + 1;

            OrdemServicoServico item = new OrdemServicoServico
            {
                Id = novoId,
                OrdemServicoId = ordemServicoId,
                ServicoId = servico.Id,
                ServicoNome = servico.Nome,
                Quantidade = quantidade,
                PrecoUnitario = precoUnitario,
                Subtotal = subtotal
            };

            DadosMock.OrdensServicoServicos.Add(item);

            AtualizarTotaisOrdemServico();

            DialogResult = DialogResult.OK;

            Close();
        }

        // =========================================================
        // ATUALIZAR TOTAIS DA OS
        // =========================================================

        private void AtualizarTotaisOrdemServico()
        {
            OrdemServico ordem = DadosMock.OrdensServico.FirstOrDefault(x => x.Id == ordemServicoId);

            if (ordem == null)
                return;

            ordem.ValorServicos = DadosMock.OrdensServicoServicos
                .Where(x => x.OrdemServicoId == ordemServicoId)
                .Sum(x => x.Subtotal);

            ordem.ValorMateriais = DadosMock.OrdensServicoMateriais
                .Where(x => x.OrdemServicoId == ordemServicoId)
                .Sum(x => x.Subtotal);

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
                FlatAppearance =
                {
                    BorderColor = background,
                    BorderSize = 1
                }
            };
        }
    }
}