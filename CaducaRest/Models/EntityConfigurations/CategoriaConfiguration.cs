using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasIndex(e => e.Clave)
                .HasDatabaseName("UI_CategoriaClave")
                .IsUnique();
        }
    }
}
