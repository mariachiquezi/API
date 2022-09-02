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
    public class UsuariosController : ControllerBase
    {
        //repositorio
        private UsuarioRepository repositorio = new UsuarioRepository();
       

        //POST - Cadastrar
        /// <summary>
        /// Cadastrar usuario
        /// </summary>
        /// <param name="usuario">Guardar dados do usuario</param>
        /// <returns>Retornar dados do usuario</returns>
       
        [HttpPost]
        //para mandar um arquivo
        public IActionResult Cadastrar([FromForm]Usuario usuario, IFormFile arquivo)

        {
            try
            {
                #region Upload de imagens
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
                usuario.Imagem = uploudResultado;
                #endregion


                //chamando o repositorio
                repositorio.Insert(usuario);
                return Ok(usuario);
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
        /// Para listar dados dos usuarios
        /// </summary>
        /// <returns>Listas dos dados</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                //chamando repositorio
                var usuarios =repositorio.GetAll();
       
                return Ok(usuarios);
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
        /// Para alterar algum dado do usuario
        /// </summary>
        /// <param name="id">Pegar id para alteracao</param>
        /// <param name="usuario">Guardar dado</param>
        /// <returns>Usuario alterado</returns>
     
        [HttpPut("{id}")]

        public IActionResult Alterar( [FromForm] Usuario usuario,int id, IFormFile arquivo)
        {
            try
            {
                #region Upload de imagens
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
                usuario.Imagem = uploudResultado;
                #endregion


                //caso o id que inserir nao seja encontrado
                var buscarUsuario = repositorio.GetById(id);
                if (buscarUsuario == null)
                {
                    return NotFound();
                }
                
                //chamando repositorio
              var usuarioAlterado=  repositorio.Update(id,usuario);
           
                return Ok(usuario);
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
        /// Excluir um usuario
        /// </summary>
        /// <param name="id">Pegar o id para excluir</param>
        /// <returns>Usuario deletado</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                //caso o id que inserir nao seja encontrado
                var buscarUsuario = repositorio.GetById(id);
                if (buscarUsuario == null)
                {
                    return NotFound();
                }

                //chamando repositorio
                repositorio.Delete(id);
                    return Ok(new
                    {
                        msg = "Usuario excluido com sucesso"
                    });
                
            }

            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexao ou impossível excluir usuarios pois há uma relaçao com outra classe.",
                    erro = ex.Message
                });
            }
        }



    }
}

    