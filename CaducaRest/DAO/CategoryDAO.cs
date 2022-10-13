using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using CaducaRest.Rules.Categoria;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.DAO
{
	/// <summary>
	/// 
	/// </summary>
	public class CategoryDAO
	{
		private readonly CaducaContext _context;
		private readonly LocService _localizer;
		private AccessDAO<Category> categoryDAO;
		/// <summary>
		/// Mensaje de error personalizado
		/// </summary>
		public CustomError customError;

		/// <summary>
		/// Clase para acceso a la base de datos
		/// </summary>
		/// <param name="context"></param>
		/// <param name="locService"></param>
		public CategoryDAO(CaducaContext context, LocService locService)
		{
			_context = context;
			_localizer = locService;
			categoryDAO = new AccessDAO<Category>(context, locService);
		}

		/// <summary>
		/// Obtiene todas las categorias
		/// </summary>
		/// <returns></returns>
		public async Task<List<Category>> ObtenerTodoAsync()
		{
			return await _context.Category.ToListAsync();
		}

		public async Task<Category> ObtenerPorIdAsync(int id)
		{
			return await _context.Category.FindAsync(id);
		}

		public async Task<bool> AgregarAsync(Category categoria)
		{
			RuleUniqueName nombreEsUnico = new RuleUniqueName
			(categoria.Id, categoria.Nombre, _context, _localizer);
			RuleUniqueKey claveEsUnica = new RuleUniqueKey
				(categoria.Id, categoria.Clave, _context, _localizer);

			List<IRule> reglas = new List<IRule>();
			reglas.Add(nombreEsUnico);
			reglas.Add(claveEsUnica);

			if (await categoryDAO.AgregarAsync(categoria, reglas))
				return true;
			else
			{
				customError = categoryDAO.customError;
				return false;
			}
		}

		/// <summary>
		/// Modifica una categoria
		/// </summary>
		/// <param name="categoria">Datos de la categoria</param>
		/// <returns></returns>
		public async Task<bool> ModificarAsync(Category categoria)
		{
			RuleUniqueName nombreEsUnico = new RuleUniqueName(categoria.Id, categoria.Nombre, _context, _localizer);
			RuleUniqueKey claveEsUnica = new RuleUniqueKey(categoria.Id, categoria.Clave, _context, _localizer);

			List<IRule> reglas = new List<IRule>();
			reglas.Add(nombreEsUnico);
			reglas.Add(claveEsUnica);

			if (await categoryDAO.ModificarAsync(categoria, reglas))
				return true;
			else
			{
				customError = categoryDAO.customError;
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

			_context.Category.Remove(categoria);
			await _context.SaveChangesAsync();

			return true;
		}

		private bool ExistCategory(int id)
		{
			return _context.Category.Any(e => e.Id == id);
		}
	}
}
