using CaducaRest.Models;
using System.Threading.Tasks;

namespace CaducaRest.Repository
{
    //Interfaz con los métodos comunes como agregar, buscar
    public interface ICategoriaRepository
    {
        Task<Category> ObtenerCategoriaAsync(int id);
        void Agregar(Category categoria);
        void Borrar(Category categoria);
    }
}
