using CaducaRest.Models;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace CaducaRest.Repository
{
    //Puedes tener otra clase que se conecte a la base de datos
    // con ADO .NET
    public class CategoriaADORepository : ICategoriaRepository
    {
        private SqlConnection connection;
        private string connectionString;

        public CategoriaADORepository(SqlConnection connection, string connectionString)
        {
            this.connection = connection;
            this.connectionString = connectionString;
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
            string queryString = "SELECT Id, Clave, Nombre FROM cateogoria WHERE Id > @id ";
            Categoria categoria = new Categoria();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString,
                                                           connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                categoria.Id = (int)reader[0];
                categoria.Clave = (int)reader[1];
                categoria.Nombre = reader[2].ToString();
                reader.Close();
            }

            return categoria;
        }
        //Los demás métodos
    }
}
