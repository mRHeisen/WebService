using MapNat.helpers.ws.exercicio;
using MapNat.helpers.ws.pessoa;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MapNat.services
{
    /// <summary>
    ///Interface do serviço
    /// </summary>
    [ServiceContract]
    public interface IMapNat
    {
        /// <summary>
        /// Este é um método do Web Service na interface do serviço, o mesmo método com os mesmos parâmetros que está no serviço em sí.
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="PublicKey"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseExercicio ObterExecicios(int PessoaCodigo);
        ///--------------
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponsePessoa ObterPessoa(int PessoaCodigo);

        /// <summary>
        /// Este é um método do Web Service na interface do serviço, o mesmo método com os mesmos parâmetros que está no serviço em sí. Neste método caso seja passado um ID é porque tem de editar um registro
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="PublicKey"></param>
        /// <param name="ID"></param>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <param name="Descricao"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseExercicio PutExercicio(string ApiKey, string PublicKey, string ID, string Latitude, string Longitude, string Descricao);
    }
}