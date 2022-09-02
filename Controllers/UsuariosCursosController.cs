using DesafioCursosGratuitos.Models;
using DesafioCursosGratuitos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioCursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosCursosController : ControllerBase
    {
        //repositorio
        private UsuarioCursoRepository repositorio = new UsuarioCursoRepository();

        //POST - Cadastrar
        /// <summary>
        /// Cadastra usuarioCursoo
        /// </summary>
        /// <param name="usuario_curso">Dados do id dos usuarios e dos cursos</param>
        /// <returns>Id do usuario e do curso</returns>
        [HttpPost]
        public IActionResult Cadastrar(UsuarioCurso usuario_curso)

        {
            try
            {
                //chamando repositorio
                repositorio.Insert(usuario_curso);
                return Ok(usuario_curso);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexao",
                    erro = ex.Message
                });
            }

        }

        //GET - Listar
        /// <summary>
        /// Listar usuarioCurso
        /// </summary>
        /// <returns>Lista de usuario curso</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                //chamando repositorio
                var usuario_curso =repositorio.GetAll();
                return Ok(usuario_curso);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexao",
                    erro = ex.Message
                });
            }
        }

        //PUT - Alterar
        /// <summary>
        /// Para alterar algum usuario curso
        /// </summary>
        /// <param name="id">Id do usuarioCurso</param>
        /// <param name="usuario_curso">Guardar os dados</param>
        /// <returns>Dados alterados</returns>
        [HttpPut("{id}")]

        public IActionResult Alterar(int id, UsuarioCurso usuario_curso)
        {
            try
            {

                //caso o id que inserir nao seja encontrado
                var buscarUsuarioCurso = repositorio.GetById(id);
                if (buscarUsuarioCurso == null)
                {
                    return NotFound();
                }

                //chamando repositorio
                var usuarioCursoAlterado = repositorio.Update(id, usuario_curso); 
                return Ok(usuario_curso);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexao",
                    erro = ex.Message
                });
            }
        }

        //DELETE- Excluir
        /// <summary>
        /// Para excluir
        /// </summary>
        /// <param name="id">Id do usuarioCurso para excluir</param>
        /// <returns>Deletado</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                //caso o id que inserir nao seja encontrado
                var buscarUsuarioCurso = repositorio.GetById(id);
                if (buscarUsuarioCurso == null)
                {
                    return NotFound();
                }
                //chamando repositorio
                repositorio.Delete(id);
                    return Ok(new
                    {
                        msg = "UsuarioCurso excluido com sucesso"
                    });
            }
            

            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexao ou impossível excluir usuarioCurso pois há uma relaçao com outra classe.",
                    erro = ex.Message
                });
            }
        }




    }
}
    

        
