using DesafioCursosGratuitos.Models;
using System.Collections.Generic;

namespace DesafioCursosGratuitos.Interfaces
{
    public interface ICategoriaRepository
    {
        //Read
        ICollection<Categoria> GetAll();

        //Create
        Categoria Insert(Categoria  categoria);

        //Update
        Categoria Update(int id, Categoria categoria);

        //Delete
        bool Delete(int id);
    }
}
