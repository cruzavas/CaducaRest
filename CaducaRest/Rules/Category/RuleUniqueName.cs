using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria
{
	public class RuleUniqueName : IRule
    {
        private string _name;
        private readonly CaducaContext _context;
        private readonly LocService _localizer;
        private int _id;

        /// <summary>
        /// Mensaje de error
        /// </summary>
        public CustomError customError { get; set; }

        /// <summary>
        /// Valida que una cateogría no se llame igual al agregar
        /// </summary>
        /// <param name="nombre">Nombre de la categoría</param>
        /// <param name="context">Objeto para la bd</param>
        /// <param name="locService">Objeto para mensajes en varios
        /// idiomas</param>
        public RuleUniqueName(int id, string nombre, CaducaContext context,
                                LocService locService)
        {
            _name = nombre;
            _context = context;
            _localizer = locService;
            _id = id;
        }

        /// <summary>
        /// Permite validar que el nombre de una categoría no se
        /// repita al agregar
        /// </summary>
        /// <returns>True si no se repite la categoría</returns>
        public bool IsValid()
        {
            var registroRepetido = _context.Categoria.AsNoTracking()
                                     .FirstOrDefault(c => c.Nombre == _name
                                                     && c.Id != _id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400,
                   String.Format(_localizer
                    .GetLocalizedHtmlString("Repeteaded"),
                                 "categoría", "nombre"), "Nombre");
                return false;
            }
            return true;
        }
    }
}
