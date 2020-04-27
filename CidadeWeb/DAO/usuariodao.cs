using CidadeWeb.Models;
using System;
using System.Data.SqlClient;
using CidadeWeb.DAO;
namespace CidadeWeb.DAO
{
   public class UsuarioDAO : IDisposable
    {
        Conexao objConexao = new Conexao();
        private SqlConnection connection;

        public UsuarioDAO()
        {
            try
            {
                connection = objConexao.AbrirConexao();
            }
            catch
            {
                throw new Exception();
            }
        }

        public void Dispose()
        {
            connection.Close();
        }
        
        public Usuario Consulta(Usuario u)
            {
                try
                {
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
                catch
                {
                    throw new Exception();
                }
        }
    }
}