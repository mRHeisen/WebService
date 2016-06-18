using System.Runtime.Serialization;

namespace MapNat.helpers.ws._default
{
    /// <summary>
    /// Classe responsável por definir a estrutura de mensagem Status no retorno da chamada via API. Sucesso pode ser 0(False) ou 1(True).
    ///O código pode ser pela ordem que ocorre como ERRO - NomeDoMétodo - 01, ERRO - NomeDoMétodo - 02 e assim por diante. 
    /// A Mensagem é o erro que ocorreu, ou falta de dados para consumir  serviço, etc.
    /// </summary>
    [DataContract]
    public class ResponseStatus
    {
        [DataMember(Name = "Sucesso", Order = 0)]
        public string Sucesso { get; set; }

        [DataMember(Name = "Codigo", Order = 1)]
        public string Codigo { get; set; }

        [DataMember(Name = "Mensagem", Order = 2)]
        public string Mensagem { get; set; }
    }
}