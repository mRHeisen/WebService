using MapNat.controllers;
using MapNat.helpers.ws.locais;
using MapNat.helpers.ws.pessoa;

namespace MapNat.services
{
    /// <summary>
    /// Aqui o SVC implementa a interace IMapNat que definiu os métodos para chamada e parâmetros.
    /// Aqui estão os mesmos métodos da interface com o mesmo nome e mesmos parâmetros recebidos, pois a interface é implementada aqui.
    /// Aqui apenas ocorre a implementação do que método irá fazer.
    /// </summary>
    public class MapNat : IMapNat
    {

        /// <summary>
        /// Busca dados dos locais como ID, descrição, latitude e longitude.
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="PublicKey"></param>
        /// <returns></returns>
        public ResponseLocais ObterExecicios(int PessoaCodigo)
        {
            var control = new LocaisController();            

            return control.ObterExecicios(PessoaCodigo);
        }
        public ResponsePessoa ObterPessoa(int PessoaCodigo)
        {
            var control = new PessoasController();

            return control.ObterPessoa(PessoaCodigo);
        }

        /// <summary>
        /// Grava dados dos locais como localizaçao e descrição ou atualiza local existente editando ele.
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="PublicKey"></param>
        /// <param name="ID"></param>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <param name="Descricao"></param>
        /// <returns></returns>
        public ResponseLocais PutLocais(string ApiKey, string PublicKey, string ID, string Latitude, string Longitude, string Descricao)
        {
            var control = new LocaisController();

            return control.PutLocais(ApiKey, PublicKey, ID, Latitude, Longitude, Descricao);
        }       
    }
}
