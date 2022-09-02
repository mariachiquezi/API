using System.ComponentModel.DataAnnotations;

namespace DesafioCursosGratuitos.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        //categoria armazena o nome da materia do curso
        [Required(ErrorMessage = "Informe uma categoria")]
        public string _Categoria { get; set; }

        public string Imagem { get; set; }
    }
}
