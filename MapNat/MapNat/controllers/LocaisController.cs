using MapNat.entities.locais;
using MapNat.helpers.ws.locais;

namespace MapNat.controllers
{
    /// <summary>
    /// Aqui é a classe que irá executar os métodos que trataram da sinformaçõese e buscará os dados que o Web Service Retornar
    /// </summary>
    public class LocaisController
    {
        private string respCode;
        LocaisDAO LocaisDAO = new LocaisDAO();

        /// <summary>
        /// Busca Lista de latitudes e longitudes cadastradas no banco de dados
        /// </summary>
        /// <returns></returns>
        internal ResponseLocais ObterExecicios(int CodPessoa)
        {
            var resp = new ResponseLocais();

            //Se a aplicação possui acesso, busca as informações, se não retorna mensagem
            
                //Pegaos Locais no banco de dados
                resp = LocaisDAO.GetLocais(CodPessoa, out respCode);

                //Se encontrou os locais
                if (resp.locais.Count > 0)
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

        /// <summary>
        /// Grava os dados básicos do local bem como descrição, latitude e longitude
        /// </summary>        
        /// <param name="ApiKey"></param>
        /// <param name="PublicKey"></param>
        /// <param name="ID"></param>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <param name="Descricao"></param>
        /// <returns></returns>
        internal ResponseLocais PutLocais(string ApiKey, string PublicKey, string ID, string Latitude, string Longitude, string Descricao)
        {
            var resp = new ResponseLocais();

                //Grava um local ou edita um existente
                resp = LocaisDAO.PutLocais(ID, Latitude, Longitude, Descricao, out respCode);

                //Se efetuou a tarefa e retornou dados
                if (resp.locais.Count > 0)
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