using MapNat.helpers.ws.treino;
using MapNat.models.treino;
using MapNat.helpers.storage;
using System.Collections.Generic;

namespace MapNat.entities.treino
{
    public class TreinoDAO
    {

        public ResponseTreino GetTreino(out string _errorCod)
        {
            ResponseTreino response = new ResponseTreino();
            response.treino = new List<Treino>();
            _errorCod = string.Empty;


            //Busca pessoa
            using (Database db = new Database())
            {
                db.Clear();
                //Consulta para obter pelo codico
                //db.SqlStat.Append("select [PessoaCodigo], [Nome], [tipo], [Situacao], [pass] from[hf].[Pessoa] WHERE PessoaCodigo = @ID");
                //db.CreateParameter("@ID", PessoaCodigo);
                db.SqlStat.Append("select [TreinoCodigo], [AlunoCodigo], [InstrutorCodigo], [Tipo] from[hf].[Treino]");

                if (db.ExecuteDataReader())
                {
                    //Busca todas latitudes e longitudes colocando na List do ResponsePessoa
                    while (db.DataReader.Read())
                    {
                        response.treino.Add(new Treino(db.GetInt("TreinoCodigo"), db.GetInt("AlunoCodigo"), db.GetInt("InstrutorCodigo"), db.GetInt("Tipo")));
                    }
                }
                else
                {
                    _errorCod = db.exception.Message; //erro na consulta ao banco de dados
                }
            }
            return response;
        }
        public ResponseTreino GetTreinoAluno(int AlunoCodigo, out string _errorCod)
        {
            ResponseTreino response = new ResponseTreino();
            response.treino = new List<Treino>();
            _errorCod = string.Empty;


            //Busca pessoa
            using (Database db = new Database())
            {
                db.Clear();
                //Consulta para obter pelo codico
                //db.SqlStat.Append("select [PessoaCodigo], [Nome], [tipo], [Situacao], [pass] from[hf].[Pessoa] WHERE PessoaCodigo = @ID");
                //db.CreateParameter("@ID", PessoaCodigo);
                db.SqlStat.Append("select [TreinoCodigo], [AlunoCodigo], [InstrutorCodigo], [Tipo] from[hf].[Treino] WHERE AlunoCodigo = @ID");
                db.CreateParameter("@ID", AlunoCodigo);

                if (db.ExecuteDataReader())
                {
                    //Busca todas latitudes e longitudes colocando na List do ResponsePessoa
                    while (db.DataReader.Read())
                    {
                        response.treino.Add(new Treino(db.GetInt("TreinoCodigo"), db.GetInt("AlunoCodigo"), db.GetInt("InstrutorCodigo"), db.GetInt("Tipo")));
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