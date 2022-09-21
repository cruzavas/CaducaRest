using CaducaRest.Models;
using System.Threading.Tasks;

namespace CaducaRest.Repository
{
    //Interfaz con los métodos comunes como agregar, buscar
    public interface ICategoriaRepository
    {
        Task<Categoria> ObtenerCategoriaAsync(int id);
        void Agregar(Categoria categoria);
        void Borrar(Categoria categoria);
    }
}
