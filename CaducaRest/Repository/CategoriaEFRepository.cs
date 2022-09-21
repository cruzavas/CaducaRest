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

        public void Agregar(Categoria categoria)
        {
            throw new System.NotImplementedException();
        }

        public void Borrar(Categoria categoria)
        {
            throw new System.NotImplementedException();
        }

        public Task<Categoria> ObtenerCategoriaAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            return await contexto.Categoria.FindAsync(id);
        }
        //Los demás métodos
    }
}
