﻿using System.Runtime.Serialization;

namespace MapNat.models.pessoa
{
    /// <summary>
    /// Este é o model com as propriedades qeu serão usadas em cada caso. 
    /// O DataMember em cima de cada propriedade com o Order diz a ordem em que os valores irão aperecer no JSON
    /// </summary>
    [DataContract]
    public class Pessoa
    {
        public Pessoa()
        { }


        public Pessoa(int? PessoaCodigo, string nome, int? tipo)
        {
            this.PessoaCodigo = PessoaCodigo;
            this.nome = nome;
            this.tipo = tipo;
        }

        [DataMember(Order = 0)]
        public int? PessoaCodigo { get; set; }
        [DataMember(Order = 1)]
        public string nome { get; set; }
        [DataMember(Order = 2)]
        public int? tipo { get; set; }
    }
}