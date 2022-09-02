using DesafioCursosGratuitos.Interfaces;
using DesafioCursosGratuitos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioCursosGratuitos.Repositories
{
    public class CursoRepository : ICursoRepository
    {
    //criar string de conexao com banco de dados
    readonly string connectionString = "data source=LAPTOP-1R5IBA4J\\SQLEXPRESS;Integrated Security=true;Initial Catalog=DesafioCursosGratuitos";
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //deletar por id
                string script = "DELETE FROM Cursos WHERE Id=@id";
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

        public ICollection<Curso> GetAll()
        {
                // criacao da lista de cursos
            var cursos = new List<Curso>();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //selecionando tudo
                string consulta = "SELECT * FROM Cursos";
                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())

                        {
                            cursos.Add(new Curso
                            {

                                Id = (int)reader[0],
                                DataHora = (DateTime)reader[1],
                                Certificado = (bool)reader[2],
                                Ativo = (bool)reader[3],
                                _categoriaId = (int)reader[4],
                            
                            });
                        }

                    }

                }
            }

            return cursos;
        }

        public Curso Insert(Curso curso)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //para inserir dados de curso
                string script = "INSERT INTO Cursos(HoraData, Certificado,Ativo, Categoriaid) VALUES (@HoraData, @Certificado,@Ativo, @Categoriaid)";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazendo declaraçao das variaveis por parametros
                    cmd.Parameters.Add("@HoraData", SqlDbType.DateTime).Value = curso.DataHora;
                    cmd.Parameters.Add("@Certificado", SqlDbType.Bit).Value = curso.Certificado;
                    cmd.Parameters.Add("@Ativo", SqlDbType.Bit).Value = curso.Ativo;
                    cmd.Parameters.Add("@CategoriaId", SqlDbType.Int).Value = curso._categoriaId;
                   


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            return curso;
        }

        internal object GetById(int id)
        {
            var curso = new Curso();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //pegar apenas um curso
                string consulta = "SELECT * FROM Cursos WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    //declarando por id
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;

                    //ler itens
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())

                        {

                            curso.Id = (int)reader[0];
                            curso.DataHora = (DateTime)reader[1];
                            curso.Certificado = (bool)reader[2];
                            curso.Ativo = (bool)reader[3];
                            curso._categoriaId = (int)reader[4];
                           

                        }

                    }

                }
            }

            return curso;
        }

        public Curso Update(int id, Curso curso)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //para alterar um curso
                string script = "UPDATE Cursos SET DataHora=@DataHora,Certificado=@Certificado,Ativo=@Ativo,Catagoriaid=@Categoriaid WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazendo declaraçao das variaveis por parametros
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@DataHora", SqlDbType.DateTime).Value = curso.DataHora;
                    cmd.Parameters.Add("@Certificado", SqlDbType.Bit).Value = curso.Certificado;
                    cmd.Parameters.Add("@Ativo", SqlDbType.Bit).Value = curso.Ativo;
                    cmd.Parameters.Add("@Categoriaid", SqlDbType.Int).Value = curso._categoriaId;


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    curso.Id = id;
                }
            }
            return curso;
        }
    }
}
