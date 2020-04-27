using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CidadeWeb.DAO
{
    public class Conexao
    {
        private SqlConnection connection = new SqlConnection();
        public Conexao()
        {
            try
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoBanco"].ConnectionString;
                connection.Open();
            }
            catch
            {
                throw new Exception();
            }
        }

        public SqlConnection AbrirConexao()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }

        public SqlConnection FecharConexao()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;

            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}