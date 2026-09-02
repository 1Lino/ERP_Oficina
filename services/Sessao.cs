namespace ERP_Oficina.Services.Sessao;

public static class Sessao
{
    public static Usuario UsuarioAtual { get; private set; }

    public static bool Autenticado =>
        UsuarioAtual != null;

    public static void Iniciar(Usuario usuario)
    {
        UsuarioAtual = usuario;
    }

    public static void Encerrar()
    {
        UsuarioAtual = null;
    }
}