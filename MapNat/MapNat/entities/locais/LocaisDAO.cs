using MapNat.helpers.ws.locais;
using MapNat.models.locais;
using MapNat.helpers.storage;
using System.Collections.Generic;
using System;

namespace MapNat.entities.locais
{
    public class LocaisDAO
    {
        /// <summary>
        /// Busca Latitude e longitude do banco de dados
        /// </summary>
        /// <param name="_errorCod"></param>
        /// <returns></returns>
        public ResponseLocais GetLocais(int CodPessoa, out string _errorCod)
        {
            ResponseLocais response = new ResponseLocais();
            response.locais = new List<Locais>();
            _errorCod = string.Empty;

            //Busca latitude e longitude
            using (Database db = new Database())
            {
                db.Clear();
                db.SqlStat.Append("select [Url], [Nome], [Repeticoes] from[hf].[Exercicio] ex inner join[hf].[ImagemExercicio] imgEx on imgEx.[ExercicioCodigo] = ex.[ExercicioCodigo] inner join[hf].[Serie] s on ex.[ExercicioCodigo] = s.[ExercicioCodigo]");


                if (db.ExecuteDataReader())
                {
                    //Busca todas latitudes e longitudes colocando na List do ResponseLocais
                    while (db.DataReader.Read())
                    {
                        response.locais.Add(new Locais(db.GetString("Url"), db.GetString("Nome"), Convert.ToString(db.GetInt("Repeticoes")), "hello"));
                    }
                }
                else
                {
                    _errorCod = db.exception.Message; //erro na consulta ao banco de dados
                }
            }
            return response;
        }

        /// <summary>
        /// Grava um local com suas informações de descrição, latitude e longitude, caso seja passa um ID altera algum dado desse local
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
        /// <param name="Descricao"></param>
        /// <param name="_errorCod"></param>
        /// <returns></returns>
        public ResponseLocais PutLocais(string ID, string Latitude, string Longitude, string Descricao, out string _errorCod)
        {            
            ResponseLocais response = new ResponseLocais();
            response.locais = new List<Locais>();
            _errorCod = string.Empty;

            //Busca latitude e longitude
            using (Database db = new Database())
            {
                db.ClearParameters();
                if (!string.IsNullOrEmpty(ID))
                {
                    db.SqlStat.Append("UPDATE Locais SET Descricao = @Descricao, Latitude = @Latitude, Longitude = @Longitude WHERE ID = @ID ");
                    db.CreateParameter("@descricao", Descricao);
                    db.CreateParameter("@Latitude", Latitude);
                    db.CreateParameter("@Longitude", Longitude);
                    db.CreateParameter("@ID", ID);

                    if (db.ExecuteNonQuery())
                    {
                        //Adiciona na Listos dados alterados para retornar eles mesmos pelo Web Service
                        response.locais.Add(new Locais(Convert.ToString(ID), Descricao, Latitude, Longitude));
                    }
                    else
                    {                       
                        _errorCod = db.exception.Message; //erro ao atualizar os dados
                    }
                }
                else
                {
                    //No Outpu Inserted.ID ele retornao ID recem criado no banco de dados para retornar no Web Service
                    db.SqlStat.Append("INSERT INTO Locais (Descricao, Latitude, Longitude) OUTPUT Inserted.ID VALUES (@Descricao, @Latitude, @Longitude) ");
                    db.CreateParameter("@descricao", Descricao);
                    db.CreateParameter("@Latitude", Latitude);
                    db.CreateParameter("@Longitude", Longitude);

                    if (db.ExecuteNonQuery())
                    {
                        //Grava um novo local no banco de dados e retorna os dados gerados de volta pelo Web Service
                        //response.locais.Add(new Locais(db.ExecuteScalar(), Descricao, Latitude, Longitude));
                    }
                    else
                    {                        
                        _errorCod = db.exception.Message; //erro ao atualizar os dados
                    }
                }
            }
            return response;
        }
    }
}