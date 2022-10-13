using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Allows to register the categories of the products that the company sells
    /// </summary>
    public class Category
	{
        /// <summary>
        /// category Id
        /// </summary>
        /// <value>Id increments automatically</value>
        public int Id { get; set; }

        /// <summary>
        /// Get or Set the category key
        /// </summary>
        /// <value>Category Key</value>
        [Required(ErrorMessage = "Required")]
        [Range(1, 999, ErrorMessage = "Range")]
        public int Clave { get; set; }

        /// <summary>
        /// Get or Set the category name
        /// </summary>
        /// <value>Category Name</value>
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(80)")]
        public string Nombre { get; set; }
	}
}
