using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.DAO
{
	/// <summary>
	/// 
	/// </summary>
	public class ProductDAO
	{
		private readonly CaducaContext _context;
		private readonly LocService _localizer;

		/// <summary>
		/// Mensaje de error personalizado
		/// </summary>
		public CustomError customError;

		/// <summary>
		/// Clase para acceso a la base de datos
		/// </summary>
		/// <param name="context"></param>
		/// <param name="locService"></param>
		public ProductDAO(CaducaContext context, LocService locService)
		{
			_context = context;
            _localizer = locService;
		}

		/// <summary>
		/// Obtiene todas las Products
		/// </summary>
		/// <returns></returns>
		public List<Product> GetAll()
		{
			return _context.Product.ToList();
		}

		/// <summary>
		/// Obtiene una Product por us Id
		/// </summary>
		/// <param name="id">Id de la Product</param>
		/// <returns></returns>
		public async Task<Product> GetByIdAsync(int id)
		{
			return await _context.Product.FindAsync(id);
		}

		/// <summary>
		/// Permite agregar una nueva Product
		/// </summary>
		/// <param name="Product"></param>
		/// <returns></returns>
		public async Task<bool> AddAsync(Product Product)
		{
			Product registroRepetido;

			registroRepetido = _context.Product.
				  FirstOrDefault(c => c.Nombre == Product.Nombre);
			if (registroRepetido != null)
			{
				customError = new CustomError(400,
						String.Format(_localizer
                            .GetLocalizedHtmlString("Repeteaded"),
										  "Product",
										  "nombre"), "Nombre");
				return false;
			}
			registroRepetido = _context.Product.
					FirstOrDefault(c => c.Clave == Product.Clave);
			if (registroRepetido != null)
			{
				customError = new CustomError(400,
						String.Format(_localizer
                            .GetLocalizedHtmlString("Repeteaded"),
										"Product",
										"clave"), "Clave");
				return false;
			}

            _context.Product.Add(Product);
			await _context.SaveChangesAsync();

			return true;
		}

		/// <summary>
		/// Modidica una Product
		/// </summary>
		/// <param name="Product">Datos de la Product</param>
		/// <returns></returns>
		public async Task<bool> ModifiedAsync(Product Product)
		{
			Product registroRepetido;

			//Se busca si existe una Product con el mismo nombre 
			//pero diferente Id
			registroRepetido = _context.Product
							.FirstOrDefault(
								   c => c.Nombre == Product.Nombre
								   && c.Id != Product.Id);
			if (registroRepetido != null)
			{
				customError = new CustomError(400,
						 String.Format(_localizer
                             .GetLocalizedHtmlString("Repeteaded"),
									 "Product", "nombre"), "Nombre");
				return false;
			}
			registroRepetido = _context.Product
						.FirstOrDefault(
							c => c.Clave == Product.Clave
							&& c.Id != Product.Id);
			if (registroRepetido != null)
			{
				customError = new CustomError(400,
						   String.Format(_localizer
                               .GetLocalizedHtmlString("Repeteaded"),
								   "Product", "clave"), "Clave");
				return false;
			}
            _context.Entry(Product).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return true;
		}

		/// <summary>
		/// Permite borrar una Product por Id
		/// </summary>
		/// <param name="id">Id de la Product</param>
		/// <returns></returns>
		public async Task<bool> DeleteAsync(int id)
		{
			var Product = await GetByIdAsync(id);
			if (Product == null)
			{
				customError = new CustomError(404,
					String.Format(_localizer
                        .GetLocalizedHtmlString("NotFound"),
												"El Product"), "Id");
				return false;
			}

            _context.Product.Remove(Product);
			await _context.SaveChangesAsync();
			return true;
		}

		private bool ExisteProduct(int id)
		{
			return _context.Product.Any(e => e.Id == id);
		}
	}
}
