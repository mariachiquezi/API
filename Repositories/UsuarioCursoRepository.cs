using DesafioCursosGratuitos.Interfaces;
using DesafioCursosGratuitos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioCursosGratuitos.Repositories
{
    public class UsuarioCursoRepository : IUsuarioCursoRepository
    {
        //criar string de conexao com banco de dados
        readonly string connectionString = "data source=LAPTOP-1R5IBA4J\\SQLEXPRESS;Integrated Security=true;Initial Catalog=DesafioCursosGratuitos";
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //deletar por id
                string script = "DELETE FROM UsuarioCurso WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;


                    cmd.CommandType = CommandType.Text;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public ICollection<UsuarioCurso> GetAll()
        {
                // criacao da lista de UsuarioCurso
            var usuario_curso = new List<UsuarioCurso>();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //selecionando tudo
                string consulta = "SELECT * FROM UsuarioCurso";
                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())

                        {
                            usuario_curso.Add(new UsuarioCurso
                            {

                                Id = (int)reader[0],
                                _usuarioId = (int)reader[1],
                                _cursoId = (int)reader[2]

                            });
                        }

                    }

                }
            }

            return usuario_curso;
        }

        public UsuarioCurso Insert(UsuarioCurso usuario_curso)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //para inserir dados de UsuarioCurso
                string script = "INSERT INTO UsuarioCurso(Usuarioid,Cursoid) VALUES (@Usuarioid,@Cursoid)";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazendo declaraçao das variaveis por parametros
                    cmd.Parameters.Add("@Usuarioid", SqlDbType.Int).Value = usuario_curso._usuarioId;
                    cmd.Parameters.Add("@Cursoid", SqlDbType.Int).Value = usuario_curso._cursoId;


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            return (usuario_curso);
        }

        internal object GetById(int id)
        {
            var usuario_curso = new UsuarioCurso();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //pegar apenas um usuarioCurso
                string consulta = "SELECT * FROM UsuarioCurso WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    //declarando por id
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;

                    //ler itens
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())

                        {

                            usuario_curso.Id = (int)reader[0];
                            usuario_curso._usuarioId = (int)reader[1];
                            usuario_curso._cursoId = (int)reader[2];

                        }

                    }

                }
            }

            return usuario_curso;
        }

        public UsuarioCurso Update(int id, UsuarioCurso usuario_curso)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //para alterar um usuarioCurso
                string script = "UPDATE UsuarioCurso SET Usuarioid=@Usuarioid,Cursoid=@Cursoid WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazendo declaraçao das variaveis por parametros
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    usuario_curso.Id = id;
                }
            }
            return usuario_curso;

        }
    }
}
