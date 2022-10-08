using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaducaRest.Models;
using CaducaRest.DAO;
using CaducaRest.Resources;

namespace CaducaRest.Controllers
{
    /// <summary>
    /// Servicios para guardar, modificar o borrar las categorías
    /// de los productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CaducaContext _context;
        //Agrega el ojeto categoriaDAO
        private CategoriaDAO categoriaDAO;

        public CategoriasController(CaducaContext context, LocService localizer)
        {
            _context = context;
            //Inicializa categoriaDAO con el contexto recibido
            // como parámetro
            categoriaDAO = new CategoriaDAO(context, localizer);
        }

        [HttpGet]
        public async Task<List<Categoria>> GetCategoriaAsync()
        {
            //Cambia el método get para utilizar el 
            // objeto categoriaDAO
            return await categoriaDAO.ObtenerTodoAsync();
        }

        /// <summary>
        /// Obtiene todas las categorías registradas
        /// </summary>
        /// <returns>Todas las categorías</returns>
        // GET: api/Categorias
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
        //{
        //    return await _context.Categoria.ToListAsync();
        //}

        /// <summary>
        /// Obtiene una categoría de acuerdo a su Id
        /// </summary>
        /// <returns>Los datos de la categoría</returns>
        /// <param name="id">Id de la categoría</param>
        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria([FromRoute] int id)
        {
            var categoria = await categoriaDAO.ObtenerPorIdAsync(id);
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
        public async Task<IActionResult> PutCategoria([FromRoute] int id, [FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != categoria.Id)
                return BadRequest();

            if (!await categoriaDAO.ModificarAsync(categoria))
            {
                return StatusCode(categoriaDAO.customError.StatusCode,
                                         categoriaDAO.customError.Message);
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
        public async Task<IActionResult> PostCategoria([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Cambiamos el código para agregar aquí la clase.
            //Si no fue correcto regresamos el mensaje de error devuelto 
            if (!await categoriaDAO.AgregarAsync(categoria))
            {
                return StatusCode(categoriaDAO.customError.StatusCode,
                                  categoriaDAO.customError.Message);
            }
            return CreatedAtAction("GetCategoria",
                                      new { id = categoria.Id }, categoria);
        }

        /// <summary>
        /// Permite borrar una categoría
        /// </summary>
        /// <returns>Los datos de la categoría eliminada</returns>
        /// <param name="id">Id de la categoría a borrar</param>
        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria([FromRoute] int id)
        {
            if (!await categoriaDAO.BorraAsync(id))
            {
                return StatusCode(categoriaDAO.customError.StatusCode,
                                       categoriaDAO.customError.Message);
            }
            return Ok();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.Id == id);
        }
    }
}
