using System;
using System.Runtime.Serialization;

namespace MapNat.models.treino
{
    /// <summary>
    /// Este é o model com as propriedades qeu serão usadas em cada caso. 
    /// O DataMember em cima de cada propriedade com o Order diz a ordem em que os valores irão aperecer no JSON
    /// </summary>
    [DataContract]
    public class Treino
    {
        public Treino()
        { }

       
        public Treino(int? TreinoCodigo, int? AlunoCodigo, int? InstrutorCodigo, int? Tipo)
        {
            this.TreinoCodigo = TreinoCodigo;
            this.AlunoCodigo = AlunoCodigo;
            this.InstrutorCodigo = InstrutorCodigo;
            this.Tipo = Tipo;
        }

        [DataMember(Order = 0)]
        public int? TreinoCodigo { get; set; }
        [DataMember(Order = 1)]
        public int? AlunoCodigo { get; set; }
        [DataMember(Order = 2)]
        public int? InstrutorCodigo { get; set; }
        [DataMember(Order = 3)]
        public int? Tipo { get; set; }
    }
}