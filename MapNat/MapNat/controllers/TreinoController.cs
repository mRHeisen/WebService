using MapNat.entities.treino;
using MapNat.helpers.ws.treino;

namespace MapNat.controllers
{
    public class TreinoController
    {
        private string respCode;
        TreinoDAO TreinoDAO = new TreinoDAO();

        /// <summary>
        /// Busca Lista Pessoas
        /// </summary>
        /// <returns></returns>
        internal ResponseTreino ObterTreino()
        {
            var resp = new ResponseTreino();

            //Se a aplicação possui acesso, busca as informações, se não retorna mensagem
            //Pegaos Locais no banco de dados
            resp = TreinoDAO.GetTreino(out respCode);

            //Se encontrou os pessoa
            if (resp.treino.Count > 0)
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
        internal ResponseTreino ObterTreinoAluno(int AlunoCodigo)
        {
            var resp = new ResponseTreino();

            //Se a aplicação possui acesso, busca as informações, se não retorna mensagem
            //Pegaos Locais no banco de dados
            resp = TreinoDAO.GetTreinoAluno(AlunoCodigo, out respCode);

            //Se encontrou os pessoa
            if (resp.treino.Count > 0)
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