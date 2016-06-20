using MapNat.helpers.ws.exercicio;
using MapNat.models.exercicio;
using MapNat.helpers.storage;
using System.Collections.Generic;
using System;

namespace MapNat.entities.exercicio
{
    public class ExercicioDAO
    {

        public ResponseExercicio GetExercicio(int CodPessoa, out string _errorCod)
        {
            ResponseExercicio response = new ResponseExercicio();
            response.exercicio = new List<Exercicio>();
            _errorCod = string.Empty;

            using (Database db = new Database())
            {
                db.Clear();
                //Consulta treino
                db.SqlStat.Append("select [Url], [Nome], [Repeticoes] from[hf].[Exercicio] ex inner join[hf].[ImagemExercicio] imgEx on imgEx.[ExercicioCodigo] = ex.[ExercicioCodigo] inner join[hf].[Serie] s on ex.[ExercicioCodigo] = s.[ExercicioCodigo]");
                //db.SqlStat.Append("select [ExercicioCodigo], [Nome], [Descricao] from[hf].[Exercicio]");

                if (db.ExecuteDataReader())
                {
                    //Busca todos
                    while (db.DataReader.Read())
                    {
                        response.exercicio.Add(new Exercicio(db.GetString("Url"), db.GetString("Nome"), db.GetInt("Repeticoes")));
                    }
                }
                else
                {
                    _errorCod = db.exception.Message; //erro na consulta ao banco de dados
                }
            }
            return response;
        }


        public ResponseExercicio PutExercicio(string ID, string Latitude, string Longitude, string Descricao, out string _errorCod)
        {
            ResponseExercicio response = new ResponseExercicio();
            response.exercicio = new List<Exercicio>();
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
                       // response.exercicio.Add(new Exercicio(Convert.ToString(ID), Descricao, Latitude, Longitude));
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