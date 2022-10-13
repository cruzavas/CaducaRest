using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Controllers
{
    /// <summary>
    /// Servicios para guardar, modificar o borrar las categorías
    /// de los productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CaducaContext _context;
        //Add the object categoryDAO
        private CategoryDAO categoryDAO;

        public CategoriesController(CaducaContext context, LocService localizer)
        {
            _context = context;
            //Initialize categoryDAO with the context received as a parameter  
            categoryDAO = new CategoryDAO(context, localizer);
        }

        [HttpGet]
        public async Task<List<Category>> GetCategoryAsync()
        {
            //Cambia el método get para utilizar el 
            // objeto categoriaDAO
            return await categoryDAO.ObtenerTodoAsync();
        }

        /// <summary>
        /// Obtiene una categoría de acuerdo a su Id
        /// </summary>
        /// <returns>Los datos de la categoría</returns>
        /// <param name="id">Id de la categoría</param>
        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var categoria = await categoryDAO.ObtenerPorIdAsync(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        /// <summary>
        /// Modifica una categoría
        /// </summary>
        /// <returns>No Content si se modifico correctamente</returns>
        /// <param name="id">Id de la categoría a Modificar</param>
        /// <param name="categoria">Datos de la Categoria.</param>
        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] Category categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != categoria.Id)
                return BadRequest();

            if (!await categoryDAO.ModificarAsync(categoria))
            {
                return StatusCode(categoryDAO.customError.StatusCode, categoryDAO.customError.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Permite registrar una nueva categoría de productos
        /// </summary>
        /// <returns>Los datos de la categoría agregada</returns>
        /// <param name="categoria">Datos de la categoría</param>
        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Cambiamos el código para agregar aquí la clase.
            //Si no fue correcto regresamos el mensaje de error devuelto 
            if (!await categoryDAO.AgregarAsync(categoria))
            {
                return StatusCode(categoryDAO.customError.StatusCode, categoryDAO.customError.Message);
            }
            return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
        }

        /// <summary>
        /// Permite borrar una categoría
        /// </summary>
        /// <returns>Los datos de la categoría eliminada</returns>
        /// <param name="id">Id de la categoría a borrar</param>
        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!await categoryDAO.BorraAsync(id))
            {
                return StatusCode(categoryDAO.customError.StatusCode, categoryDAO.customError.Message);
            }
            return Ok();
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
