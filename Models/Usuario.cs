using System.ComponentModel.DataAnnotations;

namespace DesafioCursosGratuitos.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe seu nome")]
        public string Nome { get; set; }


        [RegularExpression(".+\\@.+\\...+", ErrorMessage = "Informe um email valido")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Informe sua senha")]
        [MinLength(6)]
        public string Senha { get; set; }

        public string  Imagem { get; set; }
    }
}
