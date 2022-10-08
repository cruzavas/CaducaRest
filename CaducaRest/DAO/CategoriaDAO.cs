using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using CaducaRest.Rules.Categoria;
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
	public class CategoriaDAO
	{
		private readonly CaducaContext _context;
		private readonly LocService _localizer;
		private AccesoDAO<Categoria> categoriaDAO;
		/// <summary>
		/// Mensaje de error personalizado
		/// </summary>
		public CustomError customError;

		/// <summary>
		/// Clase para acceso a la base de datos
		/// </summary>
		/// <param name="context"></param>
		/// <param name="locService"></param>
		public CategoriaDAO(CaducaContext context, LocService locService)
		{
			_context = context;
			_localizer = locService;
			categoriaDAO = new AccesoDAO<Categoria>(context, locService);
		}

		/// <summary>
		/// Obtiene todas las categorias
		/// </summary>
		/// <returns></returns>
		public async Task<List<Categoria>> ObtenerTodoAsync()
		{
			return await _context.Categoria.ToListAsync();
		}

		public async Task<Categoria> ObtenerPorIdAsync(int id)
		{
			return await _context.Categoria.FindAsync(id);
		}

		public async Task<bool> AgregarAsync(Categoria categoria)
		{
			ReglaNombreUnico nombreEsUnico = new ReglaNombreUnico
			(categoria.Id, categoria.Nombre, _context, _localizer);
			ReglaClaveUnico claveEsUnica = new ReglaClaveUnico
				(categoria.Id, categoria.Clave, _context, _localizer);

			List<IRule> reglas = new List<IRule>();
			reglas.Add(nombreEsUnico);
			reglas.Add(claveEsUnica);

			if (await categoriaDAO.AgregarAsync(categoria, reglas))
				return true;
			else
			{
				customError = categoriaDAO.customError;
				return false;
			}
		}

		/// <summary>
		/// Modifica una categoria
		/// </summary>
		/// <param name="categoria">Datos de la categoria</param>
		/// <returns></returns>
		public async Task<bool> ModificarAsync(Categoria categoria)
		{
			ReglaNombreUnico nombreEsUnico = new ReglaNombreUnico(categoria.Id, categoria.Nombre, _context, _localizer);
			ReglaClaveUnico claveEsUnica = new ReglaClaveUnico(categoria.Id, categoria.Clave, _context, _localizer);

			List<IRule> reglas = new List<IRule>();
			reglas.Add(nombreEsUnico);
			reglas.Add(claveEsUnica);

			if (await categoriaDAO.ModificarAsync(categoria, reglas))
				return true;
			else
			{
				customError = categoriaDAO.customError;
				return false;
			}
		}

		/// <summary>
		/// Permite borrar una categoría por Id
		/// </summary>
		/// <param name="id">Id de la categoría</param>
		/// <returns></returns>
		public async Task<bool> BorraAsync(int id)
		{
			var categoria = await ObtenerPorIdAsync(id);
			if (categoria == null)
			{
				customError = new CustomError(400,
							 "La categoría que deseas borrar ya no existe, probablemente fue borrada por otro usuario", "Id");

				return false;
			}

			_context.Categoria.Remove(categoria);
			await _context.SaveChangesAsync();

			return true;
		}

		private bool ExisteCategoria(int id)
		{
			return _context.Categoria.Any(e => e.Id == id);
		}
	}
}
