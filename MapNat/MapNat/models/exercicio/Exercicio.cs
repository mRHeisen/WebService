using System.Runtime.Serialization;

namespace MapNat.models.exercicio
{
    /// <summary>
    /// Este é o model com as propriedades qeu serão usadas em cada caso. 
    /// O DataMember em cima de cada propriedade com o Order diz a ordem em que os valores irão aperecer no JSON
    /// </summary>
    [DataContract]
    public class Exercicio
    {
        public Exercicio()
        { }


        public Exercicio(string Url, string Nome, int? Repeticao)
        {
            this.Url = Url;
            this.Nome = Nome;
            this.Repeticao = Repeticao;

        }

        [DataMember(Order = 0)]
        public string Url { get; set; }
        [DataMember(Order = 1)]
        public string Nome { get; set; }
        [DataMember(Order = 2)]
        public int? Repeticao { get; set; }
    }
}