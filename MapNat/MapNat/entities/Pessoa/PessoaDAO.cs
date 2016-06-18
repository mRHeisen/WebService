using MapNat.helpers.ws.pessoa;
using MapNat.models.pessoa;
using MapNat.helpers.storage;
using System.Collections.Generic;

namespace MapNat.entities.pessoa
{
    public class PessoaDAO
    {

        public ResponsePessoa GetPessoa(int PessoaCodigo, out string _errorCod)
        {
            ResponsePessoa response = new ResponsePessoa();
            response.pessoa = new List<Pessoa>();
            _errorCod = string.Empty;


            //Busca pessoa
            using (Database db = new Database())
            {
                db.Clear();
                //Consulta para obter pelo codico
                //db.SqlStat.Append("select [PessoaCodigo], [Nome], [tipo], [Situacao], [pass] from[hf].[Pessoa] WHERE PessoaCodigo = @ID");
                //db.CreateParameter("@ID", PessoaCodigo);
                db.SqlStat.Append("select [PessoaCodigo], [Nome], [tipo], [Situacao], [pass] from[hf].[Pessoa]");

                if (db.ExecuteDataReader())
                {
                    //Busca todas latitudes e longitudes colocando na List do ResponsePessoa
                    while (db.DataReader.Read())
                    {
                        response.pessoa.Add(new Pessoa(db.GetInt("PessoaCodigo"), db.GetString("nome"), db.GetInt("tipo"), db.GetString("Situacao"), db.GetString("pass")));
                    }
                }
                else
                {
                    _errorCod = db.exception.Message; //erro na consulta ao banco de dados
                }
            }
            return response;
        }

    }
}