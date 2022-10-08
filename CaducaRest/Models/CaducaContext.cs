using CaducaRest.Models.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CaducaRest.Models
{
	public class CaducaContext : DbContext
	{
		public CaducaContext(DbContextOptions<CaducaContext> options) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
    }
}
