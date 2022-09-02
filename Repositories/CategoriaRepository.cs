using DesafioCursosGratuitos.Interfaces;
using DesafioCursosGratuitos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioCursosGratuitos.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        //criar string de conexao com banco de dados
        readonly string connectionString = "data source=LAPTOP-1R5IBA4J\\SQLEXPRESS;Integrated Security=true;Initial Catalog=DesafioCursosGratuitos";
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                //deletar por id
                string script = "DELETE FROM Categorias WHERE Id=@id";
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
        public ICollection<Categoria> GetAll()

        {
            // criacao da lista de categoria
            var categoria = new List<Categoria>();


            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //selecionando tudo
                string consulta = "SELECT * FROM Categorias";
                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())

                        {
                            //mostrar 
                            categoria.Add(new Categoria
                            {

                                Id = (int)reader[0],
                                _Categoria = (string)reader[1],
                                Imagem = (string)reader[2].ToString(),


                            }) ;
                        }


                    }
                }
            }
            return categoria;
        }

        public Categoria Insert(Categoria categoria)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //para inserir uma categoria
                string script = "INSERT INTO Categorias(Categoria,Imagem) VALUES (@Categoria,@Imagem)";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazendo declaraçao das variaveis por parametros
                    cmd.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = categoria._Categoria;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = categoria.Imagem;


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }return categoria;
        }

        internal object GetById(int id)
        {
            var categoria = new Categoria();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //pegar apenas uma categoria
                string consulta = "SELECT * FROM Categorias WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    //declarando por id
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;

                    //ler itens
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())

                        {
                            categoria.Id = (int)reader[0];
                            categoria._Categoria= (string)reader[1];
                            categoria.Imagem = (string)reader[2].ToString();
                           
                        }

                    }

                }
            }

            return categoria;
        }

        public Categoria Update(int id, Categoria categoria)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //para alterar uma categoria 
                string script = "UPDATE Categorias SET Categoria=@Categoria WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazendo declaraçao das variaveis por parametros
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = categoria._Categoria;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = categoria._Categoria;



                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    categoria.Id = id;
                }
            }
            return categoria;
        }
    }
}
