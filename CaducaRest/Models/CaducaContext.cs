using CaducaRest.Models.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CaducaRest.Models
{
    /// <summary>
    /// DB context
    /// </summary>
	public class CaducaContext : DbContext
	{
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
		public CaducaContext(DbContextOptions<CaducaContext> options) : base(options)
		{
		}
        /// <summary>
        /// Creating model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        /// <summary>
        /// Categories for products
        /// </summary>
        public virtual DbSet<Category> Category { get; set; }
        /// <summary>
        /// Products
        /// </summary>
        public virtual DbSet<Product> Product { get; set; }
    }
}
