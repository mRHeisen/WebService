using System.Runtime.Serialization;

namespace MapNat.helpers.ws._default
{
    /// <summary>
    /// Classe responsável por fornecer a resposta para as chamadas de API.
    /// Aqui pode-se utilizar exatamente como está, serve para tratar mensagem de erro ou sucesso ao consumir um método. Juntamente com o ResponseStatus.
    /// </summary>
    [DataContract]
    public class Response
    {
        public Response()
        {
            this.Status = new ResponseStatus();
        }

        [DataMember(Name = "Status", Order = 0)]
        public ResponseStatus Status { get; set; }

    }
}