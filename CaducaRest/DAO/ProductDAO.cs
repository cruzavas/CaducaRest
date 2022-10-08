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
		private readonly CaducaContext contexto;
		private readonly LocService localizacion;

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
			this.contexto = context;
			this.localizacion = locService;
		}

		/// <summary>
		/// Obtiene todas las Products
		/// </summary>
		/// <returns></returns>
		public List<Producto> ObtenerTodo()
		{
			return contexto.Producto.ToList();
		}

		/// <summary>
		/// Obtiene una Product por us Id
		/// </summary>
		/// <param name="id">Id de la Product</param>
		/// <returns></returns>
		public async Task<Producto> ObtenerPorIdAsync(int id)
		{
			return await contexto.Producto.FindAsync(id);
		}

		/// <summary>
		/// Permite agregar una nueva Product
		/// </summary>
		/// <param name="Product"></param>
		/// <returns></returns>
		public async Task<bool> AgregarAsync(Producto Product)
		{
			Producto registroRepetido;

			registroRepetido = contexto.Producto.
				  FirstOrDefault(c => c.Nombre == Product.Nombre);
			if (registroRepetido != null)
			{
				customError = new CustomError(400,
						String.Format(this.localizacion
							.GetLocalizedHtmlString("Repeteaded"),
										  "Product",
										  "nombre"), "Nombre");
				return false;
			}
			registroRepetido = contexto.Producto.
					FirstOrDefault(c => c.Clave == Product.Clave);
			if (registroRepetido != null)
			{
				customError = new CustomError(400,
						String.Format(this.localizacion
							.GetLocalizedHtmlString("Repeteaded"),
										"Product",
										"clave"), "Clave");
				return false;
			}

			contexto.Producto.Add(Product);
			await contexto.SaveChangesAsync();

			return true;
		}

		/// <summary>
		/// Modidica una Product
		/// </summary>
		/// <param name="Product">Datos de la Product</param>
		/// <returns></returns>
		public async Task<bool> ModificarAsync(Producto Product)
		{
			Producto registroRepetido;

			//Se busca si existe una Product con el mismo nombre 
			//pero diferente Id
			registroRepetido = contexto.Producto
							.FirstOrDefault(
								   c => c.Nombre == Product.Nombre
								   && c.Id != Product.Id);
			if (registroRepetido != null)
			{
				customError = new CustomError(400,
						 String.Format(this.localizacion
							 .GetLocalizedHtmlString("Repeteaded"),
									 "Product", "nombre"), "Nombre");
				return false;
			}
			registroRepetido = contexto.Producto
						.FirstOrDefault(
							c => c.Clave == Product.Clave
							&& c.Id != Product.Id);
			if (registroRepetido != null)
			{
				customError = new CustomError(400,
						   String.Format(this.localizacion
							   .GetLocalizedHtmlString("Repeteaded"),
								   "Product", "clave"), "Clave");
				return false;
			}
			contexto.Entry(Product).State = EntityState.Modified;
			await contexto.SaveChangesAsync();

			return true;
		}

		/// <summary>
		/// Permite borrar una Product por Id
		/// </summary>
		/// <param name="id">Id de la Product</param>
		/// <returns></returns>
		public async Task<bool> BorraAsync(int id)
		{
			var Product = await ObtenerPorIdAsync(id);
			if (Product == null)
			{
				customError = new CustomError(404,
					String.Format(this.localizacion
						.GetLocalizedHtmlString("NotFound"),
												"El Product"), "Id");
				return false;
			}

			contexto.Producto.Remove(Product);
			await contexto.SaveChangesAsync();
			return true;
		}

		private bool ExisteProduct(int id)
		{
			return contexto.Producto.Any(e => e.Id == id);
		}
	}
}
