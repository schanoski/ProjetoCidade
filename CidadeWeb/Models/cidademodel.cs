using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CidadeWeb.Models
{
    public class CidadeModel : IDisposable
    {
        private SqlConnection connection;

        public CidadeModel()
        {
            try { 
            string strConn = "Data Source=localhost;Initial Catalog=BDCidade;Integrated Security=true";
            connection = new SqlConnection(strConn);
            connection.Open();

            }
            catch (Exception e)
            {
                throw new ArgumentException("erro ao iniciar o banco de dados", e);
            }

        }

        public void Dispose()
        {
            connection.Close();
        }

        public void Create(Cidade cidade)
        {
            try { 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Cidade VALUES (@codigo, @nome, @uf)";

            cmd.Parameters.AddWithValue("@codigo", cidade.Codigo);
            cmd.Parameters.AddWithValue("@nome", cidade.Nome);
            cmd.Parameters.AddWithValue("@uf", cidade.Uf);

            cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception();
            }

        }

        public List<Cidade> Read()
        {
            try{ 
            List<Cidade> lista = new List<Cidade>();
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Cidade";
          
                SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                Cidade cidade = new Cidade();
                cidade.Id = (int)reader["Id"];
                cidade.Codigo = (int)reader["Codigo"];
                cidade.Nome = (string)reader["Nome"];
                cidade.Uf = (string)reader["Uf"];

                lista.Add(cidade);
            }

            return lista;
            }
            catch 
            {
                throw new Exception();
            }
        }



        public void Update(Cidade cidade)
        {
            
            try { 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Cidade SET Codigo=@codigo, Nome=@nome, Uf=@uf WHERE Id=@id";

            cmd.Parameters.AddWithValue("@codigo", cidade.Codigo);
            cmd.Parameters.AddWithValue("@nome", cidade.Nome);
            cmd.Parameters.AddWithValue("@uf", cidade.Uf);
            cmd.Parameters.AddWithValue("@id", cidade.Id);

            cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception();
            }

        }


        public void Delete(int id)
        {
            try { 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Cidade WHERE id=@Id";

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Esta passando valor nulo");
            }
            catch
            {
                throw new Exception();
            }

        }


        public Cidade Read(int id)
        {
            try{ 
            Cidade cidade = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Cidade WHERE Id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                cidade = new Cidade
                {
                    Id = reader.GetInt32(0),
                    Codigo= reader.GetInt32(1),
                    Nome = reader.GetString(2),
                    Uf = reader.GetString(3)
                };
            }

            return cidade;
            }catch
            {
                throw new Exception();
            }
        }
    }
}