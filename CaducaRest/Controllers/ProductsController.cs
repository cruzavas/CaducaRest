using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaducaRest.Controllers
{
	/// <summary>
	/// Servicios para los productos
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private ProductDAO productDAO;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context"></param>
		/// <param name="localizer"></param>
		public ProductsController(CaducaContext context, LocService localizer)
		{
			productDAO = new ProductDAO(context, localizer);
		}

		/// <summary>
		/// Get all products
		/// </summary>
		/// <returns></returns>
		// GET: api/Products
		[HttpGet]
		public IEnumerable<Product> GetProducts()
		{
			return productDAO.GetAll();
		}

		/// <summary>
		/// Get product by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET: api/Products/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct([FromRoute] int id)
		{
			var product = await productDAO.GetByIdAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		/// <summary>
		/// Update a product
		/// </summary>
		/// <param name="id">Product Id</param>
		/// <param name="product">Product data</param>
		/// <returns></returns>
		// PUT: api/Products/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != product.Id)
			{
				return BadRequest();
			}

			if (!await productDAO.ModifiedAsync(product))
			{
				return StatusCode(productDAO.customError.StatusCode,
								  productDAO.customError.Message);
			}

			return NoContent();

		}

		/// <summary>
		/// Add product
		/// </summary>
		/// <param name="producto">Add product data</param>
		/// <returns></returns>
		// POST: api/Products
		[HttpPost]
		public async Task<IActionResult> PostProduct([FromBody] Product product)
		{
			if (!await productDAO.AddAsync(product))
			{
				return StatusCode(productDAO.customError.StatusCode,
								  productDAO.customError.Message);
			}

			return CreatedAtAction("GetProducto",
				   new { id = product.Id }, product);
		}

		/// <summary>
		/// Delete product
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!await productDAO.DeleteAsync(id))
			{
				return StatusCode(productDAO.customError.StatusCode,
								  productDAO.customError.Message);
			}
			return Ok();
		}

	}
}
