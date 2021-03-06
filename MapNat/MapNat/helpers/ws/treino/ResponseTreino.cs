﻿using MapNat.helpers.ws._default;
using MapNat.models.treino;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MapNat.helpers.ws.treino
{
    /// <summary>
    /// Aqui ocorre o retorno sobre os dados, pois maioria dos método de Web Service e consumo são do Tipo ResponsePessoa(Ou como for chamado).
    /// O ideal é que para cada Model do projeto seja criado um Response(Nome do Model), e os método do webservice que o utilizarem seja do tipo dele, como os métodos que tenho em todas classes desse Web Service
    /// </summary>
    public class ResponseTreino
    {

        [DataMember(Order = 0)]
        public ResponseStatus Status { get; set; }


        [DataMember(Order = 1)]
        public List<Treino> treino { get; set; }

        public ResponseTreino()
        {
            this.Status = new ResponseStatus();
        }
    }
}