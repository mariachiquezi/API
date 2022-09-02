using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace DesafioCursosGratuitos.Utils
{
    //para ser singleton tem que ser estatica
    public static class Uploud
    {
        //Upload
        //verificar se a extensao eh permitida ou nao para fazer uploud da imagem
        public static string UploudFile(IFormFile arquivo,string[]extensoesPermitidas, string diretorio)
        {
            try
            {
                //verificar as pastas
                var pasta = Path.Combine("StaticFile", diretorio);

                //caminho para pegar a raiz + a pasta
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);

                //verificar se existe o arquivo
                if (arquivo.Length > 0)
                {
                    //pegando nome do arquivo
                    string nomeArquivo = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');

                    //validando a extensao
                    if (ValidarExtensao(extensoesPermitidas, nomeArquivo))
                    {
                        var extensao = RetornarExtensao(nomeArquivo);
                        //para impedir de dublicar o arquivo
                        var novoNome = $"{Guid.NewGuid()}.{extensao}";

                        //botando caminho da imagem
                        var caminhoCompleto = Path.Combine(caminho, novoNome);

                        //salvar o arquivo
                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            arquivo.CopyTo(stream);
                        }
                        return novoNome;
                    }
                }
                //caso ele nao consiga fazer o stream
                return "";
            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
        }

        //Validar extensão de arquivo

        public static bool ValidarExtensao(string[] extensoesPermitidas, string nomeArquivo)
        {
            //retornando as extencoes
            string extensao = RetornarExtensao(nomeArquivo);

            //foreach nas extensoes permitidas
            foreach (string ext in extensoesPermitidas)
            {
                //se ext foi igual a extensao do arquivo esta valido
                if (ext == extensao)
                {
                    return true;
                }
            }
            return false;
        }
        //Remover arquivo

        //Retornar a extensão
        public static string RetornarExtensao(string nomeArquivo)
        {
            //transformar em array e tirar o ponto

            string[]dados = nomeArquivo.Split('.');
            //pegar tamanho total-1 (ultimo)
            return dados[dados.Length - 1];
        }
    }
}
