namespace ERP_Oficina;

public partial class FormMain : Form
{
    private Panel pnlTopo;
    private Panel pnlMenu;
    private Panel pnlConteudo;
    public FormMain()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        WindowState = FormWindowState.Maximized;
        StartPosition = FormStartPosition.CenterScreen;
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Text = "Assistência Técnica - Home";

        // TOPO do app:

        pnlTopo = new Panel
        {
            Dock = DockStyle.Top,
            Height = 60,
            BackColor = Color.FromArgb(35, 45, 60)
        };

        Label lblTitulo = new Label
        {
            Text = "Assistência Técnica",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 18)
        };

        Label lblUsuario = new Label
        {
            Text = "João (Admin)", // só de exemplo, mas deve haver um "link" pra deslogar logo ao lado.
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10),
            AutoSize = true,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Location = new Point(100, 22)
        };

        pnlTopo.Controls.Add(lblTitulo);
        pnlTopo.Controls.Add(lblUsuario);

        // MENU:

        pnlMenu = new Panel
        {
            Dock = DockStyle.Left,
            Width = 220,
            BackColor = Color.FromArgb(45, 55, 72)
        };

        string[] menus =
        {
            "Dashboard",
            "Clientes",
            "Equipamentos",
            "Produtos",
            "Categorias",
            "Serviços",
            "Ordens de Serviço",
            "Estoque",
            "Relatórios",
            "Usuários",
            "Configurações"
        };

        // int top = 10;

        foreach (string item in menus)
        {
            Button btn = new Button
            {
                Text = item,
                Dock = DockStyle.Top,
                Height = 45,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(45, 55, 72);
            btn.Font = new Font("Segoe UI", 10);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => ((Button)s).BackColor = Color.FromArgb(70, 80, 100);
            btn.MouseLeave += (s, e) => ((Button)s).BackColor = Color.FromArgb(45, 55, 72);

            pnlMenu.Controls.Add(btn);
            pnlMenu.Controls.SetChildIndex(btn, 0);

            // top += 45;
        }

        // CONTEÚDO:

        pnlConteudo = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.WhiteSmoke
        };

        Label lblArea = new Label
        {
            Text = "Área de Trabalho",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            ForeColor = Color.Gray,
            AutoSize = true,
            Location = new Point(40, 40)
        };

        pnlConteudo.Controls.Add(lblArea);

        // FORM:

        Controls.Add(pnlConteudo);
        Controls.Add(pnlMenu);
        Controls.Add(pnlTopo);
    }
}