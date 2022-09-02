using DesafioCursosGratuitos.Models;
using System.Collections.Generic;

namespace DesafioCursosGratuitos.Interfaces
{
    public interface ICursoRepository
    {
        //Read
        
        ICollection<Curso> GetAll();

        //Create
        Curso Insert(Curso curso);

        //Update
        Curso Update(int id, Curso curso);

        //Delete
        bool Delete(int id);
    }
}
