using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Producto>
	{
		public void Configure(EntityTypeBuilder<Producto> builder)
		{
			builder.HasIndex(e => e.CategoriaId)
				.HasDatabaseName("IX_ProductoCategoria");

			builder.HasOne(typeof(Categoria))
				  .WithMany()
				  .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
