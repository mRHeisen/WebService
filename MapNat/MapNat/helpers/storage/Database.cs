using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MapNat.helpers.storage
{
    /// <summary>
    /// Classe do objeto Database para conexão com banco de dados otimizada exclusivamente para utilização da base MSSQLServer
    /// </summary>
    public class Database : IDisposable
    {
        /// <summary>
        /// Construtores
        /// </summary>
        public Database()
        {
            //Define os dados básicos para a classe de conexão com banco de dados
            this.DefineDataDefault();
        }

        //Construtor com partâmetro de string de conexão
        public Database(string strConn)
        {
            //Define os dados básicos para a classe de conexão com banco de dados
            this.connectionString = strConn;
            this.DefineDataDefault();
        }

        /// <summary>
        /// Propriedades Privadas
        /// </summary>
        private SqlConnection sqlConn { get; set; }
        private string connectionString { get; set; }
        private DbCommand cmd { get; set; }
        private List<SqlParameter> parameters { get; set; }

        /// <summary>
        /// Propriedades Públicas (set privado)
        /// </summary>
        public DbDataReader DataReader { get; private set; }
        public SqlDataAdapter DataAdapter { get; private set; }
        public Exception exception { get; private set; }
        public string errorMessage { get; private set; }

        /// <summary>
        /// Propriedades Públicas
        /// </summary>
        public StringBuilder SqlStat { get; set; }

        /// <summary>
        /// Abre conexão com banco de dados
        /// </summary>
        public void Open()
        {
            if ((this.sqlConn != null) && ((this.sqlConn.State == System.Data.ConnectionState.Closed)))
            {
                this.sqlConn.Open();
            }
        }

        /// <summary>
        /// Fecha conexão com banco de dados
        /// </summary>
        public void Close()
        {
            this.Clear();
            if (this.sqlConn != null) { this.sqlConn.Close(); }
        }

        /// <summary>
        /// Libera recursos utilizados nas consultas
        /// </summary>
        public void Clear()
        {
            if (this.cmd != null) { this.cmd.Dispose(); }
            if (this.DataReader != null) { this.DataReader.Dispose(); }
            if (this.DataAdapter != null) { this.DataAdapter.Dispose(); }

            this.ClearParameters();
            this.SqlStat.Clear();
        }

        /// <summary>
        /// Cria parâmetro para consulta
        /// </summary>
        /// <param name="param">Nome do parâmetro</param>
        /// <param name="valor">Valor do parâmetro</param>
        public void CreateParameter(string param, object valor)
        {
            if (!String.IsNullOrEmpty(param))
            {
                if (valor == null) { valor = DBNull.Value; }
                this.parameters.Add(new SqlParameter(param, valor));
            }
        }

        /// <summary>
        /// Remove todos parâmetros
        /// </summary>
        public void ClearParameters()
        {
            if (this.parameters != null) { this.parameters.Clear(); }
        }

        /// <summary>
        /// Executa consulta e cria DataAdapter com resultado
        /// </summary>
        /// <returns></returns>
        public Boolean ExecuteDataAdapter()
        {
            bool resp = true;
            this.PrepareExecute();

            try
            {
                this.DataAdapter = new SqlDataAdapter((SqlCommand)this.cmd);
            }
            catch (Exception ex)
            {
                this.exception = ex;
                this.errorMessage = "Erro ao executar consulta com leitura de dados";
                this.Close(); //Conexão é fechada em caso de erro
                resp = false;
            }
            return resp;
        }

        /// <summary>
        /// Executa determinada consulta e atribui valores para o dataReader da classe
        /// </summary>
        /// <returns></returns>
        public Boolean ExecuteDataReader()
        {
            bool resp = true;
            this.PrepareExecute();

            try
            {
                this.DataReader = this.cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                this.exception = ex;
                this.errorMessage = "Erro ao executar consulta com leitura de dados";
                this.Close(); //Conexão é fechada em caso de erro
                resp = false;
            }
            return resp;
        }

        /// <summary>
        /// Executa determinada consulta
        /// </summary>
        /// <returns></returns>
        public Boolean ExecuteNonQuery()
        {
            bool resp = true;
            this.PrepareExecute();

            try
            {
                this.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.exception = ex;
                this.errorMessage = "Erro ao executar consulta";
                this.Close(); //Conexão é fechada em caso de erro
                resp = false;
            }
            return resp;
        }

        /// <summary>
        /// Executa determinada consulta
        /// </summary>
        /// <returns></returns>
        public int ExecuteScalar()
        {
            int resp = 0;
            this.PrepareExecute();

            try
            {
                resp = Convert.ToInt32(this.cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                this.exception = ex;
                this.errorMessage = "Erro ao executar consulta";
                this.Close(); //Conexão é fechada em caso de erro
                resp = 0;
            }
            return resp;
        }

        /// <summary>
        /// Retorna valor do banco de dados como String
        /// </summary>
        /// <param name="_parameter">Campo do banco de dados</param>
        /// <returns>null / valor</returns>
        public string GetString(string _campo)
        {
            int ordinal = this.DataReader.GetOrdinal(_campo);
            return this.DataReader.IsDBNull(ordinal) ? null : this.DataReader.GetString(ordinal);
        }

        /// <summary>
        /// Retorna valor do banco de dados como int
        /// </summary>
        /// <param name="_campo">Campo do banco de dados</param>
        /// <returns>null / valor</returns>
        public int? GetInt(string _campo)
        {
            int ordinal = this.DataReader.GetOrdinal(_campo);
            return this.DataReader.IsDBNull(ordinal) ? null : (int?)this.DataReader.GetInt32(ordinal);
        }

        /// <summary>
        /// Retorna valor do banco de dados como boolean
        /// </summary>
        /// <param name="_campo">Campo do banco de dados</param>
        /// <returns>Caso valor no banco seja NULL, retorna false</returns>
        public bool GetBool(string _campo)
        {
            int ordinal = this.DataReader.GetOrdinal(_campo);
            return this.DataReader.IsDBNull(ordinal) ? false : this.DataReader.GetBoolean(ordinal);
        }

        /// <summary>
        /// Retorna valor do banco de dados como DateTime
        /// </summary>
        /// <param name="_campo">Campo do banco de dados</param>
        /// <returns>null / valor</returns>
        public DateTime? GetDate(string _campo)
        {
            int ordinal = this.DataReader.GetOrdinal(_campo);
            return this.DataReader.IsDBNull(ordinal) ? null : (DateTime?)this.DataReader.GetDateTime(ordinal);
        }

        /// <summary>
        /// Retorna valor do banco de dados como double
        /// </summary>
        /// <param name="_campo">Campo do banco de dados</param>
        /// <returns>null / valor</returns>
        public double? GetDouble(string _campo)
        {
            int ordinal = this.DataReader.GetOrdinal(_campo);
            return this.DataReader.IsDBNull(ordinal) ? null : (double?)this.DataReader.GetDouble(ordinal);
        }

        /// <summary>
        /// Retorna valor do banco de dados como decimal
        /// </summary>
        /// <param name="_campo">Campo do banco de dados</param>
        /// <returns>null / valor</returns>
        public decimal? GetDecimal(string _campo)
        {
            int ordinal = this.DataReader.GetOrdinal(_campo);
            return this.DataReader.IsDBNull(ordinal) ? null : (decimal?)this.DataReader.GetDecimal(ordinal);
        }

        /// <summary>
        /// Retorna valor do banco de dados como float
        /// </summary>
        /// <param name="_campo">Campo do banco de dados</param>
        /// <returns>null / valor</returns>
        public float? GetFloat(string _campo)
        {
            int ordinal = this.DataReader.GetOrdinal(_campo);
            return this.DataReader.IsDBNull(ordinal) ? null : (float?)this.DataReader.GetFloat(ordinal);
        }

        /// <summary>
        /// Método para retornar um ID aleatório com relação a tabela já existente
        /// </summary>
        /// <param name="table">Nome da tabela no banco de dados</param>
        /// <param name="prefix">Prefixo a ser utilizado</param>
        /// <returns></returns>
        public string GenerateID(string table, string prefix)
        {
            int i = 0;
            bool codOk = false;
            string resp = string.Empty;
            DbCommand _cmd = this.sqlConn.CreateCommand();
            _cmd.CommandText = string.Format("SELECT COUNT(1) FROM {0} WHERE ID = @ID", table);

            //Prepara dados
            if (String.IsNullOrEmpty(prefix)) { prefix = ""; }

            while (!codOk && i < 5)
            {
                i++;
                resp = prefix + this.GenerateCode();
                _cmd.Parameters.Clear();
                _cmd.Parameters.Add(new SqlParameter("@ID", resp));
                try
                {
                    this.Open();
                    if (Convert.ToInt32(_cmd.ExecuteScalar()) == 0)
                        codOk = true;
                }
                catch (Exception e)
                {
                    codOk = false;
                    this.exception = e;
                    this.errorMessage = "Erro ao definir código aleatório para o registro";
                }
            }

            //libera variáveis
            _cmd.Parameters.Clear();
            _cmd.Dispose();

            return resp;
        }

        /// <summary>
        /// Prepara dados comuns para execução das consultas
        /// </summary>
        private void PrepareExecute()
        {
            //Tenta abrir conexão com banco de dados
            this.Open();

            if (this.cmd != null)
            {
                //Limpa lista de parâmetros (caso tenha executado consulta anterior)
                this.cmd.Parameters.Clear();

                //Define string de comando SQL
                this.cmd.CommandText = Convert.ToString(this.SqlStat);
            }

            //Se possui parâmetros de consulta, então adiciona
            if ((this.parameters != null) && (this.parameters.Count > 0))
            {
                this.cmd.Parameters.AddRange(this.parameters.ToArray<SqlParameter>());
            }
        }

        /// <summary>
        /// Método para definir os dados padrões para classe
        /// </summary>
        private void DefineDataDefault()
        {
            //Cria String para consulta e também os parâmetros
            this.SqlStat = new StringBuilder();

            //Define string de conexão
            this.DefineConnectionString();

            if (!String.IsNullOrEmpty(this.connectionString))
            {
                //cria conexão
                this.sqlConn = new SqlConnection(this.connectionString);

                if (this.sqlConn != null)
                {
                    //Cria Command
                    this.cmd = this.sqlConn.CreateCommand();

                    //Cria objeto para lista de parâmetros
                    this.parameters = new List<SqlParameter>();
                }
                else
                {
                    this.errorMessage = "Erro ao definir conexão com banco de dados";
                }
            }
            else
            {
                this.errorMessage = "Erro ao definir string de conexão com banco de dados";
            }
        }

        /// <summary>
        /// Método para definir a string de conexão com base na variável disponível no webconfig
        /// </summary>
        private void DefineConnectionString()
        {
            string _strconn = String.IsNullOrEmpty(this.connectionString) ? "HappyFitnessModel" : this.connectionString;

            //define string de conexão
            if ((ConfigurationManager.ConnectionStrings != null) && (ConfigurationManager.ConnectionStrings[_strconn] != null))
            {
                this.connectionString = ConfigurationManager.ConnectionStrings[_strconn].ConnectionString;
            }
            else
            {
                this.connectionString = string.Empty;
                this.errorMessage = "Erro ao definir string de conexão com banco de dados";
            }
        }

        /// <summary>
        /// Gera string aleatória de 9 caracteres
        /// </summary>
        /// <returns></returns>
        private string GenerateCode()
        {
            string resp = string.Empty;
            Random rnd = new Random();
            resp = "000000000" + rnd.Next(999999999).ToString();
            resp = resp.Substring(resp.Length - 9);
            return resp;
        }

        /// <summary>
        /// libera recursos
        /// </summary>
        public void Dispose()
        {
            // Liberar recursos externos
            this.Close();

            GC.SuppressFinalize(this);
        }
    }
}