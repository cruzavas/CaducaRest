using CaducaRest.Core;
using CaducaRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.DAO
{
    public class CategoriaDAO
    {
        private readonly CaducaContext context;
        public CustomError customError;

        /// <summary>
        /// Clase para acceso a la base de datos
        /// </summary>
        /// <param name="context"></param>
        public CategoriaDAO(CaducaContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Obtiene todas las categorias
        /// </summary>
        /// <returns></returns>
        public async Task<List<Categoria>> ObtenerTodoAsync()
        {
            return await context.Categoria.ToListAsync();
        }

        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            return await context.Categoria.FindAsync(id);
        }

        public async Task<bool> AgregarAsync(Categoria categoria)
        {
            Categoria registroRepetido;

            registroRepetido = context.Categoria.FirstOrDefault(c => c.Nombre == categoria.Nombre);
            if (registroRepetido != null)
            {
                customError = new CustomError(400,
                      "Ya existe una categoría con este nombre, por favor teclea un nombre diferente", "Nombre");

                return false;
            }
            registroRepetido = context.Categoria.FirstOrDefault(c => c.Clave == categoria.Clave);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, 
                    "Ya existe una categoría con esta clave, por favor teclea una clave diferente", "Clave");

                return false;
            }

            context.Categoria.Add(categoria);
            await context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Modidica una categoria
        /// </summary>
        /// <param name="categoria">Datos de la categoria</param>
        /// <returns></returns>
        public async Task<bool> ModificarAsync(Categoria categoria)
        {
            Categoria registroRepetido;
            try
            {
                //Se busca si existe una categoria con el mismo nombre 
                //pero diferente Id
                registroRepetido = context.Categoria.FirstOrDefault(c => c.Nombre == categoria.Nombre && c.Id != categoria.Id);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400,
                                   "Ya existe una categoría con este nombre, por favor teclea un nombre diferente", "Nombre");
                    return false;
                }
                registroRepetido = context.Categoria.FirstOrDefault(c => c.Clave == categoria.Clave && c.Id != categoria.Id);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400,
                                     "Ya existe una categoría con esta clave, por favor teclea una clave diferente", "Clave");
                    return false;
                }
                context.Entry(categoria).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteCategoria(categoria.Id))
                {
                    customError = new CustomError(400, "La categoría ya no existe", "Categoría");
                    return false;
                }
            }

            return true;
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

            context.Categoria.Remove(categoria);
            await context.SaveChangesAsync();

            return true;
        }

        private bool ExisteCategoria(int id)
        {
            return context.Categoria.Any(e => e.Id == id);
        }
    }
}
