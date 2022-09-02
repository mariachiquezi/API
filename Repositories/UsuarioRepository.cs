using DesafioCursosGratuitos.Interfaces;
using DesafioCursosGratuitos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioCursosGratuitos.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //string de conexao
        readonly string connectionString = "data source=LAPTOP-1R5IBA4J\\SQLEXPRESS;Integrated Security=true;Initial Catalog=DesafioCursosGratuitos";
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //deletar por id
                string script = "DELETE FROM Usuarios WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;


                    cmd.CommandType = CommandType.Text;
                    //caso nenhuma linha for afetada retorna falso
                    int linhasAfetadas= cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                    {
                        return false;
                    }
                }
            }
                return true;
        }
        public ICollection<Usuario> GetAll()
        {
                // criacao da lista de Usuarios
            var usuarios = new List<Usuario>();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //selecionando tudo
                string consulta = "SELECT * FROM Usuarios";
                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())

                        {
                            usuarios.Add(new Usuario
                            {
                                Id = (int)reader[0],
                                Nome = (string)reader[1],
                                Email = (string)reader[2],
                                Senha = (string)reader[3],
                                Imagem = (string)reader[4].ToString(),

                            });
                        }

                    }

                }
            }

            return usuarios;
        }

        public Usuario Insert(Usuario usuario)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //para inserir dados de Usuario
                string script = "INSERT INTO Usuarios(Nome, Email, Senha,Imagem) VALUES (@Nome, @Email, @Senha,@Imagem)";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazendo declaraçao das variaveis por parametros
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = usuario.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = usuario.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = usuario.Senha;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = usuario.Imagem;


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            return usuario;
        }

        internal object GetById(int id)
        {
            var usuario = new Usuario();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //pegar apenas um usuario
                string consulta = "SELECT * FROM Usuarios WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    //declarando por id
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;

                    //ler itens
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())

                        {

                            usuario.Id = (int)reader[0];
                            usuario.Nome = (string)reader[1];
                            usuario.Email = (string)reader[2];
                            usuario.Senha = (string)reader[3];
                            usuario.Imagem = (string)reader[4].ToString();

                           
                        }

                    }

                }
            }

            return usuario;
        }

        public Usuario Update(int id, Usuario usuario)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                //para alterar um usuario
                string script = "UPDATE Usuarios SET Nome=@Nome,Email=@Email,Senha=@Senha WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazendo declaraçao das variaveis por parametros
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = usuario.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = usuario.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = usuario.Senha;


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    usuario.Id = id;
                }
            }
            return usuario;
        }
    }
}
