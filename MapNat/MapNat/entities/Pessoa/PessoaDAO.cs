using MapNat.helpers.ws.pessoa;
using MapNat.models.pessoa;
using MapNat.helpers.storage;
using System.Collections.Generic;

namespace MapNat.entities.pessoa
{
    public class PessoaDAO
    {
        /// <summary>
        /// Busca tipo e longitude do banco de dados
        /// </summary>
        /// <param name="_errorCod"></param>
        /// <returns></returns>
        public ResponsePessoa GetPessoa(int PessoaCodigo, out string _errorCod)
        {
            ResponsePessoa response = new ResponsePessoa();
            response.pessoa = new List<Pessoa>();
            _errorCod = string.Empty;

            //Busca pessoa
            using (Database db = new Database())
            {
                db.Clear();
                db.SqlStat.Append("select [PessoaCodigo], [Nome], [tipo] from[hf].[Pessoa]");

                if (db.ExecuteDataReader())
                {
                    //Busca todas latitudes e longitudes colocando na List do ResponsePessoa
                    while (db.DataReader.Read())
                    {
                        response.pessoa.Add(new Pessoa(db.GetInt("PessoaCodigo"), db.GetString("nome"), db.GetInt("tipo")));
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