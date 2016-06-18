using MapNat.entities.exercicio;
using MapNat.helpers.ws.exercicio;

namespace MapNat.controllers
{
    /// <summary>
    /// Aqui é a classe que irá executar os métodos que trataram da sinformaçõese e buscará os dados que o Web Service Retornar
    /// </summary>
    public class ExercicioController
    {
        private string respCode;
        ExercicioDAO ExercicioDAO = new ExercicioDAO();


        internal ResponseExercicio ObterExecicios(int CodPessoa)
        {
            var resp = new ResponseExercicio();

            //Se a aplicação possui acesso, busca as informações, se não retorna mensagem
            
                //Pegaos Locais no banco de dados
                resp = ExercicioDAO.GetExercicio(CodPessoa, out respCode);

            //Se encontrou os Exercicio
            if (resp.exercicio.Count > 0)
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

        internal ResponseExercicio PutExercicio(string ApiKey, string PublicKey, string ID, string Latitude, string Longitude, string Descricao)
        {
            var resp = new ResponseExercicio();

                //Grava um local ou edita um existente
                resp = ExercicioDAO.PutExercicio(ID, Latitude, Longitude, Descricao, out respCode);

                //Se efetuou a tarefa e retornou dados
                if (resp.exercicio.Count > 0)
                {
                    resp.Status.Sucesso = "1";
                    resp.Status.Codigo = "SUCCESS001";
                    resp.Status.Mensagem = "Dados salvos com sucesso";
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