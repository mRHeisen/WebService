﻿using MapNat.helpers.ws._default;
using MapNat.models.pessoa;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MapNat.helpers.ws.pessoa
{
    /// <summary>
    /// Aqui ocorre o retorno sobre os dados, pois maioria dos método de Web Service e consumo são do Tipo ResponsePessoa(Ou como for chamado).
    /// O ideal é que para cada Model do projeto seja criado um Response(Nome do Model), e os método do webservice que o utilizarem seja do tipo dele, como os métodos que tenho em todas classes desse Web Service
    /// </summary>
    public class ResponsePessoa
    {

        [DataMember(Order = 0)]
        public ResponseStatus Status { get; set; }


        [DataMember(Order = 1)]
        public List<Pessoa> pessoa { get; set; }

        public ResponsePessoa()
        {
            this.Status = new ResponseStatus();
        }
    }
}