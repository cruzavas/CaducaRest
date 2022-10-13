using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasIndex(e => e.CategoriaId)
				.HasDatabaseName("IX_ProductoCategoria");

			builder.HasOne(typeof(Category))
				  .WithMany()
				  .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
