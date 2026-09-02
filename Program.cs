// namespace ERP_Oficina;

using ERP_Oficina;
using ERP_Oficina.Forms.Login; // acessa o namespace ERP_Oficina.forms.Login pra podermos utilizar Authenticator.

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        using (var login = new Authenticator())
        {
            DadosMock.CarregarDadosTestes();

            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain(Authenticator.UsuarioAutenticado));
            }
        }
    }
}