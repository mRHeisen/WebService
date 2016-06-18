using MapNat.helpers.storage;

namespace MapNat.entities.authenticate
{
    public class AuthenticateDAO
    {
        /// <summary>
        /// Autentica aplicação que está consumindo
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="PublicKey"></param>
        /// <returns></returns>
        public bool Authenticate(string ApiKey, string PublicKey, out string _errorCod)
        {
            bool resp = false;
            _errorCod = string.Empty;

            //Busca aplicação
            using (Database db = new Database())
            {
                db.ClearParameters();
                db.SqlStat.Append("SELECT Descricao FROM Aplicacoes WHERE ApiKey=@ApiKey AND PublicKey=@PublicKey");                
                db.CreateParameter("@ApiKey", ApiKey);
                db.CreateParameter("@PublicKey", PublicKey);

                if (db.ExecuteDataReader())
                {
                    //verifica se aplicação existe
                    if (db.DataReader.Read())
                    {
                        resp = true;
                    }
                }
                else
                {
                    _errorCod = db.exception.Message; //erro na consulta ao banco de dados
                }
            }
            return resp;
        }
    }
}