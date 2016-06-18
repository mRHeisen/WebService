using MapNat.controllers;
using MapNat.helpers.ws.exercicio;
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

        public ResponseExercicio ObterExecicios(int PessoaCodigo)
        {
            var control = new ExercicioController();            

            return control.ObterExecicios(PessoaCodigo);
        }
        public ResponsePessoa ObterPessoa(int PessoaCodigo)
        {
            var control = new PessoasController();

            return control.ObterPessoa(PessoaCodigo);
        }

        public ResponseExercicio PutExercicio(string ApiKey, string PublicKey, string ID, string Latitude, string Longitude, string Descricao)
        {
            var control = new ExercicioController();

            return control.PutExercicio(ApiKey, PublicKey, ID, Latitude, Longitude, Descricao);
        }       
    }
}
