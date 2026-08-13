
namespace ERP_Oficina.Forms
{
    public class FormOrdemServico : Form
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private Label lblTitulo;

        private Label lblCliente;
        private ComboBox cmbCliente;

        private Label lblEquipamento;
        private ComboBox cmbEquipamento;

        private Label lblResponsavel;
        private ComboBox cmbResponsavel;

        private Label lblStatus;
        private ComboBox cmbStatus;

        private Label lblDataAbertura;
        private DateTimePicker dtpDataAbertura;

        private Label lblObservacoes;
        private TextBox txtObservacoes;

        private Button btnCancelar;
        private Button btnSalvar;

        // =========================================================
        // DADOS
        // =========================================================

        private OrdemServico ordemServico;

        private bool editando;

        // =========================================================
        // CONSTRUTORES
        // =========================================================

        public FormOrdemServico()
        {
            editando = false;

            InicializarFormulario();

            CarregarClientes();

            CarregarResponsaveis();

            ConfigurarNovo();
        }

        public FormOrdemServico(OrdemServico ordemServico)
        {
            this.ordemServico = ordemServico;
            editando = true;
            InicializarFormulario();
            CarregarClientes();
            CarregarResponsaveis();
            CarregarDados();
        }

        // =========================================================
        // INICIALIZAÇÃO
        // =========================================================

