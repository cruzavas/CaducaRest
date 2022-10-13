using CaducaRest.Models;
using System.Threading.Tasks;

namespace CaducaRest.Repository
{
    //Puedes crear una clase que se conecte a la base de datos
    // por medio de entity framework core
    public class CategoriaEFRepository : ICategoriaRepository
    {
        private readonly CaducaContext contexto;
        public CategoriaEFRepository(CaducaContext contexto)
        {
            this.contexto = contexto;
        }

        public void Agregar(Category categoria)
        {
            throw new System.NotImplementedException();
        }

        public void Borrar(Category categoria)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> ObtenerCategoriaAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Category> ObtenerPorIdAsync(int id)
        {
            return await contexto.Categoria.FindAsync(id);
        }
        //Los demás métodos
    }
}
