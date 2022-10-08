using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Core
{
	public interface IAccesoDAO<T> where T : class
	{
        CustomError customError { get; set; }
        Task<List<T>> ObtenerTodoAsync();
        Task<T> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync(T registro, List<IRule> reglas);
        Task<bool> ModificarAsync(T registro, List<IRule> reglas);
        Task<bool> BorraAsync(int id, List<IRule> reglas, string nombreTabla);
    }
}
