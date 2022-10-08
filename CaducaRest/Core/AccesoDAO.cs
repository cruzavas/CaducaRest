using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Core
{
    public class AccesoDAO<TEntity> : IAccesoDAO<TEntity> where TEntity : class
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;
        public CustomError customError { get; set; }

        public AccesoDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
        }

        public async Task<bool> AgregarAsync(TEntity registro,
                                             List<IRule> reglas)
        {
            foreach (var regla in reglas)
            {
                if (!regla.IsValid())
                {
                    customError = regla.customError;
                    return false;
                }
            }
            contexto.Set<TEntity>().Add(registro);
            await contexto.SaveChangesAsync();

            return true;
        }

        public async Task<bool> BorraAsync(int id, List<IRule> reglas,
                                           string nombreTabla)
        {
            var registro = await ObtenerPorIdAsync(id);
            if (registro == null)
            {
                customError = new CustomError(404, String.Format(
                  this.localizacion.GetLocalizedHtmlString("NotFound"),
                      nombreTabla), "Id");
                return false;
            }
            foreach (var regla in reglas)
            {
                if (!regla.IsValid())
                {
                    customError = regla.customError;
                    return false;
                }
            }
            contexto.Set<TEntity>().Remove(registro);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModificarAsync(TEntity registro,
                                                List<IRule> reglas)
        {
            foreach (var regla in reglas)
            {
                if (!regla.IsValid())
                {
                    customError = regla.customError;
                    return false;
                }
            }
            contexto.Entry(registro).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<TEntity> ObtenerPorIdAsync(int id)
        {
            return await contexto.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> ObtenerTodoAsync()
        {
            return await contexto.Set<TEntity>().ToListAsync();
        }
	}
}
