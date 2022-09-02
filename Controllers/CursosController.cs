using DesafioCursosGratuitos.Models;
using DesafioCursosGratuitos.Repositories;
using DesafioCursosGratuitos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioCursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        //repositorio
        private CursoRepository repositorio = new CursoRepository();

        /// <summary>
        /// Cadastrar cursos
        /// </summary>
        /// <param name="cursos">Guardar dados dos cursos</param>
        /// <returns>Retornar datahora e tipo do curso</returns>
        [HttpPost]
        public IActionResult Cadastrar( Curso cursos)

        {
            try
            {
               
                //chamando o repositorio
                repositorio.Insert(cursos);
                return Ok(cursos);
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
        /// Listar cursos
        /// </summary>
        /// <returns>Retornar lista</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                //chamando repositorio
                var cursos = repositorio.GetAll();

                return Ok(cursos);
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
        /// Alterar dado de curso
        /// </summary>
        /// <param name="id">Pegar id do curso</param>
        /// <param name="curso">Guardar dado alterado</param>
        /// <returns>Curso alterada</returns>
        [HttpPut("{id}")]

        public IActionResult Alterar(Curso curso,int id)
        {
            try
            {
                //caso o id que inserir nao seja encontrado
                var buscarCurso = repositorio.GetById(id);
                if (buscarCurso == null)
                {
                    return NotFound();
                }

                //chamando repositorio
                var cursoAlterada = repositorio.Update(id, curso);
                return Ok(curso);
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
        /// Excluir curso
        /// </summary>
        /// <param name="id">Pegar id que vai ser excluido</param>
        /// <returns>Curso deletado</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                //caso o id que inserir nao seja encontrado
                var buscarCurso = repositorio.GetById(id);
                if (buscarCurso == null)
                {
                    return NotFound();
                }
                //chamando repositorio
                repositorio.Delete(id);
                    return Ok(new
                    {
                        msg = "Curso excluido com sucesso"
                    });
                
            }

            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexao ou impossível excluir cursos pois há uma relaçao com outra classe.",
                    erro = ex.Message
                });
            }
        }




    }
}
