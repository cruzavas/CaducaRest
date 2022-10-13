using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CaducaRest.Models
{
    public class Product
    {
        /// <summary>
        /// Get or Set the product id
        /// </summary>
        /// <value>Product Id</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Get or Set the category id
        /// </summary>
        /// <value>Category Id</value>
        [Required(ErrorMessage = "Required")]
        public int CategoriaId { get; set; }

        /// <summary>
        /// Get or Set the product key
        /// </summary>
        /// <value>Product Key</value>
        [Required(ErrorMessage = "Required")]
        [Range(1, 999, ErrorMessage = "Range")]
        public int Clave { get; set; }

        /// <summary>
        /// Get or Set the product name
        /// </summary>
        /// <value>Product Name</value>
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(80)")]
        public string Nombre { get; set; }
    }
}
