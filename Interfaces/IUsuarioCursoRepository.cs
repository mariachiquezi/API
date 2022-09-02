using DesafioCursosGratuitos.Models;
using System.Collections.Generic;

namespace DesafioCursosGratuitos.Interfaces
{
    public interface IUsuarioCursoRepository
    {
        //Read
        
        ICollection<UsuarioCurso> GetAll();

        //Create
        UsuarioCurso Insert(UsuarioCurso usuario_curso);

        //Update
        UsuarioCurso Update(int id, UsuarioCurso usuario_curso);

        //Delete
        bool Delete(int id);
    }
}
