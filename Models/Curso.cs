using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioCursosGratuitos.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }

        
        [Required(ErrorMessage = "Informe true ou false\nTrue= Quero certificado\nFalse= Nao quero certificado")]
        public bool Certificado { get; set; }

        //para mostrar se esta ativo ou nao
        public bool Ativo { get; set; }


        //para pegar a FK de categoria
        public int _categoriaId { get; set; }
        public  Categoria categoria { get;set; }

      
    }
}
