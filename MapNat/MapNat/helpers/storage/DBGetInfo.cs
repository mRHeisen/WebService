using System;

namespace MapNat.helpers.storage
{
    public static class DBGetInfo
    {
        /// <summary>
        /// Restaura informação de determinada coluna da tabela especificada do banco de dados
        /// DBGetInfo é para utilizar em casos que é preciso apenas uma informação com uma condição no WHERE independente da tabela.
        /// Em vez e se fazer uma consulta e ter de criar uma nova classe ou método, chama-se esta função passando os parâmetros e ela retorna o Registro do banco solicitado
        /// </summary>
        /// <param name="_table">Tabela do banco de dados</param>
        /// <param name="_returnField">Campo desejado para retorno</param>
        /// <param name="_compareField">Campo de comparação com a coluna da tabela</param>
        /// <param name="_value">Valor de conparação</param>
        /// <param name="db">Instância da Classe Database ou Null (em caso de Null será criada internamente a instância Database)</param>
        /// <returns>Retorna valor do campo especificado</returns>
        public static string Field(string _table, string _returnField, string _compareField, string _value, Database db)
        {
            string _resp = string.Empty;
            bool _haveToDispose = false;

            //se informou todos os parâmetros
            if (!String.IsNullOrEmpty(_table) && (!String.IsNullOrEmpty(_returnField)) && (!String.IsNullOrEmpty(_compareField)))
            {
                if (db == null) { db = new Database(); _haveToDispose = true; }
                db.Clear();

                db.SqlStat.Append("SELECT " + _returnField + " AS ReturnField FROM " + _table + " WHERE " + _compareField + " = @Value");
                db.CreateParameter("@Value", _value);

                if (db.ExecuteDataReader())
                {
                    if (db.DataReader.HasRows)
                    {
                        if (db.DataReader.Read())
                        {
                            _resp = Convert.ToString(db.DataReader["ReturnField"]);
                        }
                    }
                }
            }

            if (_haveToDispose)
            {
                db.Dispose();
            }

            //retorno
            return _resp;
        }
    }
}