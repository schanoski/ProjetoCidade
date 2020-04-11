using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoCidade.Models
{
    public class CidadeModel: IDisposable
    {
        private SqlConnection connection;
        public CidadeModel()
        {
            string strConn = "Data Source=localhost; Initial Catalog = CIDADE; Integrated Security=true";
            connection = new SqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public void Create(Cidade cidade)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO TabelaCidade VALUES ( @codigoMunicipio, @nomeCidade, @ufMunicipio)";

            cmd.Parameters.AddWithValue("@codigoMuncipio", cidade.codigoMunicipio);

            cmd.Parameters.AddWithValue("@nomeMuncipio", cidade.nomeMunicipio);
            cmd.Parameters.AddWithValue("@ufMuncipio", cidade.ufMunicipio);

            cmd.ExecuteNonQuery();

        }

        public List<Cidade> Read()
        {
            List<Cidade> lista = new List<Cidade>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM TabelaCidade";

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                Cidade cidade = new Cidade();
                cidade.codigoMunicipio = (int)reader["codigoMunicipio"];
                cidade.nomeMunicipio = (string)reader["nomeMunicipio"];
                cidade.ufMunicipio = (string)reader["ufMunicipio"];

                lista.Add(cidade);
            }

            return lista;
        }

        public void Update(Cidade cidade)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE TabelaCidade SET nomeMunicipio=@nomeMunicipio;  ufMunicipio=@ufMunicipio WHERE codigoMunicipio=@codigoMunicipio";

            cmd.Parameters.AddWithValue("@codigoMunicipio", cidade.codigoMunicipio);
            cmd.Parameters.AddWithValue("@nomeMunicipio", cidade.nomeMunicipio);
            cmd.Parameters.AddWithValue("@ufMunicipio", cidade.ufMunicipio);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int codigoMunicipio)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM TabelaCidade WHERE codigoMunicipio=@codigoMunicipio";

            cmd.Parameters.AddWithValue("@codigoMunicipio", codigoMunicipio);

            cmd.ExecuteNonQuery();
        }

    }
}