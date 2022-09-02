using DesafioCursosGratuitos.Models;
using System.Collections.Generic;

namespace DesafioCursosGratuitos.Interfaces
{
    public interface IUsuarioRepository
    {
        //Read
        ICollection<Usuario> GetAll();

        //Create
        Usuario Insert (Usuario usuario);

        //Update
        Usuario Update (int id,Usuario usuario);   

        //Delete
        bool Delete (int id);


    }
}
