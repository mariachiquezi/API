namespace DesafioCursosGratuitos.Models
{
    public class UsuarioCurso
    {
        public int Id { get; set; }

        //pegar FK de usuario
        public int _usuarioId { get; set; }
        public Usuario usuarioId { get; set; }

        //pegar FK de curso
        public int _cursoId { get; set; }
        public Curso cursoId { get; set; }
    }
}
