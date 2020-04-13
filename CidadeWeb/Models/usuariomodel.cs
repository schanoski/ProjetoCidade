using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace CidadeWeb.Models
{
   public class UsuarioModel : IDisposable
    {
        private SqlConnection connection;

        public UsuarioModel()
        {
            try { 
            string strConn = "Data Source = localhost; Initial Catalog = BDCidade; Integrated Security = true";
            connection = new SqlConnection(strConn);
            connection.Open();
            }
            catch
            {
                throw new ArgumentOutOfRangeException("erro ao conectar com o banco de dados");
            }
        }

        public void Dispose()
        {
            connection.Close();
        }

        public Usuario Consulta(Usuario u)
        {
            try { 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT senha FROM Usuario WHERE usuario=@Login";
            cmd.Parameters.AddWithValue("@Login", u.Login);
            
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                Usuario objUsuario = new Usuario
                {
                    Login = u.Login,
                    Senha = Convert.ToString(sdr["senha"])
                };

                return objUsuario;
            }
            else
            {
                return null;
            }

            }
            catch (Exception e)
            {
                throw new ArgumentException("erro ao consultar o usuário no banco de dados", e);
            }
        }

    }
}