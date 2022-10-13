using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria
{
    public class RuleUniqueKey : IRule
    {
        private int _key;
        private int _id;
        private readonly CaducaContext _context;
        private readonly LocService _localizer;

        /// <summary>
        /// Mensaje de error
        /// </summary>
        public CustomError customError { get; set; }

        /// <summary>
        /// Constructor para verificar que la clave no se repite
        /// en una categoría al agregar
        /// </summary>
        /// <param name="clave">Clave de la categoría</param>
        /// <param name="context">Objeto para la bd</param>
        /// <param name="locService">Objeto para traducuir a varuis idiomas</param>
        public RuleUniqueKey(int id, int clave, CaducaContext context,
                               LocService locService)
        {
            _key = clave;
            _context = context;
            _localizer = locService;
            _id = id;
        }

        /// <summary>
        /// Indica si la clave de la categoría no se repite
        /// al agregar
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            var registroRepetido = _context.Categoria.AsNoTracking()
                                    .FirstOrDefault(c => c.Clave == _key
                                                    && c.Id != _id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(
                _localizer.GetLocalizedHtmlString("Repeteaded"),
                "categoría", "clave"), "Clave");
                return false;
            }
            return true;
        }
    }
}
