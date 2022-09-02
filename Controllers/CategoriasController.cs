using DesafioCursosGratuitos.Models;
using DesafioCursosGratuitos.Repositories;
using DesafioCursosGratuitos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DesafioCursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        //repositorio
        private CategoriaRepository repositorio = new CategoriaRepository();

        /// <summary>
        /// Cadastramento do objeto categorias
        /// </summary>
        /// <param name="categoria"> Dados de Categoria</param>
        /// <returns>Dados cadastrados</returns>
        
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Categoria categoria, IFormFile arquivo)
        {
            try
            {
                #region Uploud de imagens
                //passar o nome da imagem pra ser salva
                //determinando as extensoes permitidas
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };

                //chama a classe estatica
                string uploudResultado = Uploud.UploudFile(arquivo, extensoesPermitidas, "Images");

                //caso esteja vazio
                if (uploudResultado == "")
                {
                    return BadRequest("Arquivo nao encontrado");
                }
                categoria.Imagem = uploudResultado;
                #endregion

                //chamando o repositorio
                repositorio.Insert(categoria);

                return Ok(categoria);
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
        /// Listando categoria
        /// </summary>
        /// <returns>Categoria listada</returns>

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                //chamando repositorio
                var categorias = repositorio.GetAll();

                return Ok(categorias);
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
        /// Para alterar alguma categoria por id
        /// </summary>
        /// <param name="id">Pegar id para identificar categoria que vai ser alterada</param>
        /// <param name="categoria">Guardar dado categoria</param>
        /// <returns>Categoria alterada</returns>

        [HttpPut("{id}")]

        public IActionResult Alterar([FromForm] Categoria categoria,int id,IFormFile arquivo)
        {
            try
            {
                #region Uploud de imagens
                //passar o nome da imagem pra ser salva
                //determinando as extensoes permitidas
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };

                //chama a classe estatica
                string uploudResultado = Uploud.UploudFile(arquivo, extensoesPermitidas, "Images");

                //caso esteja vazio
                if (uploudResultado == "")
                {
                    return BadRequest("Arquivo nao encontrado");
                }
                categoria.Imagem = uploudResultado;
                #endregion

                //caso o id que inserir nao seja encontrado
                var buscarCategoria = repositorio.GetById(id);
                if (buscarCategoria == null)
                {
                    return NotFound();
                }

                //chamando repositorio
                var categoriaAlterada= repositorio.Update(id, categoria);
               
                return Ok(categoria);
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
        /// Para deletar algo de categoria
        /// </summary>
        /// <param name="id">Excluir por id</param>
        /// <returns>Categoria excluida</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                //caso o id que inserir nao seja encontrado
                var buscarCategoria = repositorio.GetById(id);
                if (buscarCategoria == null)
                {
                    return NotFound();
                }

                //chamando repositorio
                repositorio.Delete(id);
                    return Ok(new
                    {
                        msg = "Categoria excluida com sucesso"
                    });
            }

            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexao ou impossível excluir categorias pois há uma relaçao com outra classe.",
                    erro = ex.Message
                });
            }
        }

    }
}
    

  
