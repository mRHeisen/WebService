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


        public Exercicio(int? ExercicioCodigo, string Nome, string Descricao)
        {
            this.ExercicioCodigo = ExercicioCodigo;
            this.Nome = Nome;
            this.Descricao = Descricao;

        }

        [DataMember(Order = 0)]
        public int? ExercicioCodigo { get; set; }
        [DataMember(Order = 1)]
        public string Nome { get; set; }
        [DataMember(Order = 2)]
        public string Descricao { get; set; }
    }
}