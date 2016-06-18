using MapNat.entities.pessoa;
using MapNat.helpers.ws.pessoa;

namespace MapNat.controllers
{
    public class PessoasController
{
    private string respCode;
    PessoaDAO PessoaDAO = new PessoaDAO();

    /// <summary>
    /// Busca Lista de latitudes e longitudes cadastradas no banco de dados
    /// </summary>
    /// <returns></returns>
    internal ResponsePessoa ObterPessoa(int PessoaCodigo)
    {
        var resp = new ResponsePessoa();

            //Se a aplicação possui acesso, busca as informações, se não retorna mensagem
            //Pegaos Locais no banco de dados
            resp = PessoaDAO.GetPessoa(PessoaCodigo, out respCode);

            //Se encontrou os pessoa
            if (resp.pessoa.Count > 0)
            {
                resp.Status.Sucesso = "1";
                resp.Status.Codigo = "SUCCESS001";
                resp.Status.Mensagem = "Dados Retornados com sucesso";
            }
            else
            {
                resp.Status.Sucesso = "0";
                resp.Status.Codigo = "FAILED002";
                resp.Status.Mensagem = respCode;
            }

        return resp;
    }

  }
}