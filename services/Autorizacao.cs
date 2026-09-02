namespace ERP_Oficina.Services.Autorizacao;

using ERP_Oficina.Services.Sessao;
using ERP_Oficina.Models.Permissao;

public static class Autorizacao
{
    public static bool TemPermissao(Permissao permissao)
    {
        // se não existe sessão, logo não existe permissão de acesso pra qualquer tela que seja do app.
        if (Sessao.UsuarioAtual == null)
            return false;

        // se o perfil do usuário for esse, então já retorna true pra qualquer permissão, pois o administrador tem permissões totais dentro do app.
        if (Sessao.UsuarioAtual.Perfil == "Administrador")
            return true;

        // mas se o perfil for qualquer outro, segue estas regras:
        switch (Sessao.UsuarioAtual.Perfil)
        {
            case "Técnico":
                return permissao switch
                {
                    Permissao.VisualizarClientes => true,
                    Permissao.EditarClientes => true,

                    Permissao.VisualizarEquipamentos => true,
                    Permissao.EditarEquipamentos => true,

                    Permissao.VisualizarProdutos => true,
                    Permissao.EditarProdutos => true,

                    Permissao.VisualizarEstoque => true,
                    Permissao.EditarEstoque => true,

                    Permissao.VisualizarOrdensServico => true,
                    Permissao.EditarOrdensServico => true,

                    Permissao.VisualizarRelatorios => true,

                    _ => false // demais permissões => false
                };

            case "Atendente":
                return permissao switch
                {
                    Permissao.VisualizarClientes => true,
                    Permissao.EditarClientes => true,

                    Permissao.VisualizarEquipamentos => true,

                    Permissao.VisualizarProdutos => true,

                    Permissao.VisualizarOrdensServico => true,
                    Permissao.EditarOrdensServico => true,

                    Permissao.VisualizarRelatorios => true,

                    _ => false
                };

            default:
                return false;
        }
    }
}