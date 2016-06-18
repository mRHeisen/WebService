using System.Runtime.Serialization;

namespace MapNat.models.locais
{
    /// <summary>
    /// Este é o model com as propriedades qeu serão usadas em cada caso. 
    /// O DataMember em cima de cada propriedade com o Order diz a ordem em que os valores irão aperecer no JSON
    /// </summary>
    [DataContract]
    public class Locais
    {
        public Locais()
        { }


        public Locais(string _id, string _descricao, string _latitude, string _longitude)
        {
            this.ID = _id;
            this.Descricao = _descricao;
            this.Latitude = _latitude;
            this.Longitude = _longitude;
        }

        [DataMember(Order = 0)]
        public string  ID { get; set; }
        [DataMember(Order = 1)]
        public string Descricao { get; set; }
        [DataMember(Order = 2)]
        public string Latitude { get; set; }
        [DataMember(Order = 3)]
        public string Longitude { get; set; }
    }
}