        private void InicializarFormulario()
        {
            Text = "Nova Ordem de Serviço";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            Width = 600;
            Height = 570;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            // =====================================================
            // TÍTULO
            // =====================================================

            lblTitulo = new Label
            {
                Text = "Nova ordem de serviço",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Controls.Add(lblTitulo);

            // =====================================================
            // CLIENTE
            // =====================================================

            lblCliente = CriarLabel("Cliente", 30, 75);
            cmbCliente = new ComboBox
            {
                Location = new Point(30, 100),
                Width = 530,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCliente.SelectedIndexChanged += CmbCliente_SelectedIndexChanged;

            Controls.Add(lblCliente);
            Controls.Add(cmbCliente);

            // =====================================================
            // EQUIPAMENTO
            // =====================================================

            lblEquipamento = CriarLabel("Equipamento", 30, 145);
            cmbEquipamento = new ComboBox
            {
                Location = new Point(30, 170),
                Width = 530,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Controls.Add(lblEquipamento);
            Controls.Add(cmbEquipamento);

            // =====================================================
            // RESPONSÁVEL
            // =====================================================

            lblResponsavel = CriarLabel("Responsável", 30, 215);
            cmbResponsavel = new ComboBox
            {
                Location = new Point(30, 240),
                Width = 255,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Controls.Add(lblResponsavel);
            Controls.Add(cmbResponsavel);

            // =====================================================
            // STATUS
            // =====================================================

            lblStatus = CriarLabel("Status", 305, 215);
            cmbStatus = new ComboBox
            {
                Location = new Point(305, 240),
                Width = 255,
                Height = 32,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbStatus.Items.AddRange(
                new object[]
                {
                    "Aberta",
                    "Em andamento",
                    "Aguardando",
                    "Concluída",
                    "Cancelada"
                }
            );

            Controls.Add(lblStatus);
            Controls.Add(cmbStatus);

            // =====================================================
            // DATA DE ABERTURA
            // =====================================================

            lblDataAbertura = CriarLabel("Data de abertura", 30, 285);
            dtpDataAbertura = new DateTimePicker
            {
                Location = new Point(30, 310),
                Width = 255,
                Height = 32,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm"
            };

            Controls.Add(lblDataAbertura);
            Controls.Add(dtpDataAbertura);

            // =====================================================
            // OBSERVAÇÕES
            // =====================================================

            lblObservacoes = CriarLabel("Observações", 30, 355);
            txtObservacoes = new TextBox
            {
                Location = new Point(30, 380),
                Width = 530,
                Height = 75,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            Controls.Add(lblObservacoes);
            Controls.Add(txtObservacoes);

            // =====================================================
            // CANCELAR
            // =====================================================

            btnCancelar = CriarBotao("Cancelar", Color.White, Color.FromArgb(70, 70, 70));
            btnCancelar.Width = 100;
            btnCancelar.Height = 35;
            btnCancelar.Location = new Point(350, 475);
            btnCancelar.Click += BtnCancelar_Click;

            Controls.Add(btnCancelar);

            // =====================================================
            // SALVAR
            // =====================================================

            btnSalvar = CriarBotao("Salvar", Color.FromArgb(0, 120, 215), Color.White);
            btnSalvar.Width = 100;
            btnSalvar.Height = 35;
            btnSalvar.Location = new Point(460, 475);
            btnSalvar.Click += BtnSalvar_Click;

            Controls.Add(btnSalvar);

            AcceptButton = btnSalvar;
            CancelButton = btnCancelar;
        }

        // =========================================================
        // NOVA OS
        // =========================================================

        private void ConfigurarNovo()
        {
            Text = "Nova Ordem de Serviço";
            lblTitulo.Text = "Nova ordem de serviço";

            cmbCliente.SelectedIndex = -1;
            cmbEquipamento.DataSource = null;
            cmbResponsavel.SelectedIndex = -1;
            cmbStatus.SelectedItem = "Aberta";

            dtpDataAbertura.Value = DateTime.Now;

            txtObservacoes.Clear();
        }

        // =========================================================
        // CLIENTES
        // =========================================================

        private void CarregarClientes()
        {
            cmbCliente.DataSource = null;
            cmbCliente.DataSource = DadosMock.Clientes.Where(x => x.Ativo).OrderBy(x => x.Nome).ToList();
            cmbCliente.DisplayMember = "Nome";
            cmbCliente.ValueMember = "Id";
            cmbCliente.SelectedIndex = -1;
        }

        // =========================================================
        // RESPONSÁVEIS
        // =========================================================

        private void CarregarResponsaveis()
        {
            cmbResponsavel.DataSource = null;
            cmbResponsavel.DataSource = DadosMock.Usuarios.Where(x => x.Ativo).OrderBy(x => x.Nome).ToList();
            cmbResponsavel.DisplayMember = "Nome";
            cmbResponsavel.ValueMember = "Id";
            cmbResponsavel.SelectedIndex = -1;
        }

        // =========================================================
        // EQUIPAMENTOS
        // =========================================================

        private void CmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cliente cliente = cmbCliente.SelectedItem as Cliente;
            if (cliente == null) return;
            cmbEquipamento.DataSource = null;
            cmbEquipamento.DataSource = DadosMock.Equipamentos.Where(x => x.ClienteId == cliente.Id).OrderBy(x => x.Descricao).ToList();
            cmbEquipamento.DisplayMember = "Descricao";
            cmbEquipamento.ValueMember = "Id";
            cmbEquipamento.SelectedIndex = -1;
        }


        // =========================================================
        // CARREGAR EDIÇÃO
        // =========================================================

        private void CarregarDados()
        {
            Text = "Editar Ordem de Serviço";
            lblTitulo.Text = "Editar ordem de serviço";
            cmbCliente.SelectedValue = ordemServico.ClienteId;
            cmbEquipamento.SelectedValue = ordemServico.EquipamentoId;
            cmbResponsavel.SelectedValue = ordemServico.ResponsavelId;
            cmbStatus.SelectedItem = ordemServico.Status;
            dtpDataAbertura.Value = ordemServico.DataAbertura;
            txtObservacoes.Text = ordemServico.Observacoes;
        }

        // =========================================================
        // SALVAR
        // =========================================================

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            // -----------------------------------------------------
            // VALIDAÇÕES
            // -----------------------------------------------------

            if (cmbCliente.SelectedItem == null)
            {
                MessageBox.Show("Selecione um cliente.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCliente.Focus();
                return;
            }

            if (cmbEquipamento.SelectedItem == null)
            {
                MessageBox.Show("Selecione um equipamento.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEquipamento.Focus();
                return;
            }

            if (cmbResponsavel.SelectedItem == null)
            {
                MessageBox.Show("Selecione o responsável pela ordem de serviço.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbResponsavel.Focus();
                return;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Selecione o status da ordem de serviço.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return;
            }

            // -----------------------------------------------------
            // OBJETOS SELECIONADOS
            // -----------------------------------------------------

            Cliente? cliente = cmbCliente.SelectedItem as Cliente;
            Equipamento? equipamento = cmbEquipamento.SelectedItem as Equipamento;
            Usuario? usuario = cmbResponsavel.SelectedItem as Usuario;
            string? status = cmbStatus.SelectedItem.ToString();

            if (cliente == null || equipamento == null || usuario == null)
            {
                return;
            }

            // -----------------------------------------------------
            // NOVA OS
            // -----------------------------------------------------

            if (!editando)
            {
                int novoId = DadosMock.OrdensServico.Count == 0 ? 1 : DadosMock.OrdensServico.Max(x => x.Id) + 1;
                ordemServico = new OrdemServico
                {
                    Id = novoId,
                    ClienteId = cliente.Id,
                    ClienteNome = cliente.Nome,
                    EquipamentoId = equipamento.Id,
                    EquipamentoNome = equipamento.Descricao,
                    ResponsavelId = usuario.Id,
                    ResponsavelNome = usuario.Nome,
                    DataAbertura = dtpDataAbertura.Value,
                    Status = status,
                    ValorMateriais = 0,
                    ValorServicos = 0,
                    ValorTotal = 0,
                    Observacoes = txtObservacoes.Text.Trim()
                };
                DadosMock.OrdensServico.Add(ordemServico);
            }

            // -----------------------------------------------------
            // EDIÇÃO
            // -----------------------------------------------------

            else
            {
                ordemServico.ClienteId = cliente.Id;
                ordemServico.ClienteNome = cliente.Nome;
                ordemServico.EquipamentoId = equipamento.Id;
                ordemServico.EquipamentoNome = equipamento.Descricao;
                ordemServico.ResponsavelId = usuario.Id;
                ordemServico.ResponsavelNome = usuario.Nome;
                ordemServico.Status = status;
                ordemServico.DataAbertura = dtpDataAbertura.Value;
                ordemServico.Observacoes = txtObservacoes.Text.Trim();
                if (status == "Concluída" && ordemServico.DataFechamento == null)
                    ordemServico.DataFechamento = DateTime.Now;
                else if (status != "Concluída")
                    ordemServico.DataFechamento = null;
            }

            // -----------------------------------------------------
            // FINALIZA
            // -----------------------------------------------------
            // AtualizarTotaisOrdens(); // atualiza valores

            DialogResult = DialogResult.OK;
            Close();
        }

        private void AtualizarTotaisOrdens()
        {
            foreach (OrdemServico ordem in DadosMock.OrdensServico)
            {
                ordem.ValorServicos = DadosMock.OrdensServicoServicos.Where(x => x.OrdemServicoId == ordem.Id).Sum(x => x.Subtotal);
                ordem.ValorMateriais = DadosMock.OrdensServicoMateriais.Where(x => x.OrdemServicoId == ordem.Id).Sum(x => x.Subtotal);
                ordem.ValorTotal = ordem.ValorServicos + ordem.ValorMateriais;
            }
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