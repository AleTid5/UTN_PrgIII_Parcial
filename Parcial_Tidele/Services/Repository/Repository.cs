using Parcial_Tidele.Services.DBConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_Tidele.Services.Repository
{
    public class Repository
    {
        public String Table = null;
        public SqlConnection SqlConnection { set; get; }
        public SqlCommand SqlCommand { set; get; }
        public SqlDataReader SqlDataReader { set; get; }

        public Repository()
        {
            this.SqlConnection = new SqlConnection();
            this.SqlCommand = new SqlCommand();
        }

        public void ExecSelect(String Query)
        {
            this.PrepareExec(Query);
            this.SqlDataReader = this.SqlCommand.ExecuteReader();
        }

        public int ExecInsert(String Query)
        {
            this.PrepareExec(Query);
            return this.GetOrElse(this.SqlCommand.ExecuteScalar(), 0);
        }

        public void ExecUpdate(String Query)
        {
            this.PrepareExec(Query);
            this.SqlCommand.ExecuteNonQuery();
        }

        private int GetOrElse(object ToConvert, int Default)
        {
            try
            {
                return int.Parse(ToConvert.ToString());
            }
            catch (Exception)
            {
                return Default;
            }
        }

        /**
         * Funcion para convertir String en MD5 en BD.
         */
        protected string STR2MD5(String String)
        {
            return String.Format("CONVERT(VARCHAR(32), HashBytes('MD5', '{0}'), 2)", String);
        }

        private void PrepareExec(String Query)
        {
            this.SqlConnection.ConnectionString = DataAccessManager.cadenaConexion;
            this.SqlCommand.CommandType = System.Data.CommandType.Text;
            this.SqlCommand.CommandText = Query;
            this.SqlCommand.Connection = this.SqlConnection;
            this.SqlConnection.Open();
        }

        protected void AssertOrFail(String Message)
        {
            if (!this.SqlDataReader.HasRows)
                throw new Exception(Message);
        }

        public int GetRepositoryCount()
        {
            try
            {
                String Query = String.Format("SELECT COUNT(*) AS Count FROM {0}", this.Table);
                this.ExecSelect(Query);
                this.SqlDataReader.Read();

                return int.Parse(this.SqlDataReader["Count"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.SqlConnection.Close();
            }
        }

        protected DateTime GetOrNull(object toConvert)
        {
            try
            {
                return Convert.ToDateTime(toConvert);
            }
            catch (Exception)
            {
                return Convert.ToDateTime(null);
            }
        }
    }
}
